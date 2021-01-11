using System;

namespace WkHtmlToPdfWrapper
{
    public class PdfConvertException : Exception
    {
        public PdfConvertException(string msg) : base(msg) { }
    }
}