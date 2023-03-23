using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat_Informatica
{
    public enum Direction
    {
        Sus,
        Jos,
        Stanga,
        Dreapta,
    };

    public class Setari_Snake
    {
        public static int Viteza { get; set; }
        public static int Scor { get; set; }
        public static int ScorMaxim { get; set; }
        public static int Puncte { get; set; }
        public static bool JocTerminat { get; set; }
        public static Direction Directie { get; set; }


        public Setari_Snake()
        {
            Viteza = 9;
            Scor = 0;
            Puncte = 1;
            JocTerminat = false;
            Directie = Direction.Dreapta;
        }
    }
}