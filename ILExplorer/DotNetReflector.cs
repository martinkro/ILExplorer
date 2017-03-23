using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ILExplorer
{
    // DotNet

    public class DotNetAssemblyReflector : IAssemblyReflector
    {
        private Assembly _assembly;
        public DotNetAssemblyReflector(Assembly assembly)
        {
            _assembly = assembly;
        }

        public virtual IEnumerable<IAttributeReflector> GetAttributes<T>() where T : Attribute
        {
            List<CustomAttributeData> returnValue = new List<CustomAttributeData>();
            var pCustomAttributeType = typeof(T);
            foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(_assembly))
            {
                if (customAttributeData.Constructor.DeclaringType.Name == pCustomAttributeType.Name)
                {
                    returnValue.Add(customAttributeData);
                }
            }

            return returnValue.Select(x => new DotNetAttributeReflector(x)).ToList();
        }

        public IEnumerable<ITypeReflector> GetTypes()
        {
            return _assembly.GetTypes().Select(t => new DotNetTypeReflector(t)).ToList();
        }

        public string Location
        {
            get
            {
                return _assembly.Location;
            }
        }

        public string FileName
        {
            get
            {
                return _assembly.ManifestModule.Name;
            }
        }

        public string FullName
        {
            get
            {
                return _assembly.FullName;
            }
        }

        public string GetVersion()
        {
            string version = string.Empty;
            var assemblyFileVersionCustomAttributeData = GetAttributes<AssemblyFileVersionAttribute>();
            if (assemblyFileVersionCustomAttributeData.Count() == 1)
            {
                try
                {
                    var assemblyFileVersion = assemblyFileVersionCustomAttributeData.First().Values;
                    version = assemblyFileVersion["version"];
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return version;
        }
    }

    public class DotNetTypeReflector : ITypeReflector
    {
        private Type _type;
        public DotNetTypeReflector(Type type)
        {
            _type = type;
        }

        public IEnumerable<ITypeReflector> GetInterfaces()
        {
            return _type.GetInterfaces().Select(i => new DotNetTypeReflector(i)).ToList();
        }

        public IEnumerable<IAttributeReflector> GetAttributes<T>() where T : Attribute
        {
            List<CustomAttributeData> returnValue = new List<CustomAttributeData>();
            var pCustomAttributeType = typeof(T);
            foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(_type))
            {
                if (customAttributeData.Constructor.DeclaringType.Name == pCustomAttributeType.Name)
                {
                    returnValue.Add(customAttributeData);
                }
            }

            return returnValue.Select(a => new DotNetAttributeReflector(a)).ToList();
        }


        public string FullName
        {
            get
            {
                return _type.FullName;
            }
        }

        public string Name
        {
            get
            {
                return _type.Name;
            }
        }
    }


    public class DotNetAttributeReflector : IAttributeReflector
    {
        private CustomAttributeData _attribute;
        private IDictionary<string, string> _values;

        public IDictionary<string, string> Values
        {
            get
            {
                if (_values == null)
                {
                    Dictionary<string, string> returnValue = new Dictionary<string, string>();
                    try
                    {
                        ParameterInfo[] ConstructorParameters = _attribute.Constructor.GetParameters();
                        for (int i = 0; i < ConstructorParameters.Length; i++)
                        {
                            returnValue.Add(ConstructorParameters[i].Name, _attribute.ConstructorArguments[i].Value.ToString());
                        }
                    }
                    catch (KeyNotFoundException e)
                    {
                        throw e;
                    }

                    foreach (CustomAttributeNamedArgument argument in _attribute.NamedArguments)
                    {
                        returnValue.Add(argument.MemberInfo.Name, argument.TypedValue.Value.ToString());
                    }

                    _values = returnValue;
                }
                return _values;
            }
        }

        public DotNetAttributeReflector(CustomAttributeData attribute)
        {
            _attribute = attribute;
        }
    }

    public class DotNetReflector : IReflector
    {
        public IAssemblyReflector LoadAssembly(string path)
        {
            return new DotNetAssemblyReflector(Assembly.LoadFrom(path));
        }
    }
 
}
