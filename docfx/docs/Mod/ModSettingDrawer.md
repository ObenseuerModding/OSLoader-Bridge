# ModSettingDrawer Class

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: ModSettingDrawer.cs  

## Description
The `ModSettingDrawer` class represents data about a certain setting created by the setting attributes in the `ModSettings` class.
This class is exposed so you can draw your own settings if you so desire.

## Properties
Property | Description
-- | -
`GameObject objectToDraw` | The `GameObject` which is drawn by default in the default settings system. This is directly loaded from the `loader` assetbundle.
`FieldInfo relatedField` | The field which this `ModSettingDrawer` represents. This is the field you should ultimately get and set.
`Attribute relatedAttribute` | The attribute which was on top of the field and which contains related metadata to said field, such as the minimum and maximum values of a field.
`CallbackAttribute[] callbackAttributes` | The `CallbackAttribute` which you holds a reference to the callback to call if the value of the field changes.