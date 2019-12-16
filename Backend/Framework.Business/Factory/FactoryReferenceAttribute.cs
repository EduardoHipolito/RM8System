using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Business.Factory
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class FactoryReferenceAttribute : Attribute
    {
        public string TypeName { get; set; }

        public FactoryReferenceAttribute(string typeName)
        {
            this.TypeName = typeName;
        }
    }
}
