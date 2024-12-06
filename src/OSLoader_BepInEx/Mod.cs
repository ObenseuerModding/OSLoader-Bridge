using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using System.Reflection;

namespace OSLoader
{
    public abstract class Mod : MonoBehaviour
    {
        public ModInfo info;
        public ModSettings settings;

        /// <summary>
        /// Method called when the modloader requests to load the mod.
        /// </summary>
        public abstract void InitializeMod();

        /// <summary>
        /// Method called when the modloader has finished setting up and loading the mod. You can now execute any code here.
        /// </summary>
        public virtual void OnModInitialized() { }

        /// <summary>
        /// <para>
        /// Method called when the modloader requests to unload the mod. The modloader will cleanup any code patches you have, but 
        /// is unable to know what objects you have left in the scene or other modifications you have done. Please clean up anything you can
        /// in relation to objects, components and non-Harmony related code.
        /// </para>
        /// <para>
        /// If this method is unable to fully clean up everything it leaves behind, the return value should be false. The modloader will still
        /// consider the mod cleaned up, but the user will not be able to load the mod again and will be presented with an information message
        /// instructing them to restart the game if they wish to load the mod again. Returning true will simply allow the user to load it again.
        /// </para>
        /// <para>
        /// You should return false if you cannot unload certain objects, if you're not planning on allowing proper unloading at all,
        /// if your code has altered certain components which cannot be reverted, or similar situations. You should still always attempt to
        /// try to be able to unload mods, for consistency.
        /// </para>
        /// </summary>
        /// <returns>
        /// A boolean which is either true if the mod has successfully unloaded everything related to itself, or false if 
        /// it was unable to or cannot.
        /// </returns>
        public abstract bool UnloadMod();

        public void SaveSettings()
        {
            if (!HasValidSettings()) return;
            File.WriteAllText(info.settingsFilepath, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }
        
        public bool HasSettings()
        {
            return settings != null;
        }

        public bool HasValidSettings()
        {
            return settings?.SettingDrawers != null;
        }
    }
}

