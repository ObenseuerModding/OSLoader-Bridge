using System;
using UnityEngine;

namespace OSLoader.Bridge;

public class BridgeBehavior : MonoBehaviour
{
    private static Loader loader;

    private void Start()
    {
        Plugin.Log.LogInfo("Bridge Behavior start");
        try
        {
            loader = new();
        }
        catch (Exception ex)
        {
            Plugin.Log.LogError(ex);
        }
        Plugin.Log.LogInfo($"Loader initialized: {loader != null}");
    }
}
