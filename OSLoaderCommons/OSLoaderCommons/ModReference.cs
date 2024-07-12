﻿using System;
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
        public string assemblyFilepath;
        public bool valid = false;

        public ModInfo info;
        public bool loaded = false;

        public Mod actualMod;

        public Action generateUISettings;

        public ModReference(string filepath)
        {
            string[] possibleAssembly = Directory.GetFiles(filepath, "*.dll");
            if (possibleAssembly.Length != 1)
            {
                Loader.Instance.logger.Log($"Unable to create mod reference at path {filepath}: No .dll found!");
                return;
            }

            assemblyFilepath = possibleAssembly[0];

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
            if (!info.IsValid())
            {
                Loader.Instance.logger.Log($"Unable to create mod reference at path {filepath}: Info file check failed!");
                return;
            }

            info.settingsFilepath = Path.Combine(filepath, Loader.modsSettingsFilename);
            valid = true;
        }

        public void Load(bool loadOnStart = false)
        {
            if (!valid)
            {
                Loader.Instance.logger.Log($"Failed to load mod {info.name}: Valid flag is not set");
                return;
            }

            if (loaded)
            {
                Loader.Instance.logger.Log($"Not loading mod {info.name}: Mod is already loaded!");
                return;
            }

            Loader.Instance.logger.Detail("Assembly filepath: ");
            Loader.Instance.logger.Detail(assemblyFilepath);

            Assembly assembly = Assembly.LoadFrom(assemblyFilepath);
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
            actualMod = modGO.GetComponent<Mod>();
            actualMod.info = info;
            actualMod.OnModLoaded();
            generateUISettings?.Invoke();
            if (actualMod.HasValidSettings())
            {
                if (!File.Exists(info.settingsFilepath))
                {
                    actualMod.SaveSettings();
                }
                else
                {
                    actualMod.settings = JsonConvert.DeserializeObject<ModSettings>(File.ReadAllText(info.settingsFilepath));
                }
            }

            loaded = true;

            if (loadOnStart)
            {
                int modsLoaded = Loader.Instance.mods.Where(e => e.loaded).Count();
                int modsToLoad = Loader.Instance.mods.Where(e => e.info.loadOnStart).Count();
                int percentage = (int)(100f * modsLoaded / modsToLoad);
                Loader.Instance.logger.Log($"Loaded mod {info.name} ({modsLoaded}/{modsToLoad}) ({percentage}%)");
            }

        }
    }
}
