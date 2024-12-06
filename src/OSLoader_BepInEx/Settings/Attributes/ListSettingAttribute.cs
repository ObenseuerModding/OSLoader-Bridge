using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ListSettingAttribute : ModSettingAttribute
    {
        internal bool userExpandable;

        public ListSettingAttribute(string name, bool userExpandable = false) : base(name)
        {
            this.userExpandable = userExpandable;
        }

        internal override Type GetExpectedType()
        {
            return null;
        }

        internal override GameObject GetObjectToDraw()
        {
            return null;
        }
    }
}
