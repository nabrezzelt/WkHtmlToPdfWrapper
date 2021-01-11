namespace WkHtmlToPdfWrapper
{
    public class OutlineSettings
    {
        /// <summary>
        /// Do not put an outline into the pdf (default: false)
        /// </summary>
        [WkHtmlToPdfParam("no-outline")]
        public bool NoOutline { get; set; }

        /// <summary>
        /// Set the depth of the outline (default: 4)
        /// </summary>
        [WkHtmlToPdfParam("outline-depth")]
        public int? OutlineDepth { get; set; }

        public OutlineSettings()
        {
            NoOutline = false;
            OutlineDepth = 4;
        }
    }
}