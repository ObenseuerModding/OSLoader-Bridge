using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using BehaviorDesigner.Runtime.Tasks;
using BepInEx.Logging;
using OSLoader.Bridge;


//Its best practice to use file-scoped namespaces
namespace OSLoader;

public class Logger
{
    private readonly string name;
    private readonly bool logToLoaderLog;
    private readonly bool logTimestamps;
    private const string loaderFileFilepath = @"./loader.log";
    protected static ManualLogSource logSource = Plugin.Log;

    public bool logDetails = false;

    public Logger(string name, bool logToLoaderLog = false, bool logDetails = false, bool logTimestamps = true)
    {
        this.name = name;
        this.logToLoaderLog = logToLoaderLog;
        this.logDetails = logDetails;
        this.logTimestamps = logTimestamps;
    }

    public static void Initialize()
    {
        //nothing
    }

    public void Log(object obj) => Log(obj.ToString());
    public void Log(string message)
    {
        logSource.LogInfo(message);
    }

    public void Detail(object obj) => Detail(obj.ToString());

    public void Detail(string message)
    {
        if (!logDetails) return;
        Log(message);
    }

    public void Error(string message)
    {
        Log(message);
    }

    public void Warn(string message)
    {
        Log(message);
    }
}
