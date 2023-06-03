namespace LibrePatch;

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
}

public static class Consts
{
    public static string AssemblyName = "XIVLauncher.Common.dll";
    public static string AssetManagerFullName = "AssetManager";
    public static string MethodNameToRewrite = "CheckAssetRefreshNeeded";
    public static string MetaUrl = "https://aonyx.ffxiv.wang/Dalamud/Asset/Meta";
    public static string MockBaseUrl = "http://127.0.0.1";
    public static int MockPort = 9999;
    public static string XIVLauncherBasePath = "./";

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

            MockMetaUrl = $"{MockBaseUrl}:{MockPort}/Meta";
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to load config file, use default config.");
            throw;
        }
    }
}