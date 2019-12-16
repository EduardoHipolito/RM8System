using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterAttribute : Attribute
    {
        public FilterAttribute(string by)
        {
            By = by;
        }
        public FilterAttribute(bool ignore)
        {
            Ignore = ignore;
        }
        public FilterAttribute(string by, bool ignore)
        {
            Ignore = ignore;
            By = by;
        }

        public string By { get; set; }
        public bool Ignore { get; set; }
    }
}
