using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat_Informatica
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PictureBox Snake_Icon = new PictureBox();
        PictureBox Tetris_Icon = new PictureBox();
        PictureBox About_It = new PictureBox();

        string Snake = "/Snake Game Icon.png";
        string Tetris = "/Tetris Game Icon.png";
        string Aboutit = "/About.png";

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 300;  
            this.Width = 600;

            Snake_Icon.Image = Image.FromFile(Application.StartupPath + Snake);
            Snake_Icon.Location = new Point(73, 75);
            Snake_Icon.Width = 59;
            Snake_Icon.Height = 59;
            this.Controls.Add(Snake_Icon);

            Tetris_Icon.Image = Image.FromFile(Application.StartupPath + Tetris);
            Tetris_Icon.Location = new Point(73, 160);
            Tetris_Icon.Width = 59;
            Tetris_Icon.Height = 59;
            this.Controls.Add(Tetris_Icon);

            About_It.Image = Image.FromFile(Application.StartupPath + Aboutit);
            About_It.Location = new Point(560, 235);
            About_It.Width = 20;
            About_It.Height = 20;
            About_It.Click += new EventHandler(About_It_Click);
            this.Controls.Add(About_It);

            new Meniu();
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            if (Meniu.OK_Snake == true && Meniu.OK_Tetris == true)
            {
                Form2 Snake_Game;
                Snake_Game = new Form2();
                Snake_Game.Show();
                Meniu.OK_Snake = false;
                Meniu.OK_Tetris = false;
                this.Location = new Point(900, 300);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Meniu.OK_Tetris == true && Meniu.OK_Snake == true)
            {
                Form3 Tetris_Game;
                Tetris_Game = new Form3();
                Tetris_Game.Show();
                Meniu.OK_Snake = false;
                Meniu.OK_Tetris = false;
                this.Location = new Point(900, 300);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Meniu.Highscore_Snake == true && Meniu.Highscore_Tetris == true)
            {
                Meniu.Highscore_Snake = false;
                Highscore Highscore = new Highscore();
                Highscore.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Meniu.Highscore_Snake == true && Meniu.Highscore_Tetris == true)
            {
                Meniu.Highscore_Tetris = false;
                Highscore Highscore = new Highscore();
                Highscore.Show();

            }
        }

        private void About_It_Click(object sender, EventArgs e)
        {
            Form6 About;
            About = new Form6();
            About.Show();
        }
    }
}