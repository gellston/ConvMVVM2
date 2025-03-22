using System;
using System.Collections.Generic;
using System.Text;

namespace ConvMVVM2.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public class PropertyChangedForAttribute : Attribute
    {

        public PropertyChangedForAttribute(string commandName)
        {
            CommandNames = new[] { commandName };
        }

        public string[] CommandNames { get; }
    }
}
