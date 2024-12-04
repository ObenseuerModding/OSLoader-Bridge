# Info.json

## Description
Each mod comes with an `info.json` file in its root to determine some basic variables related to the mod.

## Properties
Property | Type | Description
-- | -
`modType` | `integer` (Internally, `ModType` enum) | What kind of mod this is, so the modloader knows how to load it. See `ModType` for possible values.
`name` | `string` | Name of your mod, appears in the modloader's UI for loading and unloading the mod.
`description` | `string` | Description of your mod, currently unused.
`loadOnStart` | `bool` | If the mod should be loaded by default without user input
`repositoryUrl` | `string` | A link to the mod's repository. If the link is not a `github.com` or `gitlab.com` link, it will not be shown to the player for safety reasons.
`modUrl` | `string` | A link to the mod's download page. Currently unused.
`version` | `string` (`x.y.z`) | A SemVer compliant version (only having the major, minor and patch numbers is supported) you should increment each time you release your mod.
`modId` | `string` | A unique identifier for your mod to be referenced by other mods and Harmony. I recommend we all use the [Reverse Domain Name](https://en.wikipedia.org/wiki/Reverse_domain_name_notation) notation (e.g `org.app`), such that a mod that loads custom radio songs made by myself (Ierdna100) is called `ierdna.custom-radio-songs`. A more detailed explanation for the Java environnement as an example can be found [here](https://docs.oracle.com/javase/tutorial/java/package/namingpkgs.html).
`author` | `string` | Name of the author. Currently unused.
`dllFilepath` | `filepath` | Relative filepath to the assembly containing a class inheriting the `Mod` entrypoint class. If the assembly was called `RadioMod.dll` and was found in the same folder as `info.json`, it would be for example `"RadioMod.dll"`.
`loaderVersion` | `string` (`x.y.z`) | Minimum version for the modloader required to load this mod. Currently unused.
`gameVersion` | `string` (`x.y.z`) | Minimum game version for Obenseuer required to load this mod. Currently unused.
`dependencies` | `string[]` | Array of strings containg `modId`s with other mods required to be loaded before this one, as the mod depends on them in some form or another. Circular dependencies will result in both mods being loaded, with the first one appearing in the `mods` folder having `InitializeMod()` and `OnModLoaded()` called first. You should set `modType` to `2` (`ModType.RequiresDependency`) if the array is not empty, but this will not actually affect anything, it is simply a marker for UI and such.

## Example
```json
{
    "modType": 2, // Mod requiring dependency
    "name": "Radio Mod",
    "description": "A mod that loads custom music into the game's radio appartment item.",
    "loadOnStart": false,
    "repositoryUrl": "https://github.com/Ierdna100/OSLoader",
    "modUrl": "https://github.com/Ierdna100/OSLoader/releases/tag/0.2.0-beta",
    "version": "1.2.0",
    "modId": "ierdna.radio-mod",
    "author": "ierdna100",
    "dllFilepath": "RadioMod.dll",
    "loaderVersion": "0.2.0",
    "gameVersion": "0.3.0",
    "dependencies": [
        "ierdna.custom-sounds"
    ]
}
```
