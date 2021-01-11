using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace WkHtmlToPdfWrapper
{
    public static class PdfConvert
    {
        private static PdfConvertEnvironment _environment;

        public static PdfConvertEnvironment Environment
        {
            get
            {
                if (_environment == null)
                {
                    _environment = new PdfConvertEnvironment
                    {
                        WkHtmlToPdfPath = GetWkHtmlToPdfExeLocation(),
                        Timeout = 60000
                    };
                }

                return _environment;
            }
        }

        private static string GetWkHtmlToPdfExeLocation()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wkhtmltopdf.exe");
        }

        public static byte[] ConvertHtmlToPdf(PdfDocument document, PdfConvertEnvironment environment = null)
        {
            if (environment == null)
                environment = Environment;

            if (!File.Exists(environment.WkHtmlToPdfPath))
                throw new PdfConvertException(
                    $"File '{environment.WkHtmlToPdfPath}' not found. Check if wkhtmltopdf application is installed.");
            
            //     "-q"  - silent output, only errors - no progress messages
            //     " -"  - switch output to stdout
            //     "- -" - switch input to stdin and output to stdout
            var commandLineParams = "-q " + document.GetCommandLineParameters() + " -";

            if (!string.IsNullOrEmpty(document.Html))
            {
                commandLineParams += " -";
            }

            using (var process = new Process())
            {
                try
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = environment.WkHtmlToPdfPath,
                        Arguments = commandLineParams,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    process.Start();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (!string.IsNullOrEmpty(document.Html))
                {
                    using (var stdIn = new StreamWriter(process.StandardInput.BaseStream, Encoding.UTF8))
                    {
                        stdIn.WriteLine(document.Html);
                    }
                }

                using (var outputStream = new MemoryStream())
                {
                    using (var stdOut = process.StandardOutput.BaseStream)
                    {
                        var buffer = new byte[4096];
                        int read;

                        while ((read = stdOut.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            outputStream.Write(buffer, 0, read);
                        }
                    }

                    var error = process.StandardError.ReadToEnd();

                    if (outputStream.Length == 0)
                    {
                        throw new Exception(error);
                    }

                    if (!process.WaitForExit(environment.Timeout))
                    {
                        if (!process.HasExited)
                            process.Kill();

                        throw new PdfConvertTimeoutException();
                    }

                    return outputStream.ToArray();
                }
            }
        }
    }
}