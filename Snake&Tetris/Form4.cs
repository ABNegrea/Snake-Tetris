using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Atestat_Informatica
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        OleDbConnection conn;

        private void Form4_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Highscore.accdb";
            conn = new OleDbConnection(cs);
            conn.Open();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Meniu.Salveaza_Snake = false;
            Meniu.Salveaza_Tetris = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string q = "";
            string nume = textBox1.Text;
            string highscore = "";
            if (Meniu.Salveaza_Snake == true)
            {
                highscore = Setari_Snake.ScorMaxim.ToString();
                q = "INSERT INTO Tabel (Nume, Tip_Joc, Scor) VALUES('";
                q = q + nume + "', '" + "S" + "', '" + highscore + "')";
            }
            else
            {
                highscore = Setari_Tetris.Punctaj_Maxim.ToString();
                q = "INSERT INTO Tabel (Nume, Tip_Joc, Scor) VALUES('";
                q = q + nume + "', '" + "T" + "', '" + highscore + "')";
            }
            OleDbCommand c = new OleDbCommand(q, conn);
            c.ExecuteNonQuery();
            this.Close();
        }
    }
}
