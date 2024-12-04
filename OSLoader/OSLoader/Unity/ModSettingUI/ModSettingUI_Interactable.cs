using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using UnityEngine;
using TMPro;

namespace OSLoader
{
    internal abstract class ModSettingUI_Interactable<T, T2> : ModSettingUI_Base where T2 : ModSettingAttribute
    {
        protected bool wasModified = false;
        protected T localValue;

        protected List<Action<T>> callbacks;

        protected FieldInfo linkedField;
        protected T2 attribute;

        public bool initialized = false;

        protected void OnSettingChanged()
        {
            wasModified = true;
            LoaderMainMenu.Instance.OnAnyValueChanged();
        }

        public override void OnSave()
        {
            if (wasModified)
            {
                linkedField.SetValue(modEntryUI.mod.modComponent.settings, localValue);
                callbacks?.ForEach(c => c.Invoke(localValue));
            }
        }

        public override void OnInitialize(ModSettingDrawer relatedDrawer)
        {
            linkedField = relatedDrawer.relatedField;
            attribute = (T2)relatedDrawer.relatedAttribute;

            title.text = attribute.name;
            RegisterCallbacks(relatedDrawer);
            initialized = true;
        }

        private void OnEnable()
        {
            if (!initialized) return;
            OnceEnabled();
        }

        public void RegisterCallbacks(ModSettingDrawer relatedDrawer)
        {
            foreach (CallbackAttribute callbackAttribute in relatedDrawer.callbackAttributes)
            {
                //callbacks.Add(callbackAttribute.type.GetMethod(callbackAttribute.method).CreateDelegate(callbackAttribute.type));
            }
        }

        protected abstract void OnceEnabled();
    }
}
