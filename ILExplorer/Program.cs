using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using System.IO;

namespace ILExplorer
{
    class Program
    {
        private static string version = "1.0.1";
        static void Main(string[] args)
        {
            Console.WriteLine("ILExplorer:" + version);
            TestAssemblyDefinition();
        }

        public static bool IsInheritFrom(string namespaceName, string baseName, TypeDefinition type)
        {
            bool result = false;
            var baseType = type.BaseType;
            while(baseType != null)
            {
                if(baseType.Namespace == namespaceName && baseType.Name==baseName)
                {
                    result = true;
                    break;
                }

                if(baseType.IsDefinition)
                {
                    var temp = baseType as TypeDefinition;
                    baseType = temp.BaseType;
                }
                else if (baseType.IsGenericInstance)
                {
                    GenericInstanceType temp = baseType as GenericInstanceType;
                    TypeReference t = temp.ElementType;
                    if (t.IsDefinition)
                    {
                        var x = t as TypeDefinition;
                        baseType = x.BaseType;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return result;
        }

        public static void TestAssemblyDefinition()
        {
            string path = "./testdata/ObfuscarDemo.dll";
            var resolver = new DefaultAssemblyResolver();
            resolver.AddSearchDirectory(Path.GetDirectoryName(path));
            var reader = new ReaderParameters()
            {
                AssemblyResolver = resolver
            };

            AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(path, reader);
            foreach (var module in assembly.Modules)
            {
                Console.WriteLine("Module name:" + module.Name);
                foreach(var type in module.GetTypes())
                {
                    
                    Console.WriteLine("Type {0}:{1}", type.Namespace, type.Name);
                    if(IsInheritFrom("ObfuscarDemo", "MonoBehavior", type))
                    {
                        Console.WriteLine("Special Type:" + type.Name);
                        foreach(MethodDefinition method in type.Methods)
                        {
                            
                            var a = method.ReturnType;
                            Console.WriteLine("Return Type Name:" + a.FullName);


                            Console.WriteLine("Method Name:" + method.Name);
                        }
                    }
                }
            }
        }

        public static void TestReflector()
        {
            MonoReflector reflector = new MonoReflector();
            var assemblyReflector = reflector.LoadAssembly("./testdata/ObfuscarDemo.dll");
            var types = assemblyReflector.GetTypes();
            var attributes = assemblyReflector.GetAttributes<Attribute>();
            foreach(var type in types)
            {
                Console.WriteLine("Type Name:" + type.FullName);
            }

            foreach(var attr in attributes)
            {
                foreach(var item in attr.Values){
                    Console.WriteLine("Attribute:" + item.Key + ":" + item.Value);
                }
            }
            
        }
    }
}
