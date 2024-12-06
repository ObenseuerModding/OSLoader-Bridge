using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using OSLoaderCommons;

namespace OSLoader
{
    public class ModInfo
    {
        [JsonProperty] public ModType modType;
        [JsonProperty] public string name;
        [JsonProperty] public string description;
        [JsonProperty] public bool loadOnStart;
        [JsonProperty] public string repositoryUrl;
        [JsonProperty] public string modUrl;
        [JsonProperty] private string version;
        [JsonProperty] public string modId;
        [JsonProperty] public string author;
        [JsonProperty] public string dllFilepath;
        [JsonProperty] private string loaderVersion;
        [JsonProperty] private string gameVersion;
        [JsonProperty] public string[] dependencies;

        [JsonIgnore]
        internal string infoFilepath;
        [JsonIgnore]
        internal string settingsFilepath;

        [JsonIgnore]
        public Version Version
        {
            get { return new Version(version); }
            internal set { version = value.ToString(); }
        }

        [JsonIgnore]
        public Version LoaderVersion
        {
            get { return new Version(loaderVersion); }
            internal set { loaderVersion = value.ToString(); }
        }

        [JsonIgnore]
        public Version GameVersion
        {
            get { return new Version(gameVersion); }
            set { gameVersion = value.ToString(); }
        }

        // Returns null if is valid, returns a string message with an error message if it is not
        public string Validate()
        {
            Loader.Instance.logger.Detail($"Validation check for ModInfo at path {infoFilepath}: name: {name}, version: {version}");
            if (string.IsNullOrEmpty(name))
            {
                return "No name provided!";
            }

            if (string.IsNullOrEmpty(version))
            {
                return "No version provided!";
            }

            if (string.IsNullOrEmpty(repositoryUrl) || !(repositoryUrl.Contains("github.com") || repositoryUrl.Contains("gitlab.com"))) {
                repositoryUrl = null;
            }

            if (string.IsNullOrEmpty(modUrl))
            {
                modUrl = null;
            }

            if (string.IsNullOrEmpty(dllFilepath))
            {
                return "No DLL filepath provided!";
            }

            return null;
        }

        public void SaveInfo()
        {
            File.WriteAllText(infoFilepath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
