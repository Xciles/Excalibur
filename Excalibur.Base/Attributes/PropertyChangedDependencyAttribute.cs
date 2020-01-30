using System;
using System.Collections.Generic;

namespace Excalibur.Base.Attributes
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
