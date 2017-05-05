using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObfuscarDemo
{
    public class Singleton<T> where T : class, new()
    {
        private static T instance = null;
        public static T Instance
        {
            get
            {
                return GetInstance();
            }
        }

        public static T GetInstance()
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }

        protected Singleton()
        {

        }

    }
}
