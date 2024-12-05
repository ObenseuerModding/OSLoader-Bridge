using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;


//Its best practice to use file-scoped namespaces
namespace OSLoader;

public class Logger
{
    private readonly string name;
    private readonly bool logToLoaderLog;
    private readonly bool logTimestamps;
    private const string loaderFileFilepath = @"./loader.log";

    //This creates a single stream instance that manages all logging
    //Rather than rely on File.WriteAllText which creates and destroys
    //Streams each time you call it
    private readonly static TextWriter _logOutput = new StreamWriter(loaderFileFilepath, false)
    {
        AutoFlush = true
    };

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
        // Since we set append to false in the writer we can
        // simply begin writing and it will clear for us

        _logOutput.WriteLine("[OS Loader] Logger initialized");
    }

    // This feels like a bad idea. There is a verbose and normal
    // Version of doorstop so if the user has these logs its because
    // The chose to install verbose and you shouldn't touch them imo
    public static void DeleteDoorstopLog()
    {
        string[] files = Directory.GetFiles("./", "*.log");
        foreach (string file in files)
        {
            if (file.ToLower().Contains("doorstop_"))
            {
                Loader.Instance.logger.Log("Deleting doorstop log file with path " + file);
                try
                {
                    File.Delete(file);
                }
                catch // (Exception e)
                {
                    // This actually comes in handy, we don't delete the last one (Shared File Exception)
                    // Loader.Instance.logger.Log("Could not delete Doorstop log file: " + e);
                }
            }
        }
    }

    public void Log(object obj)
    {
        Log(obj.ToString());
    }

    public void Log(string message)
    {
        string log = "";
        if (logTimestamps)
            log += $"[{JsonConvert.SerializeObject(DateTime.Now, new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" })}] ";

        log += $"[{name}] [INFO] {message}";
        if (logToLoaderLog)
            _logOutput.WriteLine(log);

        if (Loader.Instance.ModloaderInitialized)
            Debug.Log(log);
    }

    public void Detail(object obj) =>
    //{
    // The previous code here feels like a bug so I "fixed" it
    // For ya, buddy.

    //Log(obj.ToString());
    Detail(obj.ToString());

    // Also: Seriously consider using the inline form for these
    // types of methods
    // public void Detail(object obj) => Detail(obj.ToString())
    // Would be a lot cleaner so I left this as an example for you
    //}

    public void Detail(string message)
    {
        if (!logDetails) return;

        string log = "";
        if (logTimestamps)
            log += $"[{JsonConvert.SerializeObject(DateTime.Now, new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" })}] ";

        log += $"[{name}] [DETAIL] {message}";
        if (logToLoaderLog)
            _logOutput.WriteLine(log);

        if (Loader.Instance.ModloaderInitialized)
            Debug.Log(log);
    }

    public void Error(string message)
    {
        string log = "";
        if (logTimestamps)
            log += $"[{JsonConvert.SerializeObject(DateTime.Now, new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" })}] ";

        log += $"[{name}] [ERROR] {message}";
        if (logToLoaderLog)
            _logOutput.WriteLine(log);

        if (Loader.Instance.ModloaderInitialized)
            Debug.LogError(log);
    }

    public void Warn(string message)
    {
        string log = "";
        if (logTimestamps)
            log += $"[{JsonConvert.SerializeObject(DateTime.Now, new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" })}] ";

        log += $"[{name}] [WARN] {message}";
        if (logToLoaderLog)
            _logOutput.WriteLine(log);

        if (Loader.Instance.ModloaderInitialized)
            Debug.LogWarning(log);
    }
}
