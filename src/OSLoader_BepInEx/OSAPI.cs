using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine.Events;
using Newtonsoft.Json;
using UnityEngine;
using System.Linq;

namespace OSLoader
{
    public static class OSAPI
    {
        public static Version GameVersion
        {
            get => new Version(Application.version);
            private set { }
        }

        public static OSScene CurrentScene { 
            get => Loader.Instance.currentScene;
            private set { }
        }

        public static bool IsModLoaded(string modName) => Loader.Instance.mods.Where(e => e.loaded).Count() != 0;

        public static bool IsModValid(string modName) => Loader.Instance.mods.Where(e => e.valid).Count() != 0;
    }
}
