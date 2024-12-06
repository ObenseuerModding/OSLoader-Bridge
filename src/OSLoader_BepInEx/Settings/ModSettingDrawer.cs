using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    public class ModSettingDrawer
    {
        public GameObject objectToDraw;
        public FieldInfo relatedField;
        public Attribute relatedAttribute;
        public CallbackAttribute[] callbackAttributes;

        public ModSettingDrawer(GameObject objectToDraw, FieldInfo relatedField, Attribute relatedAttribute, CallbackAttribute[] callbackAttributes)
        {
            this.objectToDraw = objectToDraw;
            this.relatedField = relatedField;
            this.relatedAttribute = relatedAttribute;
            this.callbackAttributes = callbackAttributes;
        }
    }
}
