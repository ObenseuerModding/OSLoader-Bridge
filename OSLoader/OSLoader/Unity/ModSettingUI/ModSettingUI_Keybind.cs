using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OSLoader
{
    internal class ModSettingUI_Keybind : ModSettingUI_Interactable<KeyCode, KeybindSettingAttribute>
    {
        public Toggle toggle;
        public TMP_Text label;

        private bool awaitingInput;

        public override void OnInitialize(ModSettingDrawer settingDrawer)
        {
            base.OnInitialize(settingDrawer);
            toggle.onValueChanged.AddListener(OnValueChanged);
            OnceEnabled();
        }

        private void OnValueChanged(bool newValue)
        {
            awaitingInput = true;
            label.text = "...";
        }

        private void OnGUI()
        {
            if (awaitingInput && Event.current.isKey)
            {
                Debug.Log(Event.current.ToString());
                if (attribute.specificDissalowedKeys.Contains(Event.current.keyCode))
                {
                    return;
                }

                if (attribute.constraints.HasFlag(KeybindConstraints.NoFunctions) && Event.current.functionKey)
                {
                    return;
                }

                if (attribute.constraints.HasFlag(KeybindConstraints.NoExistingGameBinds) && IsExistingGameBind(Event.current))
                {
                    return;
                }

                if (attribute.constraints.HasFlag(KeybindConstraints.NoExistingModBinds) && IsExistingModBind(Event.current))
                {
                    return;
                }

                if (attribute.constraints.HasFlag(KeybindConstraints.NoDefaultSteamBinds) && IsExistingDefaultSteamBind(Event.current))
                {
                    return;
                }
            
                if (Event.current.keyCode != KeyCode.Return)
                {
                    localValue = Event.current.keyCode;
                }

                toggle.isOn = true;
                label.text = localValue.ToString();
                awaitingInput = false;
            }
        }

        private bool IsExistingGameBind(Event e)
        {
            return false;
        }

        private bool IsExistingModBind(Event e)
        {
            return false;
        }

        private bool IsExistingDefaultSteamBind(Event e)
        {
            return e.keyCode == KeyCode.Tab && e.modifiers.HasFlag(EventModifiers.Shift);
        }

        protected override void OnceEnabled()
        {
            localValue = (KeyCode)linkedField.GetValue(modEntryUI.mod.modComponent.settings);
            label.text = localValue.ToString();
        }
    }
}
