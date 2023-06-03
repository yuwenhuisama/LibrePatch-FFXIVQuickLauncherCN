using LibrePatch;

// Rewrite assembly
Console.WriteLine("Try to rewrite assembly...");
foreach(var dirName in Directory.GetDirectories("./")) {
    if (dirName.Contains("app-"))
    {
        Console.WriteLine("Find target directory: {0}, try to rewrite assembly", dirName);
        AssemblyHacker.Hack(dirName);
        Console.WriteLine("Succeeded to rewrite assembly.", dirName);
    }
}

// Start mock http server
Console.WriteLine("Start mock http server, you can safely close this window after injection completed.");
MockHttpServer.Start();
