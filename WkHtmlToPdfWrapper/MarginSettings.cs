using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WkHtmlToPdfWrapper
{
    /// <summary>
    /// Set page margin (default is left and right 10mm)
    /// </summary>
    public class MarginSettings
    {
        public MarginSettings()
        {
            Unit = Units.Millimeters;
        }

        public MarginSettings(double top, double right, double bottom, double left) : this()
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

        [WkHtmlToPdfParam("margin-bottom")]
        public double? Bottom { get; set; }

        [WkHtmlToPdfParam("margin-bottom")]
        public double? Left { get; set; }

        [WkHtmlToPdfParam("margin-bottom")]
        public double? Right { get; set; }

        [WkHtmlToPdfParam("margin-bottom")]
        public double? Top { get; set; }

        [Browsable(false)]
        public double All
        {
            set => Top = Right = Bottom = Left = value;
        }

        /// <summary>
        /// Defaults to Millimeters.
        /// </summary>
        public string Unit { get; set; }

        public override string ToString()
        {
            var paramBuilder = new StringBuilder();
            var properties = GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => p.CanRead);

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(true);
                var propertyValue = property.GetValue(this);

                if (propertyValue == null)
                {
                    continue;
                }

                if (attributes.Length > 0)
                {
                    if (attributes[0] is WkHtmlToPdfParamAttribute)
                    {
                        var attribute = attributes[0] as WkHtmlToPdfParamAttribute;

                        if (propertyValue is double doubleValue)
                        {
                            PdfDocument.ApplyConfig(paramBuilder, attribute.Name, doubleValue.ToString("0.##", CultureInfo.InvariantCulture) + Unit);
                        }
                        else
                        {
                            PdfDocument.ApplyConfig(paramBuilder, attribute.Name, propertyValue);
                        }
                    }
                    else if (attributes[0] is WkHtmlToPdfNestedParamAttribute)
                    {
                        PdfDocument.GenerateAutoCommandLineParameters(paramBuilder, propertyValue);
                    }
                }
            }

            return paramBuilder.ToString();
        }
    }
}