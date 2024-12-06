using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class IntegerSettingSliderAttribute : ModSettingAttribute
    {
        internal int minValue;
        internal int maxValue;
        internal int step;
        internal bool smooth;

        public IntegerSettingSliderAttribute(string name, int minValue, int maxValue, int step = 1, bool smooth = false) : base(name)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.step = step;
            this.smooth = smooth;
        }

        internal override Type GetExpectedType()
        {
            return typeof(int);
        }

        internal override GameObject GetObjectToDraw()
        {
            return Loader.Instance.prefabs.intSliderSetting;
        }
    }
}
