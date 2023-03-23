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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public List<PictureBox> Snake = new List<PictureBox>();
        public List<int> Pozitie = new List<int>(); // 1-Sus || 2-Jos || 3-Stanga || 4-Dreapta
        PictureBox Mar= new PictureBox();
        PictureBox Cap_Sarpe = new PictureBox();
        PictureBox Suprafata_de_Joc = new PictureBox();
        PictureBox Rama_Suprafata = new PictureBox();
        PictureBox Cupa = new PictureBox();
        PictureBox ScorTotal = new PictureBox();
        PictureBox Seteaza = new PictureBox();

        Label Punctaj = new Label();
        Label PunctajMaxim = new Label();
        Label GameOver = new Label();

        string Teren = "/Snake Game Background.png";
        string Rama = "/Snake Game Frame.png";
        string Fruct = "/Snake Game Apple.png";
        string ScorMaxim = "/Snake Game Highscore.png";
        string Scor = "/Snake Game Score.png";
        string Setam = "/Salveaza Snake.png";

        string CapStanga = "/Snake Game Head Left.png";
        string CapDreapta = "/Snake Game Head Right.png";
        string CapSus = "/Snake Game Head Up.png";
        string CapJos = "/Snake Game Head Down.png";

        string CoadaStanga = "/Snake Game Tail Left.png";
        string CoadaDreapta = "/Snake Game Tail Right.png";
        string CoadaSus = "/Snake Game Tail Up.png";
        string CoadaJos = "/Snake Game Tail Down.png";
        string CoadaStangaSus = "/Snake Game Tail LeftUp.png";
        string CoadaStangaJos = "/Snake Game Tail LeftDown.png";
        string CoadaDreaptaSus = "/Snake Game Tail RightUp.png";
        string CoadaDreaptaJos = "/Snake Game Tail RightDown.png";
        string CoadaSusStanga = "/Snake Game Tail UpLeft.png";
        string CoadaSusDreapta = "/Snake Game Tail UpRight.png";
        string CoadaJosStanga = "/Snake Game Tail DownLeft.png";
        string CoadaJosDreapta = "/Snake Game Tail DownRight.png";

        string CorpOrizontal = "/Snake Game Body Horizontal.png";
        string CorpVertical = "/Snake Game Body Vertical.png";
        string CorpStangaSus = "/Snake Game Body LeftUp.png";
        string CorpStangaJos = "/Snake Game Body LeftDown.png";
        string CorpDreaptaSus = "/Snake Game Body RightUp.png";
        string CorpDreaptaJos = "/Snake Game Body RightDown.png";

        string Inceput = "              Apasă Enter sau Space \n                pentru a incepe jocul \n\n   W/Up - Sus                A/Left - Stanga \n   S/Down - Jos            D/Right - Dreapta";
        string PrimulRand = "                Joc Terminat! \n               Scor Maxim: ";
        string AlTreileaRand = "\n        Apasă Enter sau Space \n          pentru a juca din nou";
        string AlDoileaRand = "         Esti campion la Snake!     ";
        string TerminatPrimulRand = "            Bravo, ai castigat!\n";

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Height = 635;
            this.Width = 590;
            this.Location = new Point(100, 50);

            Rama_Suprafata.Image = Image.FromFile(Application.StartupPath + Rama);
            Rama_Suprafata.Height = 635;
            Rama_Suprafata.Width = 590;
            Rama_Suprafata.Location = new Point(0, 0);
            this.Controls.Add(Rama_Suprafata);

            Suprafata_de_Joc.Image = Image.FromFile(Application.StartupPath + Teren);
            Suprafata_de_Joc.Height = 450;
            Suprafata_de_Joc.Width = 510;
            Suprafata_de_Joc.Location = new Point(30, 110);
            Suprafata_de_Joc.BackColor = Color.Transparent;
            Rama_Suprafata.Controls.Add(Suprafata_de_Joc);

            Cap_Sarpe.Image = Image.FromFile(Application.StartupPath + CapDreapta);
            Cap_Sarpe.Height = 30;
            Cap_Sarpe.Width = 30;

            Mar.Image = Image.FromFile(Application.StartupPath + Fruct);
            Mar.BackColor = Color.Transparent;
            Mar.Height = 30;
            Mar.Width = 30;

            Cupa.BackColor = Color.Transparent;
            Cupa.Location = new Point(150, 10);
            Cupa.Image = Image.FromFile(Application.StartupPath + ScorMaxim);

            ScorTotal.BackColor = Color.Transparent;
            ScorTotal.Location = new Point(30, 20);
            ScorTotal.Image = Image.FromFile(Application.StartupPath + Scor);
            ScorTotal.Height = 50;
            ScorTotal.Width = 50;

            Punctaj.Location = new Point(75, 18);
            Punctaj.BackColor = Color.Transparent;
            Punctaj.Height = 50;
            Punctaj.Width = 100;
            Punctaj.Font = new Font("Microsoft Sans Serif", 25);
            Punctaj.ForeColor = Color.White;

            PunctajMaxim.Location = new Point(220, 18);
            PunctajMaxim.BackColor = Color.Transparent;
            PunctajMaxim.Height = 50;
            PunctajMaxim.Width = 100;
            PunctajMaxim.Font = new Font("Microsoft Sans Serif", 25);
            PunctajMaxim.ForeColor = Color.White;

            GameOver.Location = new Point(0, 120);
            GameOver.BackColor= Color.Black;
            GameOver.Height = 180;
            GameOver.Width = 510;
            GameOver.Font = new Font("Microsoft Sans Serif", 20);
            GameOver.ForeColor = Color.White;
            GameOver.Text = Inceput;
            Suprafata_de_Joc.Controls.Add(GameOver);

            Seteaza.Location = new Point(350, 22);
            Seteaza.Height = 30;
            Seteaza.Width = 125;
            Seteaza.Image = Image.FromFile(Application.StartupPath + Setam);
            Seteaza.Click += new EventHandler(Seteaza_Click);

            new Setari_Snake();
            Setari_Snake.JocTerminat = true;

            Timer.Interval = 1000 / Setari_Snake.Viteza;
            Timer.Tick += (s, ev) => { Miscare(s, ev); };
            Timer.Start();

            if (Setari_Snake.JocTerminat == true)
            {
                if (VerificareTaste.Verificare(Keys.Enter) || VerificareTaste.Verificare(Keys.Space))
                    Start();
            }
        }

        public void Start()
        {
            new Setari_Snake();
            Snake.Clear();
            Pozitie.Clear();
            Rama_Suprafata.Controls.Remove(Seteaza);
            Suprafata_de_Joc.Controls.Clear();
            Snake.Add(Cap_Sarpe);
            Rama_Suprafata.Controls.Add(Punctaj);
            Rama_Suprafata.Controls.Add(ScorTotal);
            Pozitie.Add(4);
            Punctaj.Text = Setari_Snake.Scor.ToString();
            Snake[0].Location = new Point(3 * 30, 7 * 30);
            Suprafata_de_Joc.Controls.Add(Snake[0]);
            GameOver.Font = new Font("Microsoft Sans Serif", 25);
            Mancare();
        }

        public void Mancare()
        {
            Random PozitieRandom = new Random();
            int x = PozitieRandom.Next(0, 16)*30;
            int y = PozitieRandom.Next(0, 14)*30;
            bool OK = false;
            while (OK == false)
            {
                OK = true;
                if (x == 3 * 30 && y == 7 * 30 && Snake.Count == 1)
                     OK = false;
                for (int i = 0; i < Snake.Count; i++)
                    if (Snake[i].Location.X == x && Snake[i].Location.Y == y)
                    {
                        OK = false;
                        break;
                    }
                if(OK==false)
                {
                    PozitieRandom = new Random();
                    x = PozitieRandom.Next(0, 16) * 30;
                    y = PozitieRandom.Next(0, 14) * 30;
                }
            }
            Mar.Location = new Point(x,y);
            Suprafata_de_Joc.Controls.Add(Mar);
        }

        public void Miscare(object sender, EventArgs e)
        {
            if (Setari_Snake.JocTerminat == true)
            {
                if (VerificareTaste.Verificare(Keys.Enter) || VerificareTaste.Verificare(Keys.Space))
                    Start();
            }
            else
            {
                if ((VerificareTaste.Verificare(Keys.Right) || VerificareTaste.Verificare(Keys.D)) && Setari_Snake.Directie != Direction.Stanga)
                    Setari_Snake.Directie = Direction.Dreapta;
                else if ((VerificareTaste.Verificare(Keys.Left) || VerificareTaste.Verificare(Keys.A)) && Setari_Snake.Directie != Direction.Dreapta)
                    Setari_Snake.Directie = Direction.Stanga;
                else if ((VerificareTaste.Verificare(Keys.Up) || VerificareTaste.Verificare(Keys.W)) && Setari_Snake.Directie != Direction.Jos)
                    Setari_Snake.Directie = Direction.Sus;
                else if ((VerificareTaste.Verificare(Keys.Down) || VerificareTaste.Verificare(Keys.S)) && Setari_Snake.Directie != Direction.Sus)
                    Setari_Snake.Directie = Direction.Jos;

                MiscareSarpe();
            }
            Suprafata_de_Joc.Invalidate();
        }

        public void MiscareSarpe()
        {

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Setari_Snake.Directie)
                    {
                        case Direction.Dreapta:
                            Snake[i].Location = new Point(Snake[i].Location.X + 30, Snake[i].Location.Y);
                            Pozitie[i] = 4;
                            break;
                        case Direction.Stanga:
                            Snake[i].Location = new Point(Snake[i].Location.X - 30, Snake[i].Location.Y);
                            Pozitie[i] = 3;
                            break;
                        case Direction.Sus:
                            Snake[i].Location = new Point(Snake[i].Location.X, Snake[i].Location.Y - 30);
                            Pozitie[i] = 1;
                            break;
                        case Direction.Jos:
                            Snake[i].Location = new Point(Snake[i].Location.X, Snake[i].Location.Y + 30);
                            Pozitie[i] = 2;
                            break;
                    }

                    int PozitieMaximaX = 16 * 30;
                    int PozitieMaximaY = 14 * 30;

                    if (Snake[i].Location.X < 0 || Snake[i].Location.Y < 0)
                        Moarte();
                    if (Snake[i].Location.X > PozitieMaximaX || Snake[i].Location.Y > PozitieMaximaY)
                        Moarte();

                    for (int j = 1; j < Snake.Count; j++)
                        if (Snake[i].Location.X == Snake[j].Location.X && Snake[i].Location.Y == Snake[j].Location.Y)
                        {
                            Moarte();
                            break;
                        }
                    if (Snake[0].Location.X == Mar.Location.X && Snake[0].Location.Y == Mar.Location.Y)
                        Mananca();
                }
                else
                {
                    Snake[i].Location = new Point(Snake[i - 1].Location.X, Snake[i - 1].Location.Y);
                    Pozitie[i] = Pozitie[i - 1];
                }
            }

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    if (Pozitie[i] == 4)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CapDreapta);
                    else if (Pozitie[i] == 3)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CapStanga);
                    else if (Pozitie[i] == 1)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CapSus);
                    else
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CapJos);
                }
                else if (i == Snake.Count - 1)
                {

                    if (Pozitie[i - 1] == 2 && Pozitie[i] == 3)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CoadaJosStanga);

                    else if (Pozitie[i - 1] == 4 && Pozitie[i] == 1)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CoadaDreaptaSus);

                    else if (Pozitie[i - 1] == 2 && Pozitie[i] == 4)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CoadaJosDreapta);

                    else if (Pozitie[i - 1] == 3 && Pozitie[i] == 1)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CoadaStangaSus);

                    else if (Pozitie[i - 1] == 3 && Pozitie[i] == 2)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CoadaStangaJos);

                    else if (Pozitie[i - 1] == 1 && Pozitie[i] == 4)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CoadaSusDreapta);

                    else if (Pozitie[i - 1] == 4 && Pozitie[i] == 2)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CoadaDreaptaJos);

                    else if (Pozitie[i - 1] == 1 && Pozitie[i] == 3)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CoadaSusStanga);

                    else if (Pozitie[i - 1] == 3 && Pozitie[i] == 3)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CoadaStanga);

                    else if (Pozitie[i - 1] == 4 && Pozitie[i] == 4)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CoadaDreapta);

                    else if (Pozitie[i - 1] == 2 && Pozitie[i] == 2)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CoadaJos);

                    else if (Pozitie[i - 1] == 1 && Pozitie[i] == 1)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CoadaSus);
                }
                else
                {
                    if ((Snake[i].Location.X == Snake[i - 1].Location.X && Snake[i - 1].Location.X == Snake[i + 1].Location.X) ||
                        (Snake[i].Location.Y == Snake[i - 1].Location.Y && Snake[i - 1].Location.Y == Snake[i + 1].Location.Y))
                    {
                        if (Pozitie[i] == 1 || Pozitie[i] == 2)
                            Snake[i].Image = Image.FromFile(Application.StartupPath + CorpVertical);
                        else
                            Snake[i].Image = Image.FromFile(Application.StartupPath + CorpOrizontal);
                    }
                    else if (Snake[i - 1].Location.X + 30 == Snake[i].Location.X && Snake[i].Location.Y - 30 == Snake[i + 1].Location.Y)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CorpDreaptaSus);

                    else if (Snake[i + 1].Location.Y == Snake[i].Location.Y + 30 && Snake[i].Location.X == Snake[i - 1].Location.X + 30)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CorpDreaptaJos);

                    else if (Snake[i + 1].Location.Y + 30 == Snake[i].Location.Y && Snake[i].Location.X == Snake[i - 1].Location.X - 30)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CorpStangaSus);

                    else if (Snake[i + 1].Location.Y == Snake[i].Location.Y + 30 && Snake[i].Location.X == Snake[i - 1].Location.X - 30)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CorpStangaJos);

                    else if (Snake[i + 1].Location.X + 30 == Snake[i].Location.X && Snake[i - 1].Location.Y == Snake[i].Location.Y - 30)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CorpDreaptaSus);

                    else if (Snake[i + 1].Location.X - 30 == Snake[i].Location.X && Snake[i].Location.Y + 30 == Snake[i - 1].Location.Y)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CorpStangaJos);

                    else if (Snake[i].Location.X - 30 == Snake[i + 1].Location.X && Snake[i].Location.Y == Snake[i - 1].Location.Y - 30)
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CorpDreaptaJos);

                    else
                        Snake[i].Image = Image.FromFile(Application.StartupPath + CorpStangaSus);
                }
            }
        }

        public void Mananca()
        {
            PictureBox CorpNou = new PictureBox();
            CorpNou.Image = Image.FromFile(Application.StartupPath + Fruct);
            CorpNou.Width = 30;
            CorpNou.Height = 30;
            CorpNou.Location = new Point(Snake[Snake.Count - 1].Location.X, Snake[Snake.Count - 1].Location.Y);
            Snake.Add(CorpNou);
            Pozitie.Add(0);
            Suprafata_de_Joc.Controls.Add(CorpNou);
            Setari_Snake.Scor+=Setari_Snake.Puncte;
            Punctaj.Text = Setari_Snake.Scor.ToString();
            if (Setari_Snake.Scor >= 15 * 17 - 1)
                Castigat();
            Mancare();
        }

        public void Castigat()
        {
            Setari_Snake.JocTerminat = true;
            Suprafata_de_Joc.Controls.Clear();
            if (Setari_Snake.Scor >= Setari_Snake.ScorMaxim)
                Setari_Snake.ScorMaxim = Setari_Snake.Scor;
            PunctajMaxim.Text = Setari_Snake.ScorMaxim.ToString();
            Rama_Suprafata.Controls.Add(PunctajMaxim);
            Rama_Suprafata.Controls.Add(Cupa);
            //Score = 0;
            Suprafata_de_Joc.Controls.Add(GameOver);
            GameOver.Text = (TerminatPrimulRand +AlDoileaRand+ AlTreileaRand);
        }

        public void Moarte()
        {
            GameOver.Font = new Font("Microsoft Sans Serif", 25);
            Setari_Snake.JocTerminat = true;
            Suprafata_de_Joc.Controls.Clear();
            if (Setari_Snake.Scor >= Setari_Snake.ScorMaxim)
                Setari_Snake.ScorMaxim = Setari_Snake.Scor;
            PunctajMaxim.Text = Setari_Snake.ScorMaxim.ToString();
            Rama_Suprafata.Controls.Add(PunctajMaxim);
            Rama_Suprafata.Controls.Add(Cupa);
           // Setari_Snake.Scor = 0;
            Suprafata_de_Joc.Controls.Add(GameOver);
            Rama_Suprafata.Controls.Add(Seteaza);
            GameOver.Text = (PrimulRand + PunctajMaxim.Text.ToString() + AlTreileaRand);
        }

        private void Seteaza_Click(object sender, EventArgs e)
        {
            Form4 Salveaza;
            Salveaza = new Form4();
            Salveaza.Show();
            Meniu.OK_Tetris = false;
            Meniu.OK_Snake = false;
            this.Location = new Point(900, 300);
            Rama_Suprafata.Controls.Remove(Seteaza);
            this.Location = new Point(100, 50);
            Meniu.Salveaza_Snake = true;
        }


        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            VerificareTaste.Apasat(e.KeyCode, true);
        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            VerificareTaste.Apasat(e.KeyCode, false);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            new Meniu();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

        }
    }
}