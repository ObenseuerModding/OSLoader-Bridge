using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine.UI;

namespace OSLoader
{
    internal class ModSettingUI_Enumeration : ModSettingUI_Interactable<int, EnumSettingAttribute>
    {
        public TMP_Dropdown dropdown;

        public override void OnInitialize(ModSettingDrawer settingDrawer)
        {
            base.OnInitialize(settingDrawer);
            dropdown.AddOptions(Enum.GetNames(linkedField.FieldType).ToList());
            dropdown.onValueChanged.AddListener(OnValueChanged);
            OnceEnabled();
        }

        protected override void OnceEnabled()
        {
            localValue = (int)linkedField.GetValue(modEntryUI.mod.modComponent.settings);
            dropdown.value = localValue;
            dropdown.RefreshShownValue();
        }

        private void OnValueChanged(int newValue)
        {
            localValue = newValue;
            OnSettingChanged();
        }
    }
}
