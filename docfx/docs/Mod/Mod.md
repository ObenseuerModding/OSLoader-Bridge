# Mod Class

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: Mod.cs  
Inherits [MonoBehaviour](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html)

## Description
The mod class is the class in a mod's assembly that acts as the entrypoint, as a sort of "Program.Main". This class is to be inherited and cannot be constructed, as it is abstract. It has special functionality attached to it, as detailed below. The assembly containing your class inheriting `Mod` must be the one you reference in your `info.json`'s `dllFilepath` property. The very contents of the `info.json` can also be accessed via the `info` property, as detailed below.

The [`settings`]() member also has special functionality, as detailed in its page.

This class also inherits MonoBehaviour, and exists in `DontDestroyOnLoad` at all times, as long as the mod is loaded. If the mod is not loaded on start via its `info.json` property `loadOnStart`, your code will only execute when the user clicks on the `load` button in OSLoader's mod manager menu.

You are expected to implement the functionality of `InitializeMod()`, `UnloadMod()` and `OnModInitialized()`. It is not recommended to run code in `Start()` and
`Awake()`, as the modloader needs to run code after these to setup your mod. You should therefore run all of non-initialization code (that is to say, all code
not related to mod settings, mod info or global variables related to the mod class like a `static instance`) in `OnModInitialized()`/

> [!CAUTION]
> In the future, the modloader will automatically handle Harmony patching in order to allow properly unloading mods. Currently, that is not implemented.
> Instead, you should patch with Harmony yourself, do not bother cleaning up as it is very complex for little gain, and mod unloading is not truly
> supported yet.

## Usage
[!code-csharp[](../Code Examples/Mod.cs)]

## Methods
Method | Description
-- | -
`void InitializeMod` | Method called when the modloader is requested to load your mod. Create your settings in this method.
`void OnModInitialized` | Method called when modloader is done initializing. Run all other mod related code here.
`bool UnloadMod` | Method called when the modloader is requested to unload your mod. This method is currently not used, will be implemented soon.
`void SaveSettings` | Method called whenever you wish to save settings, if modified programmatically. Note that calling this in `InitializeMod` will set the settings to their default values.
`bool HasSettings` | Returns true if the mod has settings
`bool HasValidSettings` | Returns true if the mod has no invalid settings, such as a setting with a mismatched attribute attached to it

## Properties
Property | Description
-- | -
`ModInfo info` | Mod's info as present in the `info.json` file
`ModSettings settings` | Mod settings as present in the `settings.json` file