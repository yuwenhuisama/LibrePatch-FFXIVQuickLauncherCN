namespace LibrePatch;

using Mono.Cecil;
using Mono.Cecil.Cil;

public static class AssemblyHacker
{
    public static void Hack(string assemblyBasePath)
    {
        var assemblyPath = Path.Combine(assemblyBasePath, Consts.AssemblyName);
        using (var assembly = AssemblyDefinition.ReadAssembly(assemblyPath))
        {
            var type = assembly.MainModule.Types
                .FirstOrDefault(t => t.Name == Consts.AssetManagerFullName, null);
    
            if (type == null)
            {
                Console.WriteLine($"Type of {Consts.AssetManagerFullName} Not found.");
                return;
            }

            Console.WriteLine("Find type to modify: {0}", type.FullName);
    
            var targetMethod = type.Methods.FirstOrDefault(m => m.Name == Consts.MethodNameToRewrite, null);
            if (targetMethod == null)
            {
                Console.WriteLine($"Method of {Consts.MethodNameToRewrite} Not found.");
                return;
            }
   
            Console.WriteLine("Find method to modify: {0}", targetMethod.Name);
    
            var il = targetMethod.Body.GetILProcessor();
            var op = il.Body.Instructions.FirstOrDefault(i =>
            {
                return i.OpCode == OpCodes.Ldstr && i.Operand is string &&
                       ((string)i.Operand) == "https://aonyx.ffxiv.wang/Dalamud/Asset/Meta";
            }, null);
    
            if (op == null)
            {
                Console.WriteLine("Target OpCode Not found.");
                return;
            }

            var newIl = il.Create(OpCodes.Ldstr, Consts.MockMetaUrl);
            il.Replace(op, newIl);

            assembly.Write(assemblyPath + ".TMP");
        }

        // save org file
        Console.WriteLine("Backup original assembly file.");
        File.Move(assemblyPath, assemblyPath + ".bak");
        File.Move(assemblyPath + ".TMP", assemblyPath);
        Console.WriteLine("Success to rewrite.");
    }
}