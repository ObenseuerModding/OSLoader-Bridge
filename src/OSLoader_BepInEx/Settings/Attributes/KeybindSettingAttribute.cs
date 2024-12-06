using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class KeybindSettingAttribute : ModSettingAttribute
    {
        internal KeybindConstraints constraints;
        internal KeyCode[] specificDissalowedKeys;

        public KeybindSettingAttribute(string name, KeybindConstraints keybindConstraints = KeybindConstraints.Default, KeyCode[] dissalowedKeys = null) : base(name)
        {
            constraints = keybindConstraints;
            specificDissalowedKeys = dissalowedKeys;
        }

        internal override Type GetExpectedType()
        {
            return typeof(KeyCode);
        }

        internal override GameObject GetObjectToDraw()
        {
            return Loader.Instance.prefabs.keybindSetting;
        }
    }
}
