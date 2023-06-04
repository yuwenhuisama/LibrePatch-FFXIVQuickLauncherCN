using LibrePatch;

// Init constance.
Consts.Init();

if (Consts.HackStrategyVersion == 1)
{
    // Rewrite assembly
    Console.WriteLine("Hack with v1 strategy.");
    Console.WriteLine("Try to rewrite assembly...");
    foreach (var dirName in Directory.GetDirectories(Consts.XIVLauncherBasePath))
    {
        if (dirName.Contains("app-"))
        {
            Console.WriteLine("Find target directory: {0}, try to rewrite assembly", dirName);
            AssemblyHacker.HackV1(dirName);
            Console.WriteLine("Succeeded to rewrite assembly.");
        }
    }

    // Start mock http server
    Console.WriteLine("Start mock http server, you can safely close this window after injection completed.");
    MockHttpServer.Start();
}
else
{
    Console.WriteLine("Hack with v2 strategy.");
    Console.WriteLine("Try to rewrite assembly...");
    foreach (var dirName in Directory.GetDirectories(Path.Combine(Consts.XIVLauncherBasePath,
                 Consts.DalamudBaseDirectoryToScan)))
    {
        Console.WriteLine("Find target directory: {0}, try to rewrite assembly", dirName);
        AssemblyHacker.HackV2(dirName);
        Console.WriteLine("Succeeded to rewrite assembly.");
    }

    Console.WriteLine("Assembly rewrite completed, you can safely close this window.");
}