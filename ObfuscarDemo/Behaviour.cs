using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObfuscarDemo
{
    public class Behaviour:Component
    {
        public bool enabled
        {
            get;set;
        }

        public bool isActiveAndEnabled
        {
            get;
        }
    }
}
