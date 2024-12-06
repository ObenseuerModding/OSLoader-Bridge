using System;
using System.Collections.Generic;
using System.Text;

namespace OSLoader
{
    // Declaration is OnChanged<T>(string valueName, T newValue)
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ListItemCallbackAttribute : Attribute
    {
        internal Type type;
        internal string method;

        public ListItemCallbackAttribute(Type type, string method = null)
        {
            this.type = type;
            this.method = method;
        }
    }
}
