using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WkHtmlToPdfWrapper
{
    public class PdfDocument
    {
        public string Html { get; set; }

        /// <summary>
        /// Default is <see cref="PageSize"/>
        /// </summary>
        [WkHtmlToPdfParam("page-size")]
        public PageSize PageSize { get; set; } = PageSize.A4;

        /// <summary>
        /// Path to the HTML header file
        /// </summary>
        [WkHtmlToPdfParam("header-html")]
        public string HeaderUri { get; set; }

        /// <summary>
        /// Spacing between header and content in mm
        /// <br />
        /// Default is 0mm
        /// </summary>
        [WkHtmlToPdfParam("header-spacing")]
        public int HeaderSpacing { get; set; } = 0;

        /// <summary>
        /// Path to the HTML footer file
        /// </summary>
        [WkHtmlToPdfParam("footer-html")]
        public string FooterUri { get; set; }

        /// <summary>
        /// Spacing between footer and content in mm
        /// <br />
        /// Default is 0mm
        /// </summary>
        [WkHtmlToPdfParam("footer-spacing")]
        public int FooterSpacing { get; set; } = 0;

        /// <summary>
        /// Left aligned header text
        /// </summary>
        [WkHtmlToPdfParam("header-left")]
        public string HeaderLeft { get; set; }

        /// <summary>
        /// Center aligned header text
        /// </summary>
        [WkHtmlToPdfParam("header-center")]
        public string HeaderCenter { get; set; }

        /// <summary>
        /// Right aligned header text
        /// </summary>
        [WkHtmlToPdfParam("header-right")]
        public string HeaderRight { get; set; }

        /// <summary>
        /// Left aligned footer text
        /// </summary>
        [WkHtmlToPdfParam("footer-left")]
        public string FooterLeft { get; set; }

        /// <summary>
        /// Center aligned footer text
        /// </summary>
        [WkHtmlToPdfParam("footer-center")]
        public string FooterCenter { get; set; }

        /// <summary>
        /// Right aligned footer text
        /// </summary>
        [WkHtmlToPdfParam("footer-right")]
        public string FooterRight { get; set; }

        /// <summary>
        /// Set an additional cookie(repeatable), value should be url encoded.
        /// </summary>
        [WkHtmlToPdfParam("cookie")]
        public Dictionary<string, string> Cookies { get; set; }

        /// <summary>
        /// Default is 12
        /// </summary>
        [WkHtmlToPdfParam("header-font-size")]
        public int HeaderFontSize { get; set; } = 12;

        /// <summary>
        /// Default is 12
        /// </summary>
        [WkHtmlToPdfParam("footer-font-size")]
        public int FooterFontSize { get; set; } = 12;

        [WkHtmlToPdfParam("header-font-name")]
        public string HeaderFontName { get; set; } = "Arial";

        /// <summary>
        /// Set header font name (default Arial)
        /// </summary>
        [WkHtmlToPdfParam("footer-font-name")]
        public string FooterFontName { get; set; } = "Arial";

        /// <summary>
        /// Use print media-type instead of screen (default: false)
        /// </summary>
        [WkHtmlToPdfParam("print-media-type")]
        public bool UsePrintMediaType { get; set; }

        /// <summary>
        /// Use this SVG file when rendering unchecked checkboxes
        /// </summary>
        [WkHtmlToPdfParam("checkbox-svg")]
        public string CheckboxUncheckedSvgPath { get; set; }

        /// <summary>
        /// Use this SVG file when rendering checked checkboxes
        /// </summary>
        [WkHtmlToPdfParam("checkbox-checked-svg")]
        public string CheckboxCheckedSvgPath { get; set; }

        /// <summary>
        /// Sets the page orientation (default: Portrait)
        /// </summary>
        [WkHtmlToPdfParam("orientation")]
        public Orientation? PageOrientation { get; set; } = Orientation.Portrait;

        /// <summary>
        /// Outline options
        /// </summary>
        [WkHtmlToPdfNestedParam]
        public OutlineSettings OutlineSettings { get; set; }

        [WkHtmlToPdfNestedParam]
        public MarginSettings Margin { get; set; }

        //public string GenerateCommandLineParams()
        //{
        //    var paramsBuilder = new StringBuilder();

        //    paramsBuilder.AppendFormat("--page-size {0} ", PageSize);

        //    if (!string.IsNullOrEmpty(HeaderUri))
        //    {
        //        paramsBuilder.AppendFormat("--header-html {0} ", HeaderUri);
        //        Margin.Top = 25;
        //        HeaderSpacing = 5;
        //    }
        //    if (!string.IsNullOrEmpty(FooterUri))
        //    {
        //        paramsBuilder.AppendFormat("--footer-html {0} ", FooterUri);
        //        Margin.Bottom = 25;
        //        FooterSpacing = 5;
        //    }
        //    if (!string.IsNullOrEmpty(HeaderLeft))
        //        paramsBuilder.AppendFormat("--header-left \"{0}\" ", HeaderLeft);

        //    if (!string.IsNullOrEmpty(HeaderCenter))
        //        paramsBuilder.AppendFormat("--header-center \"{0}\" ", HeaderCenter);

        //    if (!string.IsNullOrEmpty(HeaderRight))
        //        paramsBuilder.AppendFormat("--header-right \"{0}\" ", HeaderRight);

        //    if (!string.IsNullOrEmpty(FooterLeft))
        //        paramsBuilder.AppendFormat("--footer-left \"{0}\" ", FooterLeft);

        //    if (!string.IsNullOrEmpty(FooterCenter))
        //        paramsBuilder.AppendFormat("--footer-center \"{0}\" ", FooterCenter);

        //    if (!string.IsNullOrEmpty(FooterRight))
        //        paramsBuilder.AppendFormat("--footer-right \"{0}\" ", FooterRight);

        //    //if (!string.IsNullOrEmpty(HeaderFontSize))
        //    //    paramsBuilder.AppendFormat("--header-font-size \"{0}\" ", HeaderFontSize);

        //    //if (!string.IsNullOrEmpty(FooterFontSize))
        //    //    paramsBuilder.AppendFormat("--footer-font-size \"{0}\" ", FooterFontSize);

        //    if (!string.IsNullOrEmpty(HeaderFontName))
        //        paramsBuilder.AppendFormat("--header-font-name \"{0}\" ", HeaderFontName);

        //    if (!string.IsNullOrEmpty(FooterFontName))
        //        paramsBuilder.AppendFormat("--footer-font-name \"{0}\" ", FooterFontName);

        //    if (UsePrintMediaType)
        //        paramsBuilder.Append("--print-media-type ");

        //    if (!string.IsNullOrEmpty(CheckboxUncheckedSvgPath))
        //        paramsBuilder.AppendFormat("--checkbox-svg \"{0}\" ", CheckboxUncheckedSvgPath);

        //    if (Margin != null)
        //    {
        //        if (Margin.Left != null)
        //        {
        //            paramsBuilder.AppendFormat("-L \"{0}\" ", Margin.Left);
        //        }

        //        if (Margin.Top != null)
        //        {
        //            paramsBuilder.AppendFormat("-T \"{0}\" ", Margin.Top);
        //        }

        //        if (Margin.Right != null)
        //        {
        //            paramsBuilder.AppendFormat("-R \"{0}\" ", Margin.Right);
        //        }

        //        if (Margin.Bottom != null)
        //        {
        //            paramsBuilder.AppendFormat("-B \"{0}\" ", Margin.Bottom);
        //        }
        //    }

        //    if (ExtraParams != null)
        //        foreach (var extraParam in ExtraParams)
        //            paramsBuilder.AppendFormat("--{0} {1} ", extraParam.Key, extraParam.Value);

        //    if (Cookies != null)
        //        foreach (var cookie in Cookies)
        //            paramsBuilder.AppendFormat("--cookie {0} {1} ", cookie.Key, cookie.Value);

        //    return paramsBuilder.ToString();
        //}

        public string GetCommandLineParameters()
        {
            return GenerateAutoCommandLineParameters(new StringBuilder(), this);
        }

        internal static string GenerateAutoCommandLineParameters(StringBuilder paramBuilder, object configObject)
        {
            if (configObject == null)
                return string.Empty;

            var properties = configObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(p => p.CanRead);

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(true);
                var propertyValue = property.GetValue(configObject);

                if (propertyValue == null || attributes.Length <= 0)
                {
                    continue;
                }

                switch (attributes[0])
                {
                    case WkHtmlToPdfParamAttribute _:
                    {
                        var attribute = attributes[0] as WkHtmlToPdfParamAttribute;

                        ApplyConfig(paramBuilder, attribute.Name, propertyValue);
                        break;
                    }
                    case WkHtmlToPdfNestedParamAttribute _:
                        GenerateAutoCommandLineParameters(paramBuilder, propertyValue);
                        break;
                }
            }

            return paramBuilder.ToString();

        }

        internal static void ApplyConfig(StringBuilder paramBuilder, string parameterName, object value)
        {
            switch (value)
            {
                case double d:
                    paramBuilder.Append($"--{parameterName} {d.ToString("0.##", CultureInfo.InvariantCulture)} ");
                    break;
                case bool b:
                    if (b)
                        paramBuilder.Append($"--{parameterName} ");
                    break;
                case Dictionary<string, string> dictionary:
                    foreach (var d in dictionary)
                    {
                        paramBuilder.Append($"--{parameterName} \"{d.Key}\" \"{d.Value}\" ");
                    }
                    break;
                default:
                    paramBuilder.Append($"--{parameterName} \"{value}\" ");
                    break;
            }
        }
    }
}