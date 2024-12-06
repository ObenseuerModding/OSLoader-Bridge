using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine.UI;

namespace OSLoader
{
    internal class ModSettingUI_Bool : ModSettingUI_Interactable<bool, BoolSettingAttribute>
    {
        public Toggle toggle;

        public override void OnInitialize(ModSettingDrawer settingDrawer)
        {
            base.OnInitialize(settingDrawer);
            toggle.onValueChanged.AddListener(OnValueChanged);
            OnceEnabled();
        }

        protected override void OnceEnabled()
        {
            localValue = (bool)linkedField.GetValue(modEntryUI.mod.modComponent.settings);
        }

        private void OnValueChanged(bool newValue)
        {
            localValue = newValue;
            OnSettingChanged();
        }
    }
}
