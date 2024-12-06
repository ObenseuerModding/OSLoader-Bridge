using BepInEx;
using BepInEx.Logging;
using UnityEngine;

namespace OSLoader.Bridge;

[BepInPlugin(LCMPluginInfo.PLUGIN_GUID, LCMPluginInfo.PLUGIN_NAME, LCMPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log = null!;
    internal static Loader loader = null!;

    private void Awake()
    {
        Log = Logger;
        Log.LogInfo($"Plugin {LCMPluginInfo.PLUGIN_NAME} version {LCMPluginInfo.PLUGIN_VERSION} is loaded!");

        GameObject go = new("OSLoader Bridge", typeof(BridgeBehavior));
        DontDestroyOnLoad(go);
    }
}
