using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat_Informatica
{
    class Meniu
    {
        public static bool OK_Snake { get; set; }
        public static bool OK_Tetris { get; set; }
        public static bool Salveaza_Snake { get; set; }
        public static bool Salveaza_Tetris { get; set; }
        public static bool Highscore_Snake { get; set; }
        public static bool Highscore_Tetris { get; set; }


        public Meniu()
        {
            OK_Snake = true;
            OK_Tetris = true;
            Salveaza_Snake = false;
            Salveaza_Tetris = false;
            Highscore_Snake = true;
            Highscore_Tetris = true;
        }
    }
}
