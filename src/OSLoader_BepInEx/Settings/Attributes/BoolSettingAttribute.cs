using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BoolSettingAttribute : ModSettingAttribute
    {
        public BoolSettingAttribute(string name) : base(name) { }

        internal override Type GetExpectedType()
        {
            return typeof(bool);
        }

        internal override GameObject GetObjectToDraw()
        {
            return Loader.Instance.prefabs.boolSetting;
        }
    }
}
