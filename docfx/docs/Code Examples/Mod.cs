using System;
using OSLoader;
using UnityEngine;

namespace YourNamespace
{
    public class YourMod : Mod
    {
        public static YourMod instance;

        private GameObject someObjectYouMake;

        // Initialize only settings here and global variables like YourMod.instance
        public override void InitializeMod()
        {
            instance = this;
            settings = new CustomSettingsExample();
        }

        // You can now run anything in here, modloader has setup your mod
        public override void OnModInitialized()
        {
            Debug.Log("This great mod was loaded!");
            
            // Each instance has a ModSettings attached to it
            settings.yourSetting = false;
            SaveSettings()

            // Each instance has a ModInfo class attached to it
            Debug.Log($"The author of this mod is {info.author}")

            someObjectYouMake = Instantiate(somePrefab);
        }

        public override bool UnloadMod() 
        {
            // Clean up your mod when being unloaded
            Destroy(someObjectYouMake);

            // If unloading doesn't work, return false
            if (someObjectYouMake != null) {
                return false;
            }

            // If it did clean up, return true
            return true;
        }
    }
}
