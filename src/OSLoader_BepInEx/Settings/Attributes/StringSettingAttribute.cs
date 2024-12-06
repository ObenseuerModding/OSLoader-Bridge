using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class StringSettingAttribute : ModSettingAttribute
    {
        internal StringConstraints constraints;
        internal int maxLength;

        // DO NOT set the default to int.MaxValue! It will overflow because TMP uses an int to represent the max length, but the uint is nice because it prevents
        // negative values. DO NOT "FIX IT"!
        public StringSettingAttribute(string name, StringConstraints stringConstraints = StringConstraints.None, uint maxLength = int.MaxValue) : base(name)
        {
            constraints = stringConstraints;
            this.maxLength = (int)maxLength;
        }

        internal override Type GetExpectedType()
        {
            return typeof(string);
        }

        internal override GameObject GetObjectToDraw()
        {
            return Loader.Instance.prefabs.stringSetting;
        }
    }
}
