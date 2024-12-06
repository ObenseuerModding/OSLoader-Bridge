using System;
using System.Collections.Generic;
using System.Text;

namespace OSLoader
{

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SettingsHeaderAttribute : Attribute
    {
        internal string name;

        public SettingsHeaderAttribute(string name)
        {
            this.name = name;
        }
    }
}
