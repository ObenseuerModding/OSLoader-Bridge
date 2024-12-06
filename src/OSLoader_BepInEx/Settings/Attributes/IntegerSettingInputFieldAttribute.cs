using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class IntegerSettingInputFieldAttribute : ModSettingAttribute
    {
        internal bool clamped;
        internal int minValue;
        internal int maxValue;
        internal int step;

        public IntegerSettingInputFieldAttribute(string name, int step = 0) : base(name) 
        {
            this.step = step;
        }

        public IntegerSettingInputFieldAttribute(string name, int minValue, int maxValue, int step = 0) : base(name)
        {
            clamped = true;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.step = step;
        }

        internal override Type GetExpectedType()
        {
            return typeof(int);
        }

        internal override GameObject GetObjectToDraw()
        {
            return Loader.Instance.prefabs.intSetting;
        }
    }
}
