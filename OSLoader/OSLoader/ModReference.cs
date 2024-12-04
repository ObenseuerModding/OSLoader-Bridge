using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace OSLoader
{
    internal class ModReference
    {
        public string modFilepath;
        public bool valid = false;

        public ModInfo info;
        public bool loaded = false;

        public Mod modComponent;

        public Action generateUISettings;

        public ModReference(string filepath)
        {
            modFilepath = filepath;
            string infoFilepath = Path.Combine(filepath, Loader.modsInfoFilename);
            if (!File.Exists(infoFilepath))
            {
                Loader.Instance.logger.Log($"Unable to create mod reference at path {filepath}: No info file found!");
                return;
            }

            string rawModInfo = File.ReadAllText(infoFilepath);
            Loader.Instance.logger.Detail($"rawModInfo for file {infoFilepath}:");
            Loader.Instance.logger.Detail(rawModInfo);
            info = JsonConvert.DeserializeObject<ModInfo>(rawModInfo);
            info.infoFilepath = infoFilepath;
            string validation = info.Validate();
            if (validation != null)
            {
                Loader.Instance.logger.Log($"Unable to create mod reference at path {filepath}: {validation}");
                return;
            }

            info.settingsFilepath = Path.Combine(filepath, Loader.modsSettingsFilename);
            valid = true;
        }

        public void Load(bool isCalledByLoadOnStart = false)
        {
            if (!valid)
            {
                Loader.Instance.logger.Log($"Failed to load mod {info.name}: Valid flag is not set!");
                return;
            }

            if (loaded)
            {
                Loader.Instance.logger.Log($"Not loading mod {info.name}: Mod is already loaded!");
                return;
            }

            string assemblyFilepath = Path.Combine(modFilepath, info.dllFilepath);
            if (!File.Exists(assemblyFilepath))
            {
                Loader.Instance.logger.Log($"Not loading mod {info.name}: No DLL found at specified filepath!");
            }

            Loader.Instance.logger.Detail("Assembly filepath: ");
            Loader.Instance.logger.Detail(modFilepath);
            Loader.Instance.logger.Detail(info.dllFilepath);
            Loader.Instance.logger.Detail(assemblyFilepath);

            Assembly assembly;
            try
            {
                assembly = Assembly.LoadFrom(assemblyFilepath);
            } 
            catch (FileNotFoundException)
            {
                Loader.Instance.logger.Log($"Assembly filepath '{assemblyFilepath}' does not exist! Not loading mod.");
                return;
            }
            
            var entrypoint = from type in assembly.GetTypes()
                             where type.IsSubclassOf(typeof(Mod))
                             select type;

            if (entrypoint == null || entrypoint.Count() != 1)
            {
                Loader.Instance.logger.Log($"Failed to load mod {info.name}: Mod structure is invalid");
                return;
            }

            GameObject modGO = new GameObject(info.name, entrypoint.First());
            GameObject.DontDestroyOnLoad(modGO);
            modComponent = modGO.GetComponent<Mod>();
            modComponent.info = info;

            modComponent.InitializeMod();
            if (modComponent.HasValidSettings())
            {
                if (!File.Exists(info.settingsFilepath))
                {
                    modComponent.SaveSettings();
                }
                else
                {
                    modComponent.settings = (ModSettings)JsonConvert.DeserializeObject(File.ReadAllText(info.settingsFilepath), modComponent.settings.GetType());
                }
            }
            generateUISettings?.Invoke();

            modComponent.OnModInitialized();
            loaded = true;

            if (isCalledByLoadOnStart)
            {
                int modsLoaded = Loader.Instance.mods.Where(e => e.loaded).Count();
                int modsToLoad = Loader.Instance.mods.Where(e => e.info.loadOnStart).Count();
                int percentage = (int)(100f * modsLoaded / modsToLoad);
                Loader.Instance.logger.Log($"Loaded mod {info.name} ({modsLoaded}/{modsToLoad}) ({percentage}%)");
            }
        }
    }
}
