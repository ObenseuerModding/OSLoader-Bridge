import fs from "fs";
import chpr from "child_process";
import path from "path";

/* Structure of distributable after build:
distributable
├─ installData
│  ├─ assemblies
│  │  ├─ mscorlib.dll
│  │  ├─ netstandard.dll
│  │  ...
│  │  └─ UnityEngine.XRModule.dll
│  ├ OSLoaderContents
│  │  ├─ loader
│  │  ├─ loader_config.json
│  │  ├─ Newtonsoft.Json.dll
│  │  ├─ OSLoader.dll
│  │  └─ UnityEngine.CoreModule.dll
│  ├─ winhttp.dll
│  └─ doorstop_config.ini
├─ Facepunch.Steamworks.Win64.dll
├─ Installer.exe
├─ Installer.dll
└─ steam_api64.dll
*/

const cmd_build_OSLoader = "cd ../OSLoader/ & dotnet build --configuration Release";
//const cmd_build_assetBundle = ""; // Figure out how to do this at some point
//const cmd_build_ProjectReferences = ""; // Figure out how to do this at some point
//const cmd_build_Launcher = ""; // DNE yet
const cmd_build_Installer = "cd ../Installer/ & dotnet build --configuration Release";

const outputPath = "./distributable/";
const logFile = "./output.log";
const OSLoaderBin = "../OSLoader/OSLoader/bin/Release/netstandard2.0";

const loaderConfig = {
    logDetails: false,
    version: "0.3.0",
    enabled: true
};

function main() {
    fs.rmSync(outputPath, { recursive: true });

    fs.writeFileSync(logFile, "");
    if (!fs.existsSync(outputPath)) fs.mkdirSync(outputPath);
    if (!fs.existsSync(path.join(outputPath, "installData"))) fs.mkdirSync(path.join(outputPath, "installData"));
    if (!fs.existsSync(path.join(outputPath, "installData", "assemblies"))) fs.mkdirSync(path.join(outputPath, "installData", "assemblies"));
    if (!fs.existsSync(path.join(outputPath, "installData", "OSLoaderContents"))) fs.mkdirSync(path.join(outputPath, "installData", "OSLoaderContents"));
    log("Generated structure of distributable folder");

    log(chpr.execSync(cmd_build_OSLoader).toString());
    log(chpr.execSync(cmd_build_Installer).toString());

    for (const file of fs.readdirSync("./unity2019.4.40f1")) {
        if (!fs.lstatSync(path.join("./unity2019.4.40f1", file)).isFile()) continue;
        if (fs.existsSync(path.join(outputPath, "installData/assemblies", file))) continue;
        log(`Copying DLL ${file} because it did not exist`);
        fs.copyFileSync(path.join("./unity2019.4.40f1", file), path.join(outputPath, "installData/assemblies", file));
    }

    log("Copying OSLoaderContents files");
    fs.copyFileSync(path.join(OSLoaderBin, "OSLoader.dll"), path.join(outputPath, "installData/OSLoaderContents/OSLoader.dll"));
    fs.copyFileSync(path.join(OSLoaderBin, "Newtonsoft.Json.dll"), path.join(outputPath, "installData/OSLoaderContents/Newtonsoft.Json.dll"));
    fs.copyFileSync(path.join(OSLoaderBin, "UnityEngine.CoreModule.dll"), path.join(outputPath, "installData/OSLoaderContents/UnityEngine.CoreModule.dll"));

    log("Generating loader_config.json");
    fs.writeFileSync(path.join(outputPath, "installData/OSLoaderContents/loader_config.json"), JSON.stringify(loaderConfig, null, "\t"));

    log("Copying loader assetbundle");
    fs.copyFileSync(path.join("../ProjectReferences/Assets/StreamingAssets/loader"), path.join(outputPath, "installData/OSLoaderContents/loader"));
    fs.copyFileSync(
        path.join("../ProjectReferences/Assets/StreamingAssets/loader.manifest"),
        path.join(outputPath, "installData/OSLoaderContents/loader.manifest")
    );

    log("Copying doorstop related files");
    fs.copyFileSync("./doorstop/winhttp.dll", path.join(outputPath, "installData/winhttp.dll"));
    fs.copyFileSync("./doorstop/doorstop_config.ini", path.join(outputPath, "installData/doorstop_config.ini"));

    log("Copying Facepunch and installer related stuff");
    fs.copyFileSync("./steam_api64/steam_api64.dll", path.join(outputPath, "steam_api64.dll"));
    fs.copyFileSync("../Installer/Installer/bin/Release/net8.0/Installer.exe", path.join(outputPath, "Installer.exe"));
    fs.copyFileSync("../Installer/Installer/bin/Release/net8.0/Installer.dll", path.join(outputPath, "Installer.dll"));
    fs.copyFileSync("../Installer/Installer/bin/Release/net8.0/Installer.runtimeconfig.json", path.join(outputPath, "Installer.runtimeconfig.json"));
    fs.copyFileSync("../Installer/Installer/bin/Release/net8.0/Facepunch.Steamworks.Win64.dll", path.join(outputPath, "Facepunch.Steamworks.Win64.dll"));

    fs.writeFileSync(path.join(outputPath, "Uninstall.bat"), "@echo off\nInstaller.exe uninstall\n");
}

function log(text: string) {
    console.log(text);
    fs.appendFileSync(logFile, text + "\n");
}

main();
