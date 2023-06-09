﻿namespace LibrePatch;

using System.Text.Json;
using System.Text.Json.Serialization;

public class Config
{
    [JsonPropertyName("AssemblyName")]
    public string AssemblyName { get; set; }

    [JsonPropertyName("AssetManagerName")]
    public string AssetManagerName { get; set; }

    [JsonPropertyName("MethodNameToRewrite")]
    public string MethodNameToRewrite { get; set; }

    [JsonPropertyName("MetaUrl")]
    public string MetaUrl { get; set; }

    [JsonPropertyName("MockBaseUrl")]
    public string MockBaseUrl { get; set; }

    [JsonPropertyName("MockPort")]
    public int MockPort { get; set; }

    [JsonPropertyName("XIVLauncherBasePath")]
    public string XIVLauncherBasePath { get; set; }

    [JsonPropertyName("DalamudAssemblyName")]
    public string DalamudAssemblyName { get; set; }

    [JsonPropertyName("DalamudBaseDirectoryToScan")]
    public string DalamudBaseDirectoryToScan { get; set; }

    [JsonPropertyName("PluginManagerName")]
    public string PluginManagerName { get; set; }

    [JsonPropertyName("HackStrategyVersion")]
    public int HackStrategyVersion { get; set; }

    [JsonPropertyName("AssetStoreUrlFieldName")]
    public string AssetStoreUrlFieldName { get; set; }

    [JsonPropertyName("LocalFileSystemBasePath")]
    public string LocalFileSystemBasePath { get; set; }

    [JsonPropertyName("LocalCheatFileName")]
    public string LocalCheatFileName { get; set; }
}

public static class Consts
{
    public static string AssemblyName = "XIVLauncher.Common.dll";
    public static string AssetManagerFullName = "AssetManager";
    public static string MethodNameToRewrite = "CheckAssetRefreshNeeded";
    public static string MetaUrl = "https://aonyx.ffxiv.wang/Dalamud/Asset/Meta";

    public static string LocalFileSystemBaseUrl = "/file";
    public static string LocalFileSystemBasePath = "./file";

    public static string LocalCheatFileName =
        "cheatplugin.011262350f30f39ad448bd802e653a7067c10c072f42cd973c2302c65a5e2661.json";

    public static string LocalCheatFilePath = Path.Combine(LocalFileSystemBasePath, LocalCheatFileName);

    public static string LocalFakeCheatFileUrl =
        $"{MockBaseUrl}:{MockPort}{LocalFileSystemBaseUrl}/{LocalCheatFileName}";

    public static string MockBaseUrl = "http://127.0.0.1";
    public static int MockPort = 9999;
    public static string XIVLauncherBasePath = "./";
    public static string AssetStoreUrlFieldName = "ASSET_STORE_URL";

    public static string DalamudAssemblyName = "Dalamud.dll";
    public static string DalamudBaseDirectoryToScan = "Roaming/addon/Hooks";
    public static string PluginManagerName = "PluginManager";
    public static int HackStrategyVersion = 2;

    public static string MockMetaUrl = $"{MockBaseUrl}:{MockPort}/Meta";

    public static void Init()
    {
        try
        {
            var conf = File.ReadAllText("./Config.json");
            var confObj = JsonSerializer.Deserialize<Config>(conf);

            AssemblyName = confObj!.AssemblyName;
            AssetManagerFullName = confObj.AssetManagerName;
            MethodNameToRewrite = confObj.MethodNameToRewrite;
            MetaUrl = confObj.MetaUrl;
            MockBaseUrl = confObj.MockBaseUrl;
            MockPort = confObj.MockPort;
            XIVLauncherBasePath = confObj.XIVLauncherBasePath;
            AssetStoreUrlFieldName = confObj.AssetStoreUrlFieldName;

            LocalFileSystemBasePath = confObj.LocalFileSystemBasePath;
            LocalCheatFileName = confObj.LocalCheatFileName;
            LocalCheatFilePath = Path.Combine(LocalFileSystemBasePath, LocalCheatFileName);
            LocalFakeCheatFileUrl = $"{MockBaseUrl}:{MockPort}/file/{LocalCheatFileName}";

            DalamudAssemblyName = confObj.DalamudAssemblyName;
            DalamudBaseDirectoryToScan = confObj.DalamudBaseDirectoryToScan;
            PluginManagerName = confObj.PluginManagerName;
            HackStrategyVersion = confObj.HackStrategyVersion;

            MockMetaUrl = $"{MockBaseUrl}:{MockPort}/Meta";
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to load config file, use default config.");
            throw;
        }
    }
}