using System;
using System.Collections.Generic;

namespace Excalibur.Cross.Attributes
{
    public class PropertyChangedDependentOnAttribute : Attribute
    {
        public IList<string> DependentOnPropertyNames { get; private set; }

        public PropertyChangedDependentOnAttribute(params string[] args)
        {
            DependentOnPropertyNames = args;
        }
    }
}
