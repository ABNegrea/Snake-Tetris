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
    public partial class Highscore : Form
    {
        public Highscore()
        {
            InitializeComponent();
        }

        OleDbConnection conn;

        private void Form5_Load(object sender, EventArgs e)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Highscore.accdb";
            string q = "";
            conn = new OleDbConnection(cs);
            conn.Open();
            if (Meniu.Highscore_Snake == false)
                q = "SELECT Nume, Scor FROM Tabel WHERE Tip_Joc='S' ORDER BY -Scor";
            else
                q = "SELECT Nume, Scor FROM Tabel WHERE Tip_Joc='T' ORDER BY -Scor";
            OleDbCommand c = new OleDbCommand(q, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(c);
            DataTable t = new DataTable();
            da.Fill(t);
            dataGridView1.DataSource = t;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            Meniu.Highscore_Snake = true;
            Meniu.Highscore_Tetris = true;
        }
    }
}
