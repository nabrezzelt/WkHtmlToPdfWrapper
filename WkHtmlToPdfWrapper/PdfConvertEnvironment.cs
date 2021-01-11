namespace WkHtmlToPdfWrapper
{
    public class PdfConvertEnvironment
    {
        public PdfConvertEnvironment()
        {
            Timeout = 60_000;
        }

        public string WkHtmlToPdfPath { get; set; }

        public int Timeout { get; set; }
    }
}