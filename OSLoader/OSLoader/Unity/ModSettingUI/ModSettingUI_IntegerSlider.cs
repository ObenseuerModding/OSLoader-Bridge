using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OSLoader
{
    internal class ModSettingUI_IntegerSlider : ModSettingUI_Interactable<int, IntegerSettingSliderAttribute>
    {
        public TMP_Text valueDisplay;
        public Slider slider;

        public override void OnInitialize(ModSettingDrawer settingsDrawer)
        {
            base.OnInitialize(settingsDrawer);
            slider.maxValue = attribute.maxValue;
            slider.minValue = attribute.minValue;
            slider.onValueChanged.AddListener(OnValueEntered);

            OnceEnabled();
        }

        private void OnValueEntered(float newValue)
        {
            int iNewValue = (int)newValue;
            int steppedNewValue = iNewValue / attribute.step * attribute.step;
            localValue = steppedNewValue;
            valueDisplay.text = localValue.ToString();
            if (!attribute.smooth)
            {
                slider.value = steppedNewValue;
            }
            OnSettingChanged();
        }

        protected override void OnceEnabled()
        {
            localValue = (int)linkedField.GetValue(modEntryUI.mod.modComponent.settings);
            slider.value = localValue;
            valueDisplay.text = localValue.ToString();
        }

        public override void OnSave()
        {
            linkedField.SetValue(modEntryUI.mod.modComponent.settings, localValue);
        }
    }
}
