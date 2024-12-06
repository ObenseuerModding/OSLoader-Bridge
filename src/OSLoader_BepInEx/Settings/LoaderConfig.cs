using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace OSLoader
{
    internal class LoaderConfig
    {
        [JsonProperty] public bool logDetails;
        [JsonProperty] public bool enabled;
        [JsonProperty] private string version;

        [JsonIgnore]
        public Version Version
        {
            get { return new Version(version); }
            set { version = value.ToString(); }
        }

        public static void Load(string loaderFilepath, string loaderConfigFileFilepath, out LoaderConfig configRef)
        {
            if (File.Exists(Path.Combine(loaderFilepath, loaderConfigFileFilepath)))
            {
                Loader.Instance.logger.Log("Config file found, reading...");
                string rawLoaderConfig = File.ReadAllText(Path.Combine(loaderFilepath, loaderConfigFileFilepath));
                Loader.Instance.logger.Detail("Raw Loader Config: " + rawLoaderConfig);

                try
                {
                    configRef = JsonConvert.DeserializeObject<LoaderConfig>(rawLoaderConfig);
                    if (!configRef.IsValid())
                    {
                        Loader.Instance.logger.Log("Config file has invalid structure, resetting to default values...");
                        configRef = new LoaderConfig();
                        File.WriteAllText(Path.Combine(loaderFilepath, loaderConfigFileFilepath), JsonConvert.SerializeObject(configRef, Formatting.Indented));
                    }
                    return;
                }
                catch
                {
                    Loader.Instance.logger.Log("Something went wrong when trying to parse the loader's config. Creating new file");
                }
            }
            else
            {
                Loader.Instance.logger.Log("No config file found, creating file");
            }

            configRef = new LoaderConfig();
            File.WriteAllText(Path.Combine(loaderFilepath, loaderConfigFileFilepath), JsonConvert.SerializeObject(configRef, Formatting.Indented));

            Loader.Instance.logger.logDetails = configRef.logDetails;
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
