using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat_Informatica
{
    public class Setari_Tetris
    {
        public static int Viteza_Cadere { get; set; }
        public static int Viteza_Detectare { get; set; }
        public static int Tip { get; set; }
        public static int Culoare { get; set; }
        public static int Rotatie_Curenta { get; set; }
        public static bool JocTerminat { get; set; }
        public static int Punctaj { get; set; }
        public static int Nivel { get; set; }
        public static int Linii_Curatate { get; set; }
        public static int Punctaj_Maxim { get; set; }
        public static int Urmatoarea_Piesa { get; set; }
        public static int Urmatoarea_Culoare { get; set; }

        public Setari_Tetris()
        {
            Viteza_Cadere = 400;
            Viteza_Detectare = 165;
            Tip = 0;
            Rotatie_Curenta = 1;
            Punctaj = 0;
            Punctaj_Maxim = 0;
            Linii_Curatate = 0;
            Nivel = 1;
            Culoare = 0;
            Urmatoarea_Piesa = 0;
            Urmatoarea_Culoare = 0;
            JocTerminat = false;
        }
    }
}
