using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat_Informatica
{
    public class VerificareTaste
    {
        private static Hashtable Taste = new Hashtable();

        public static bool Verificare(Keys Tasta)
        {
            if (Taste[Tasta] == null)
                return false;
            return (bool)Taste[Tasta];
        }

        public static void Apasat(Keys key, bool state)
        {
            Taste[key] = state;
        }
    }
}