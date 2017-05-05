using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObfuscarDemo
{
    public class GameFramework:MonoSingleton<GameFramework>
    {
        public GameFramework()
        {

        }

        public IEnumerator<string> TestCoroutine()
        {
            yield return "XML";
        }

        public System.Collections.IEnumerator TestCoroutine2()
        {
            yield return 1;
        }
    }
}
