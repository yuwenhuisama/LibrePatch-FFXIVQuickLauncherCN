namespace LibrePatch;

public static class Consts
{
    public static readonly string AssemblyName = "XIVLauncher.Common.dll";
    public static readonly string AssetManagerFullName = "AssetManager";
    public static readonly string MethodNameToRewrite = "CheckAssetRefreshNeeded";
    public static readonly string MetaUrl = "https://aonyx.ffxiv.wang/Dalamud/Asset/Meta";
    public static readonly string MockBaseUrl = "http://127.0.0.1";
    public static readonly int MockPort = 9999;
    public static readonly string MockMetaUrl = $"{MockBaseUrl}:{MockPort}/Meta";
}