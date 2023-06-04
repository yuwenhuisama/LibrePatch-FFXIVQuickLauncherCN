namespace LibrePatch;

using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;

public static class AssemblyHacker
{
    public static void HackV1(string assemblyBasePath)
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
                       ((string)i.Operand) == Consts.MetaUrl;
            }, null);

            if (op == null)
            {
                Console.WriteLine("Target OpCode Not found.");
                return;
            }

            var field = type.Fields.FirstOrDefault(f => f.Name == Consts.AssetStoreUrlFieldName, null);
            if (field == null)
            {
                Console.WriteLine($"No field of {Consts.AssetStoreUrlFieldName} found");
            }
            else
            {
                field.Constant = Consts.MockMetaUrl;
            }

            var newIl = il.Create(OpCodes.Ldstr, Consts.MockMetaUrl);
            il.Replace(op, newIl);

            assembly.Write(assemblyPath + ".TMP");
        }

        // save org file
        BackupOriginAssembly(assemblyPath);
    }

    public static void HackV2(string dalamudAssemblyPath)
    {
        var assemblyPath = Path.Combine(dalamudAssemblyPath, Consts.DalamudAssemblyName);
        using (var assembly = AssemblyDefinition.ReadAssembly(assemblyPath, new ReaderParameters
               {
                   AssemblyResolver = new CustomResolver(dalamudAssemblyPath),
               }))
        {
            var type = assembly.MainModule.Types
                .FirstOrDefault(t => t.Name == Consts.PluginManagerName, null);

            if (type == null)
            {
                Console.WriteLine($"Type of {Consts.PluginManagerName} Not found.");
                return;
            }

            Console.WriteLine("Find type to modify: {0}", type.FullName);

            var targetMethod = type.Methods.FirstOrDefault(m => m.Name == ".ctor", null);
            if (targetMethod == null)
            {
                Console.WriteLine("Method of ctor Not found.");
                return;
            }

            Console.WriteLine("Find method to modify: {0}", targetMethod.Name);

            var il = targetMethod.Body.GetILProcessor();
            var op = il.Body.Instructions.FirstOrDefault(i =>
            {
                return i.OpCode == OpCodes.Stfld && i.Operand is FieldReference &&
                       ((FieldReference)i.Operand).Name == "bannedPlugins";
            }, null);

            if (op == null)
            {
                Console.WriteLine("Target OpCode Not found.");
                return;
            }

            var filedRef = ((FieldReference)op.Operand);

            var ldarg0 = il.Create(OpCodes.Ldarg_0);
            var ldloc = il.Create(OpCodes.Ldloc_3);
            var stFld = il.Create(OpCodes.Stfld, filedRef);
            il.InsertAfter(op, ldarg0);
            il.InsertAfter(ldarg0, ldloc);
            il.InsertAfter(ldloc, stFld);

            assembly.Write(assemblyPath + ".TMP");
        }

        BackupOriginAssembly(assemblyPath);
    }

    private static void BackupOriginAssembly(string assemblyPath)
    {
        Console.WriteLine("Backup original assembly file.");
        File.Move(assemblyPath, assemblyPath + ".bak");
        File.Move(assemblyPath + ".TMP", assemblyPath);
        Console.WriteLine("Success to rewrite.");
    }

    private class CustomResolver : BaseAssemblyResolver
    {
        private readonly DefaultAssemblyResolver defaultResolver_;
        private readonly string dalamudAssemblyFolder_;

        public CustomResolver(string dalamudAssemblyFolder)
        {
            this.dalamudAssemblyFolder_ = dalamudAssemblyFolder;
            this.defaultResolver_ = new DefaultAssemblyResolver();
        }

        public override AssemblyDefinition Resolve(AssemblyNameReference name)
        {
            AssemblyDefinition assembly;
            try
            {
                assembly = this.defaultResolver_.Resolve(name);
            }
            catch (AssemblyResolutionException ex)
            {
                assembly = AssemblyDefinition.ReadAssembly(Path.Combine(dalamudAssemblyFolder_,
                    ex.AssemblyReference.Name + ".dll"));
            }

            return assembly;
        }
    }
}