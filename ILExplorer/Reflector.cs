using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILExplorer
{
   
    public interface IReflector
    {
        IAssemblyReflector LoadAssembly(string path);
    }

    public interface IAssemblyReflector
    {
        IEnumerable<IAttributeReflector> GetAttributes<T>() where T : Attribute;
        IEnumerable<ITypeReflector> GetTypes();
        string Location { get; }
        string FileName { get; }
        string FullName { get; }
    }

    public interface ITypeReflector
    {
        IEnumerable<ITypeReflector> GetInterfaces();
        IEnumerable<IAttributeReflector> GetAttributes<T>() where T : Attribute;
        string FullName { get; }
        string Name { get; }
    }

    public interface IAttributeReflector
    {
        IDictionary<string,string> Values { get; }
    }



}
