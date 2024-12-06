using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FloatSettingInputFieldAttribute : ModSettingAttribute
    {
        internal bool clamped;
        internal float minValue;
        internal float maxValue;
        internal float step;

        public FloatSettingInputFieldAttribute(string name, float step = 0.0f) : base(name) 
        {
            this.step = step;
        }

        public FloatSettingInputFieldAttribute(string name, float minValue, float maxValue, float step = 0.0f) : base(name)
        {
            clamped = true;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.step = step;
        }

        internal override Type GetExpectedType()
        {
            return typeof(float);
        }

        internal override GameObject GetObjectToDraw()
        {
            return Loader.Instance.prefabs.floatSetting;
        }
    }
}
