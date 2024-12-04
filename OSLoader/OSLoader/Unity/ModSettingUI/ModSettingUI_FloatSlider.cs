using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OSLoader
{
    internal class ModSettingUI_FloatSlider : ModSettingUI_Interactable<float, FloatSettingSliderAttribute>
    {
        public TMP_Text valueDisplay;
        public Slider slider;

        public override void OnInitialize(ModSettingDrawer settingDrawer)
        {
            base.OnInitialize(settingDrawer);

            slider.value = localValue;
            slider.maxValue = attribute.maxValue;
            slider.minValue = attribute.minValue;
            slider.onValueChanged.AddListener(OnValueEntered);

            OnceEnabled();
        }

        private void OnValueEntered(float newValue)
        {
            float steppedNewValue = Mathf.Floor(newValue / attribute.step) * attribute.step;
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
            localValue = (float)linkedField.GetValue(modEntryUI.mod.modComponent.settings);
            slider.value = localValue;
            valueDisplay.text = localValue.ToString();
        }
    }
}
