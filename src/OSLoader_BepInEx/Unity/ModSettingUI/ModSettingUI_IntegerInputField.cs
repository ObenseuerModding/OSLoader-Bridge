using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OSLoader
{
    internal class ModSettingUI_IntegerInputField : ModSettingUI_Interactable<int, IntegerSettingInputFieldAttribute>
    {
        public TMP_InputField inputField;

        public override void OnInitialize(ModSettingDrawer settingsDrawer)
        {
            base.OnInitialize(settingsDrawer);
            inputField.characterValidation = TMP_InputField.CharacterValidation.Integer;
            inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
            inputField.onSubmit.AddListener(OnValueChanged);
            inputField.onEndEdit.AddListener(OnValueChanged);
            inputField.onDeselect.AddListener(OnValueChanged);

            OnceEnabled();
        }

        private void OnValueChanged(string newValue)
        {
            int iNewValue = int.Parse(newValue);

            if (attribute.step != 0)
            {
                iNewValue = iNewValue / attribute.step * attribute.step;
            }

            if (attribute.clamped)
            {
                localValue = Mathf.Clamp(iNewValue, attribute.minValue, attribute.maxValue);
            }
            else
            {
                localValue = iNewValue;
            }

            inputField.text = localValue.ToString();

            OnSettingChanged();
        }

        protected override void OnceEnabled()
        {
            localValue = (int)linkedField.GetValue(modEntryUI.mod.modComponent.settings);
            inputField.text = localValue.ToString();
        }
    }
}
