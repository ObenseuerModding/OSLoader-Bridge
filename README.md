Link to documentation because Github doesnt put them up automatically anymore:

https://ierdna100.github.io/OSLoader/

# OSLoader
Mod loader for Obenseuer based on Unity Doorstop

## Projects present in the repository
### Builder
Typescript/NodeJS project that builds everything else listed below in a ship-able ZIP file.

### Installer
`.exe` project that allows the user to install the modloader with 1 click.

### OSLoader
Loads mods found in the `.../Obenseuer/OSLoader/mods/` folder in the game making use of Doorstop. 

### ProjectReferences
Unity project. The normal project is compiled in order to access its assemblies to replace the game's ones so they are no longer stripped. There is also an assetbundle inside that gets compiled individually, that contains the UI for the modloader.

### TestMod
Testing if mods work. I don't know why you'd want to use this, but it's there. Currently, mods can not access
private/internal variables. This will come with a future version.

