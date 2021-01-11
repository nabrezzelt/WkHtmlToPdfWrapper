using System;

namespace WkHtmlToPdfWrapper
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class WkHtmlToPdfParamAttribute : Attribute
    {
        public string Name { get; }

        public WkHtmlToPdfParamAttribute(string name)
        {
            Name = name;
        }
    }
}