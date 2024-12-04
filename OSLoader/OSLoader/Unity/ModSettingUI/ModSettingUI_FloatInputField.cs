using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OSLoader
{
    internal class ModSettingUI_FloatInputField : ModSettingUI_Interactable<float, FloatSettingInputFieldAttribute>
    {
        public TMP_InputField inputField;

        public override void OnInitialize(ModSettingDrawer settingDrawer)
        {
            base.OnInitialize(settingDrawer);
            inputField.characterValidation = TMP_InputField.CharacterValidation.Decimal;
            inputField.contentType = TMP_InputField.ContentType.DecimalNumber;
            inputField.onSubmit.AddListener(OnValueChanged);
            inputField.onEndEdit.AddListener(OnValueChanged);
            inputField.onDeselect.AddListener(OnValueChanged);
            OnceEnabled();
        }

        protected override void OnceEnabled()
        {
            localValue = (float)linkedField.GetValue(modEntryUI.mod.modComponent.settings);
            inputField.text = localValue.ToString();
        }

        private void OnValueChanged(string newValue)
        {
            float fNewValue = float.Parse(newValue);

            if (attribute.step != 0)
            {
                fNewValue = Mathf.Floor(fNewValue / attribute.step) * attribute.step;
            }

            if (attribute.clamped)
            {
                localValue = Mathf.Clamp(fNewValue, attribute.minValue, attribute.maxValue);
            }
            else
            {
                localValue = fNewValue;
            }

            inputField.text = localValue.ToString();

            OnSettingChanged();
        }
    }
}
