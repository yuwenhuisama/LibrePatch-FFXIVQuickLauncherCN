namespace LibrePatch;

using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using EmbedIO;
using EmbedIO.Actions;
using EmbedIO.Files;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using JsonSerializer = System.Text.Json.JsonSerializer;

public static class MockHttpServer
{
    internal class AssetInfo
    {
        [JsonPropertyName("Version")]
        public int Version { get; set; }

        [JsonPropertyName("Assets")]
        public List<Asset> Assets { get; set; } = null!;

        public class Asset
        {
            [JsonPropertyName("Url")]
            public string Url { get; set; } = null!;

            [JsonPropertyName("FileName")]
            public string FileName { get; set; } = null!;

            [JsonPropertyName("Hash")]
            public string Hash { get; set; } = null!;
        }
    }

    private class MetaController : WebApiController
    {
        [Route(HttpVerbs.Get, "/")]
        public async Task<string> Meta()
        {
            Console.WriteLine("Request Incoming, Hijack it.");
            using var client = new HttpClient();
            var res = await client.GetFromJsonAsync<AssetInfo>(Consts.MetaUrl);
            var cheatRes = res!.Assets.FirstOrDefault(asset => asset.FileName.Contains("cheatplugin"));
            if (cheatRes != null)
            {
                using var sha1 = SHA1.Create();
                using var file = File.OpenRead(Consts.LocalCheatFilePath);
                var fileHash = sha1.ComputeHash(file);
                var stringHash = BitConverter.ToString(fileHash).Replace("-", "");
                cheatRes.Url = Consts.LocalFakeCheatFileUrl;
                cheatRes.Hash = stringHash;
            }

            var json = JsonSerializer.Serialize(res);
            return json;
        }
    }

    public static void Start()
    {
        using var httpServer = new WebServer(o =>
                o.WithUrlPrefix($"{Consts.MockBaseUrl}:{Consts.MockPort}")
                    .WithMode(HttpListenerMode.EmbedIO))
            .WithLocalSessionManager()
            .WithStaticFolder(Consts.LocalFileSystemBaseUrl, Consts.LocalFileSystemBasePath, false,
                conf => { conf.WithDirectoryLister(DirectoryLister.Html); })
            .WithWebApi("/Meta", (async (context, data) =>
            {
                context.Response.ContentType = "application/json";
                context.Response.ContentEncoding = WebServer.Utf8NoBomEncoding;
                await using var writer = context.OpenResponseStream(false, false);
                if (data is string s)
                {
                    await writer.WriteAsync(Encoding.UTF8.GetBytes(s));
                }
                else
                {
                    throw new Exception("Unknown data type.");
                }
            }), m => m.WithController<MetaController>())
            .WithModule(new ActionModule("/", HttpVerbs.Any, ctx => ctx.SendDataAsync(new { Message = "Error" })));

        httpServer.RunAsync();
        Console.ReadLine();
    }
}