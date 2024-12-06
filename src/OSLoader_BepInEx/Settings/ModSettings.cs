using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace OSLoader
{
    public abstract class ModSettings
    {
        [JsonIgnore]
        public List<ModSettingDrawer> SettingDrawers { get; internal set; } = new List<ModSettingDrawer>();

        public ModSettings()
        {
            foreach (FieldInfo fieldInfo in GetType().GetFields())
            {
                if (!ParseAttributesForField(fieldInfo))
                {
                    SettingDrawers = null;
                    return;
                };
            }
        }

        private bool ParseAttributesForField(FieldInfo fieldInfo)
        {
            Attribute settingAttribute = null;
            GameObject objectToDraw = null;
            FieldInfo settingField = null;
            List<CallbackAttribute> callbackAttributes = new List<CallbackAttribute>();
            foreach (Attribute attribute in fieldInfo.GetCustomAttributes())
            {
                if (attribute is SettingsHeaderAttribute header)
                {
                    SettingDrawers.Add(new ModSettingDrawer(Loader.Instance.prefabs.header, fieldInfo, header, null));
                    continue;
                }

                if (attribute is CallbackAttribute callbackAttribute)
                {
                    string targetMethodName = callbackAttribute.method ?? $"On{fieldInfo.Name[0].ToString().ToUpper()}{fieldInfo.Name.Substring(1)}Changed";

                    MethodInfo callback = callbackAttribute.type.GetMethod(targetMethodName);
                    if (callback == null) 
                    {
                        Loader.Instance.logger.Error($"Callback on class '{callbackAttribute.type}' with method '{callbackAttribute.method}' does not exist! Cannot generate settings.");
                        if (callbackAttribute.method == null)
                        {
                            Loader.Instance.logger.Error($"Are you sure you properly generated the method '{targetMethodName}'? The name was programmatically generated as the method name provided was null.");
                        }
                        return false;
                    }

                    if (!callback.IsStatic)
                    {
                        Loader.Instance.logger.Error($"Callback on class '{callbackAttribute.type}' with method '{callbackAttribute.method}' is not static! Cannot generate settings.");
                        return false;
                    }

                    if (callback.GetParameters().Length != 1)
                    {
                        Loader.Instance.logger.Error($"Callback on class '{callbackAttribute.type}' with method '{callbackAttribute.method}' has the wrong number of parameters! Cannot generate settings.");
                        return false;
                    }

                    // THIS DOES NOT HANDLE LISTS
                    if (callback.GetParameters()[0].ParameterType != fieldInfo.FieldType)
                    {
                        Loader.Instance.logger.Error($"Callback on class '{callbackAttribute.type}' with method '{callbackAttribute.method}' parameter mismatch! Cannot generate settings.");
                        return false;
                    }

                    callbackAttributes.Add(callbackAttribute);
                    continue;
                }

                if (!(attribute is ModSettingAttribute modSettingAttribute))
                {
                    Loader.Instance.logger.Error($"Invalid attribute of type '{attribute.GetType()}' on mod settings at field '{fieldInfo.Name}'! Cannot generate settings.");
                    return false;
                }

                if (settingField != null)
                {
                    Loader.Instance.logger.Error($"Too many attributes on mod settings at field '{fieldInfo.Name}'! You can only specify one ModSettingAttribute per field. Cannot generate settings.");
                    return false;
                }

                Type attributeType = modSettingAttribute.GetType();
                if (attributeType == typeof(ListSettingAttribute))
                {
                    // TODO: Special considerations
                }
                else if (attributeType == typeof(EnumSettingAttribute) && !fieldInfo.FieldType.IsEnum)
                {
                    Loader.Instance.logger.Error($"Type mismatch in settings attributes at field '{fieldInfo.Name}'! (type 'enum' does not match expected type '{fieldInfo.FieldType}'). Cannot generate settings.");
                    return false;
                }

                if (fieldInfo.GetValue(this) == null)
                {
                    Loader.Instance.logger.Error($"Default value in settings at field '{fieldInfo.Name}' is null! Cannot generate settings.");
                    return false;
                }

                settingField = fieldInfo;
                objectToDraw = modSettingAttribute.GetObjectToDraw();
                settingAttribute = attribute;
            }

            if (settingField == null)
            {
                Loader.Instance.logger.Error($"No setting attribute found at field '{fieldInfo.Name}'! Cannot generate settings.");
                return false;
            }

            SettingDrawers.Add(new ModSettingDrawer(objectToDraw, settingField, settingAttribute, callbackAttributes.ToArray()));
            return true;
        }
    }
}
