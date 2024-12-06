using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Reflection;
using System.Linq;
using Unity.Collections;

namespace OSLoader
{
    internal class Loader
    {
        public static Loader Instance { get; private set; }

        public bool ModloaderInitialized { get; private set; } = false;

        public const string loaderFilepath = @"./OSLoader";
        public const string loaderConfigFileFilepath = @"loader_config.json";
        public const string modsFilepath = @"mods";
        public const string modsSettingsFilename = @"settings.json";
        public const string modsInfoFilename = @"info.json";
        public const string assetBundleFilepath = @"loader";

        public MenuPrefabs prefabs;
        public ModStates modStates;

        public List<ModReference> mods = new List<ModReference>();

#if DEBUG
        public Logger logger = new Logger("OS Loader", true, true);
#else
        public Logger logger = new Logger("OS Loader", false, true);
#endif

        public AssetBundle assetBundle;
        public LoaderConfig config;

        public OSScene currentScene;
        
        public Loader()
        {
            Instance = this;
            Logger.Initialize();

            LoaderConfig.Load(loaderFilepath, loaderConfigFileFilepath, out config);

            if (!config.enabled)
            {
                logger.Log("OS Loader disabled. Not initializing.");
                return;
            }

            logger.Log("OS Loader initializing...");

            // When first called, this determines that the game has loaded its main assemblies
            SceneManager.sceneLoaded += OnSceneLoaded;
            logger.Log("OS Loader listening for game start...");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (!ModloaderInitialized) OnGameStart();
        }

        public void OnSceneFinishedLoading(Scene scene)
        {
            currentScene = (OSScene)scene.buildIndex;
        }

        private void OnGameStart()
        {
            ModloaderInitialized = true;

            // Asset Bundle
            assetBundle = AssetBundle.LoadFromFile(Path.Combine(loaderFilepath, assetBundleFilepath));
            logger.Log("Asset bundle loaded!");
            if (logger.logDetails)
            {
                logger.Detail("Contents of asset bundle:");
                foreach (string assetName in assetBundle.GetAllAssetNames())
                {
                    logger.Detail("- " + assetName);
                }
            }

            // Figure out what mods exist
            string modsDir = Path.Combine(loaderFilepath, modsFilepath);
            if (Directory.Exists(modsDir))
            {
                string[] modsFilepaths = Directory.GetDirectories(Path.Combine(loaderFilepath, modsFilepath));
                foreach (string modFilepath in modsFilepaths)
                {
                    var mod = new ModReference(modFilepath);
                    if (mod.valid) mods.Add(mod);
                }
            }
            else
            {
                Directory.CreateDirectory(modsDir);
                logger.Log("No mods directory found, created one, skipping mod loading, given no mods could have been found");
            }

            // Create mod UI
            prefabs = assetBundle.LoadAllAssets<MenuPrefabs>().SingleOrDefault();
            modStates = assetBundle.LoadAllAssets<ModStates>().SingleOrDefault();

            GameObject loaderCanvas = GameObject.Instantiate(prefabs.canvas);
            GameObject.DontDestroyOnLoad(loaderCanvas);
            GameObject.Instantiate(prefabs.loaderMenu, loaderCanvas.transform).GetComponent<LoaderMainMenu>().Initialize();

            // Mod loading
            logger.Log("Loading mods...");
            foreach (ModReference mod in mods)
            {
                if (mod.info.loadOnStart) mod.Load(isCalledByLoadOnStart: true);
            }
            logger.Log("Finished loading mods!");

            logger.Log("OS Loader initialized!");
            Logger.DeleteDoorstopLog();
        }
    }
}

