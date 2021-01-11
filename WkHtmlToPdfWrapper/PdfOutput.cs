using System;
using System.IO;

namespace WkHtmlToPdfWrapper
{
    public class PdfOutput
    {
        public string OutputFilePath { get; set; }

        public Stream OutputStream { get; set; }

        public byte[] FileBytes { get; set; }

        public Action<PdfDocument, byte[]> OutputCallback { get; set; }
    }
}