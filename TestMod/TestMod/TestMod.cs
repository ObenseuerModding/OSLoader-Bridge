using System;
using OSLoader;
using UnityEngine;

namespace TestMod
{
    public class TestMod : Mod
    {
        public static TestMod instance;

        public override void InitializeMod()
        {
            instance = this;
            settings = new CustomSettingsExample();
        }

        public override void OnModInitialized()
        {
            Debug.Log("This amazing mod was loaded!");
        }

        public override void UnloadMod()
        {
            // Nothing yet
        }
    }
}
