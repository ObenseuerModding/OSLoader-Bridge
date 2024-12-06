using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class ModSettingAttribute : Attribute
    {
        internal string name;

        public ModSettingAttribute(string name)
        {
            this.name = name;
        }

        internal abstract Type GetExpectedType();

        internal abstract GameObject GetObjectToDraw();
    }
}
