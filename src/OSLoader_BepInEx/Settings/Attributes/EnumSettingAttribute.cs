using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class EnumSettingAttribute : ModSettingAttribute
    {
        public EnumSettingAttribute(string name) : base(name) { }

        internal override Type GetExpectedType()
        {
            return typeof(int);
        }

        internal override GameObject GetObjectToDraw()
        {
            return Loader.Instance.prefabs.enumSetting;
        }
    }
}
