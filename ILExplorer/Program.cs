using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILExplorer
{
    class Program
    {
        private static string version = "1.0.1";
        static void Main(string[] args)
        {
            Console.WriteLine("ILExplorer:" + version);
            TestReflector();
        }

        public static void TestReflector()
        {
            MonoReflector reflector = new MonoReflector();
            var assemblyReflector = reflector.LoadAssembly("./testdata/Mono.Cecil.dll");
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
