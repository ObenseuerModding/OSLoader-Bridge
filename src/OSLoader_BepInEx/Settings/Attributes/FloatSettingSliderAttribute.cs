using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FloatSettingSliderAttribute : ModSettingAttribute
    {
        internal float minValue;
        internal float maxValue;
        internal float step;
        internal bool smooth;

        public FloatSettingSliderAttribute(string name, float minValue, float maxValue, float step = 0.0f, bool smooth = false) : base(name)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.step = step;
            this.smooth = smooth;
        }

        internal override Type GetExpectedType()
        {
            return typeof(float);
        }

        internal override GameObject GetObjectToDraw()
        {
            return Loader.Instance.prefabs.floatSliderSetting;
        }
    }
}
