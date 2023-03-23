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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public List<PictureBox> Piesa = new List<PictureBox>();
        public List<PictureBox> Piesa_Next = new List<PictureBox>();
        PictureBox Suprafata_de_Joc = new PictureBox();
        PictureBox Piesa_Urmatoare = new PictureBox();
        PictureBox[,] Mapa = new PictureBox[20, 20];
        PictureBox Seteaza = new PictureBox();

        Label Scor = new Label();
        Label Nivel = new Label();
        Label Scor_Maxim = new Label();
        Label Linii_Curatate = new Label();
        Label Scris = new Label();
        Label Sfarsit = new Label();

        string Teren = "/Tetris Game Background.png";
        string Fundal = "/Tetris Game Next Piece.png";
        string Highscore = "Scor maxim: ";
        string Score = "Scor: ";
        string Lines_Cleared = "Linii curatate: ";
        string Setam = "/Salveaza Tetris.png";

        string Incepe = "Pentru a incepe apasa \n   Space sau Enter \n";
        string Terminat = "       Joc terminat \n";
        string Regula1 = "Space - Cadere Instanta";
        string Regula0 = "W/Up - Rotire Piesa";
        string Regula2 = "S/Down - Cadere Rapida";
        string Regula3 = "A/Left - Stanga";
        string Regula4 = "D/Right - Dreapta";

        int[,] Cadran = new int[20,20];

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Height = 725;
            this.Width = 600;
            this.Location = new Point(100, 50);

            Suprafata_de_Joc.Image = Image.FromFile(Application.StartupPath + Teren);
            Suprafata_de_Joc.Height = 621;
            Suprafata_de_Joc.Width = 321;
            Suprafata_de_Joc.Location = new Point(25, 25);
            this.Controls.Add(Suprafata_de_Joc);

            Piesa_Urmatoare.Image = Image.FromFile(Application.StartupPath + Fundal);
            Piesa_Urmatoare.Height = 105;
            Piesa_Urmatoare.Width = 136;
            Piesa_Urmatoare.Location = new Point(375, 25);
            this.Controls.Add(Piesa_Urmatoare);

            new Setari_Tetris();
            Setari_Tetris.JocTerminat = true;

            Scor_Maxim.Location = new Point(368, 150);
            Scor_Maxim.BackColor = Color.Transparent;
            Scor_Maxim.ForeColor = Color.DarkBlue;
            Scor_Maxim.Text = Highscore + "0";
            Scor_Maxim.Height = 50;
            Scor_Maxim.Width = 500;
            Scor_Maxim.Font = new Font("DIGIFACE", 20);
            this.Controls.Add(Scor_Maxim);

            Scor.Location = new Point(368, 200);
            Scor.BackColor = Color.Transparent;
            Scor.ForeColor = Color.DarkBlue;
            Scor.Text = Score + "0";
            Scor.Height = 50;
            Scor.Width = 500;
            Scor.Font = new Font("DIGIFACE", 20);
            this.Controls.Add(Scor);

            Linii_Curatate.Location = new Point(368, 250);
            Linii_Curatate.BackColor = Color.Transparent;
            Linii_Curatate.ForeColor = Color.DarkBlue;
            Linii_Curatate.Text = Lines_Cleared + "0";
            Linii_Curatate.Height = 50;
            Linii_Curatate.Width = 500;
            Linii_Curatate.Font = new Font("DIGIFACE", 20);
            this.Controls.Add(Linii_Curatate);

            Scris.Location = new Point(368, 350);
            Scris.BackColor = Color.Transparent;
            Scris.ForeColor = Color.DarkBlue;
            Scris.Text = Incepe + "\n" + Regula0 + "\n" + Regula3 + "\n" + Regula4 + "\n" + Regula1 + "\n" + Regula2;
            Scris.Height = 500;
            Scris.Width = 500;
            Scris.Font = new Font("DIGIFACE", 15);
            this.Controls.Add(Scris);

            Seteaza.Location = new Point(400, 300);
            Seteaza.Height = 30;
            Seteaza.Width = 125;
            Seteaza.Image = Image.FromFile(Application.StartupPath + Setam);
            Seteaza.Click += new EventHandler(Seteaza_Click);

            Timer.Interval = Setari_Tetris.Viteza_Detectare;
            Timer.Tick += (s, ev) => { Miscare(s, ev); };
            Timer.Start();

            Timer_Cadere.Interval = Setari_Tetris.Viteza_Cadere;
            Timer_Cadere.Tick += (s, ev) => { Cadere(s, ev); };
            Timer_Cadere.Start();

            if (VerificareTaste.Verificare(Keys.Enter) || VerificareTaste.Verificare(Keys.Space))
                Start();
        }

        public void Start()
        {
            new Setari_Tetris();
            Suprafata_de_Joc.Controls.Clear();
            Cadran = new int[20, 20];

            Random FormaRandom = new Random();
            Setari_Tetris.Urmatoarea_Piesa = FormaRandom.Next(1, 8);
            Setari_Tetris.Urmatoarea_Culoare = FormaRandom.Next(1, 8);
            Scris.Text = "";

            for (int i = 0; i < 4; i++)
            {
                Piesa.Add(new PictureBox());
                Piesa[i].Width = 30;
                Piesa[i].Height = 30;
                Suprafata_de_Joc.Controls.Add(Piesa[i]);
            }

            for (int i = 0; i < 4; i++)
            {
                Piesa_Next.Add(new PictureBox());
                Piesa_Next[i].Width = 30;
                Piesa_Next[i].Height = 30;
                Piesa_Urmatoare.Controls.Add(Piesa_Next[i]);
            }
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 10; j++)
                {
                    Mapa[j, i] = new PictureBox();
                    Mapa[j, i].Width = 30;
                    Mapa[j, i].Height = 30;
                    Mapa[j, i].BackColor = Color.Transparent;
                }

            Generare_Tetromino();
            this.Controls.Remove(Seteaza);
        }

        public void Generare_Tetromino()
        {
            Random FormaRandom = new Random();
            Setari_Tetris.Tip = Setari_Tetris.Urmatoarea_Piesa;
            Setari_Tetris.Culoare = Setari_Tetris.Urmatoarea_Culoare;
            Setari_Tetris.Urmatoarea_Piesa = FormaRandom.Next(1, 8); // 1 = I || 2 = O || 3 = T || 4 = J || 5 = L || 6 = S || 7 = Z
            Setari_Tetris.Urmatoarea_Culoare = FormaRandom.Next(1, 8);
            Setari_Tetris.Rotatie_Curenta = 1;

            Urmatoarea_Piesa();

            string Poza = "/" + Setari_Tetris.Culoare.ToString() + " Tetris Game.png";

            int x = 63, y = 32;

            for (int i = 0; i < 4; i++)
                Piesa[i].Image = Image.FromFile(Application.StartupPath + Poza);

            if (Setari_Tetris.Tip == 1) // I
            {
                for (int i = 0; i < 4; i++)
                    Piesa[i].Location = new Point(x += 31, y);
            }
            else if (Setari_Tetris.Tip == 2) // O
            {
                x = 94;
                y = 1;
                Piesa[0].Location = new Point(x += 31, y);
                Piesa[1].Location = new Point(x += 31, y);
                Piesa[3].Location = new Point(x, y += 31);
                Piesa[2].Location = new Point(x -= 31, y);
            }
            else if (Setari_Tetris.Tip == 3) // T
            {
                for (int i = 0; i < 3; i++)
                    Piesa[i].Location = new Point(x += 31, y);
                Piesa[3].Location = new Point(x -= 31, y -= 31);
            }
            else if (Setari_Tetris.Tip == 4) // J
            {
                for (int i = 1; i < 4; i++)
                    Piesa[i].Location = new Point(x += 31, y);
                Piesa[0].Location = new Point(x -= 62, y -= 31);
            }
            else if (Setari_Tetris.Tip == 5) // L
            {
                for (int i = 1; i < 4; i++)
                    Piesa[i].Location = new Point(x += 31, y);
                Piesa[0].Location = new Point(x, y -= 31);
            }
            else if (Setari_Tetris.Tip == 6) // S
            {
                Piesa[0].Location = new Point(x += 31, y);
                Piesa[1].Location = new Point(x += 31, y);
                Piesa[2].Location = new Point(x, y -= 31);
                Piesa[3].Location = new Point(x += 31, y);
            }
            else if (Setari_Tetris.Tip == 7) // Z
            {
                y = 1;
                Piesa[0].Location = new Point(x += 31, y);
                Piesa[1].Location = new Point(x += 31, y);
                Piesa[2].Location = new Point(x, y += 31);
                Piesa[3].Location = new Point(x += 31, y);
            }
        }

        public void Miscare(object sender, EventArgs e)
        {
            if (Setari_Tetris.JocTerminat == false)
            {
                if (VerificareTaste.Verificare(Keys.Down) || VerificareTaste.Verificare(Keys.S))
                {
                    Timer_Cadere.Interval = 50;
                    Setari_Tetris.Punctaj++;
                }
                else
                    Timer_Cadere.Interval = Setari_Tetris.Viteza_Cadere;

                if (VerificareTaste.Verificare(Keys.Up) || VerificareTaste.Verificare(Keys.W))
                {
                    if (Setari_Tetris.Tip == 1) Rotatie_Tetromino_I();

                    else if (Setari_Tetris.Tip == 3) Rotatie_Tetromino_T();

                    else if (Setari_Tetris.Tip == 4) Rotatie_Tetromino_J();

                    else if (Setari_Tetris.Tip == 5) Rotatie_Tetromino_L();

                    else if (Setari_Tetris.Tip == 6) Rotatie_Tetromino_S();

                    else if (Setari_Tetris.Tip == 7) Rotatie_Tetromino_Z();

                    Verificare_Pozitie();
                }

                if (VerificareTaste.Verificare(Keys.Space))
                {
                    int Puncte = 0;
                    bool OK = true;
                    while (OK == true)
                    {
                        Puncte++;
                        for (int i = 0; i < 4 && OK == true; i++)
                            if (Piesa[i].Location.Y + 31 > 608 || Cadran[Piesa[i].Location.X / 31, Piesa[i].Location.Y / 31 + 1] != 0) OK = false;
                        if (OK == true)
                            for (int i = 0; i < 4; i++)
                                Piesa[i].Location = new Point(Piesa[i].Location.X, Piesa[i].Location.Y + 31);
                    }
                    Setari_Tetris.Punctaj += Puncte * 2 - Puncte / 2;
                }

                if (VerificareTaste.Verificare(Keys.Right) || VerificareTaste.Verificare(Keys.D))
                {
                    bool OK = true;
                    for (int i = 0; i < 4 && OK; i++)
                        if (Piesa[i].Location.X + 31 > 290 || Cadran[Piesa[i].Location.X / 31 + 1, Piesa[i].Location.Y / 31] != 0)
                            OK = false;
                    if (OK == true)
                        for (int i = 0; i < 4; i++)
                            Piesa[i].Location = new Point(Piesa[i].Location.X + 31, Piesa[i].Location.Y);
                }

                else if (VerificareTaste.Verificare(Keys.Left) || VerificareTaste.Verificare(Keys.A))
                {
                    bool OK = true;
                    for (int i = 0; i < 4 && OK; i++)
                        if (Piesa[i].Location.X - 31 < 1 || Cadran[Piesa[i].Location.X / 31 - 1, Piesa[i].Location.Y / 31] != 0)
                            OK = false;
                    if (OK == true)
                        for (int i = 0; i < 4; i++)
                            Piesa[i].Location = new Point(Piesa[i].Location.X - 31, Piesa[i].Location.Y);
                }
            }
            else if (VerificareTaste.Verificare(Keys.Enter) || VerificareTaste.Verificare(Keys.Space))
                    Start();
        }

        public void Verificare_Pozitie()
        {
            int OK = 1;
            while (OK != 0)
            {
                OK = 0;
                for (int i = 0; i < 4 && OK == 0; i++)
                    if (Piesa[i].Location.X > 290)
                        OK = 1;
                    else if (Piesa[i].Location.X < 1)
                        OK = 2;
                if (OK == 1)
                    for (int i = 0; i < 4; i++)
                        Piesa[i].Location = new Point(Piesa[i].Location.X - 31, Piesa[i].Location.Y);
                if (OK == 2)
                    for (int i = 0; i < 4; i++)
                        Piesa[i].Location = new Point(Piesa[i].Location.X + 31, Piesa[i].Location.Y);
            }
        }

        public void Cadere(object sender, EventArgs e)
        {
            if (Setari_Tetris.JocTerminat == false)
            {
                bool OK = true;
                for (int i = 0; i < 4 && OK; i++)
                    if (Piesa[i].Location.Y + 31 > 590 || Piesa[i].Location.Y + 31 < 1 || Cadran[Piesa[i].Location.X / 31, Piesa[i].Location.Y / 31 + 1] != 0)
                        OK = false;
                if (OK == true)
                    for (int i = 0; i < 4; i++)
                        Piesa[i].Location = new Point(Piesa[i].Location.X, Piesa[i].Location.Y + 31);
                else
                {
                    for (int i = 0; i < 10; i++)
                        if (Cadran[i, 0] != 0)
                        {
                            Setari_Tetris.JocTerminat = true;
                            if (Setari_Tetris.Punctaj > Setari_Tetris.Punctaj_Maxim)
                                Setari_Tetris.Punctaj_Maxim = Setari_Tetris.Punctaj;
                            Scor_Maxim.Text = Highscore + Setari_Tetris.Punctaj_Maxim.ToString();
                        }

                    if (Setari_Tetris.JocTerminat == false)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Cadran[Piesa[i].Location.X / 31, Piesa[i].Location.Y / 31] = 1;
                            Mapa[Piesa[i].Location.X / 31, Piesa[i].Location.Y / 31].Image = Piesa[i].Image;
                            Mapa[Piesa[i].Location.X / 31, Piesa[i].Location.Y / 31].Location = Piesa[i].Location;
                            Suprafata_de_Joc.Controls.Add(Mapa[Piesa[i].Location.X / 31, Piesa[i].Location.Y / 31]);
                        }
                        Curatare_Linie();
                        if (Setari_Tetris.Linii_Curatate >= 10 * Setari_Tetris.Nivel)
                        {
                            Setari_Tetris.Nivel++;
                            Timer_Cadere.Interval -= 5;
                        }
                        Generare_Tetromino();
                        Scor.Text = Score + Setari_Tetris.Punctaj.ToString();
                        Linii_Curatate.Text = Lines_Cleared + Setari_Tetris.Linii_Curatate.ToString();

                    }
                    else
                        Moarte();
                }
            }
        }

        public void Rotatie_Tetromino_I()
        {

            if (Setari_Tetris.Rotatie_Curenta == 1)
            {
                if (Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y - 31) / 31] == 0 && Cadran[Piesa[2].Location.X / 31, Piesa[2].Location.Y / 31] == 0 &&
                   Cadran[Piesa[2].Location.X / 31, (Piesa[1].Location.Y + 31) / 31] == 0 && Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y + 31) / 31]==0)
                {
                    Setari_Tetris.Rotatie_Curenta = 2;
                    Piesa[0].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y - 31);
                    Piesa[1].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y);
                    Piesa[2].Location = new Point(Piesa[2].Location.X, Piesa[1].Location.Y + 31);
                    Piesa[3].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y + 31);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 2)
            {
                if (Cadran[Piesa[1].Location.X / 31, Piesa[1].Location.Y / 31] == 0 && Cadran[(Piesa[2].Location.X + 31) / 31, Piesa[1].Location.Y / 31] == 0 &&
                    Cadran[(Piesa[2].Location.X - 31) / 31, Piesa[1].Location.Y / 31] == 0 && Cadran[(Piesa[1].Location.X - 31) / 31, Piesa[1].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 3;
                    Piesa[2].Location = new Point(Piesa[1].Location.X, Piesa[1].Location.Y);
                    Piesa[3].Location = new Point(Piesa[2].Location.X + 31, Piesa[1].Location.Y);
                    Piesa[1].Location = new Point(Piesa[2].Location.X - 31, Piesa[1].Location.Y);
                    Piesa[0].Location = new Point(Piesa[1].Location.X - 31, Piesa[1].Location.Y);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 3)
            {
                if (Cadran[Piesa[1].Location.X / 31, (Piesa[1].Location.Y - 31) / 31] == 0 && Cadran[Piesa[1].Location.X / 31, (Piesa[1].Location.Y + 31) / 31] == 0 &&
                    Cadran[Piesa[1].Location.X / 31, (Piesa[2].Location.Y + 31) / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 4;
                    Piesa[0].Location = new Point(Piesa[1].Location.X, Piesa[1].Location.Y - 31);
                    Piesa[2].Location = new Point(Piesa[1].Location.X, Piesa[1].Location.Y + 31);
                    Piesa[3].Location = new Point(Piesa[1].Location.X, Piesa[2].Location.Y + 31);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 4)
            {
                if (Cadran[(Piesa[1].Location.X - 31) / 31, Piesa[1].Location.Y / 31] == 0 && Cadran[(Piesa[1].Location.X + 31) / 31, Piesa[1].Location.Y / 31] == 0 &&
                    Cadran[(Piesa[2].Location.X + 31) / 31, Piesa[1].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 1;
                    Piesa[0].Location = new Point(Piesa[1].Location.X - 31, Piesa[1].Location.Y);
                    Piesa[2].Location = new Point(Piesa[1].Location.X + 31, Piesa[1].Location.Y);
                    Piesa[3].Location = new Point(Piesa[2].Location.X + 31, Piesa[1].Location.Y);
                }
            }
        }

        public void Rotatie_Tetromino_T()
        {
            if (Setari_Tetris.Rotatie_Curenta == 1)
            {
                if (Cadran[Piesa[1].Location.X / 31, (Piesa[1].Location.Y + 31) / 31] == 0)
                {
                    Piesa[0].Location = new Point(Piesa[1].Location.X, Piesa[1].Location.Y + 31);
                    Setari_Tetris.Rotatie_Curenta = 2;
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 2)
            {
                if (Cadran[(Piesa[1].Location.X - 31) / 31, Piesa[1].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 3;
                    Piesa[3].Location = new Point(Piesa[1].Location.X - 31, Piesa[1].Location.Y);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 3)
            {
                if (Cadran[Piesa[1].Location.X / 31, (Piesa[1].Location.Y - 31) / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 4;
                    Piesa[2].Location = new Point(Piesa[1].Location.X, Piesa[1].Location.Y - 31);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 4)
            {
                if (Cadran[(Piesa[1].Location.X + 31) / 31, Piesa[1].Location.Y / 31] == 0 && Cadran[Piesa[3].Location.X / 31, Piesa[3].Location.Y / 31] == 0 &&
                    Cadran[Piesa[2].Location.X / 31, Piesa[2].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 1;
                    Piesa[0].Location = new Point(Piesa[3].Location.X, Piesa[3].Location.Y);
                    Piesa[3].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y);
                    Piesa[2].Location = new Point(Piesa[1].Location.X + 31, Piesa[1].Location.Y);
                }
            }
        }

        public void Rotatie_Tetromino_J()
        {
            if (Setari_Tetris.Rotatie_Curenta == 1)
            {
                if (Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y - 31) / 31] == 0 && Cadran[(Piesa[1].Location.X + 31) / 31, Piesa[1].Location.Y / 31] == 0 &&
                    Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y + 31) / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 2;
                    Piesa[1].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y - 31);
                    Piesa[0].Location = new Point(Piesa[1].Location.X + 31, Piesa[1].Location.Y);
                    Piesa[3].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y + 31);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 2)
            {
                if (Cadran[(Piesa[2].Location.X + 31) / 31, Piesa[2].Location.Y / 31] == 0 && Cadran[Piesa[1].Location.X / 31, (Piesa[1].Location.Y + 31) / 31] == 0 &&
                    Cadran[(Piesa[2].Location.X - 31) / 31, Piesa[2].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 3;
                    Piesa[1].Location = new Point(Piesa[2].Location.X + 31, Piesa[2].Location.Y);
                    Piesa[0].Location = new Point(Piesa[1].Location.X, Piesa[1].Location.Y + 31);
                    Piesa[3].Location = new Point(Piesa[2].Location.X - 31, Piesa[2].Location.Y);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 3)
            {
                if (Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y + 31) / 31] == 0 && Cadran[(Piesa[1].Location.X - 31) / 31, Piesa[1].Location.Y / 31] == 0 &&
                    Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y - 31) / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 4;
                    Piesa[1].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y + 31);
                    Piesa[0].Location = new Point(Piesa[1].Location.X - 31, Piesa[1].Location.Y);
                    Piesa[3].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y - 31);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 4)
            {
                if (Cadran[(Piesa[2].Location.X - 31) / 31, Piesa[2].Location.Y / 31] == 0 && Cadran[Piesa[1].Location.X / 31, (Piesa[1].Location.Y - 31) / 31] == 0 &&
                    Cadran[(Piesa[2].Location.X + 31) / 31, Piesa[2].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 1;
                    Piesa[1].Location = new Point(Piesa[2].Location.X - 31, Piesa[2].Location.Y);
                    Piesa[0].Location = new Point(Piesa[1].Location.X, Piesa[1].Location.Y - 31);
                    Piesa[3].Location = new Point(Piesa[2].Location.X + 31, Piesa[2].Location.Y);
                }
            }
        }

        public void Rotatie_Tetromino_L()
        {
            if (Setari_Tetris.Rotatie_Curenta == 1)
            {
                if (Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y - 31) / 31] == 0 && Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y + 31) / 31] == 0 &&
                    Cadran[(Piesa[3].Location.X + 31) / 31, Piesa[3].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 2;
                    Piesa[1].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y - 31);
                    Piesa[3].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y + 31);
                    Piesa[0].Location = new Point(Piesa[3].Location.X + 31, Piesa[3].Location.Y);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 2)
            {
                if (Cadran[(Piesa[2].Location.X + 31) / 31, Piesa[2].Location.Y / 31] == 0 && Cadran[(Piesa[2].Location.X - 31) / 31, Piesa[2].Location.Y / 31] == 0 &&
                    Cadran[Piesa[3].Location.X / 31, (Piesa[3].Location.Y + 31) / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 3;
                    Piesa[1].Location = new Point(Piesa[2].Location.X + 31, Piesa[2].Location.Y);
                    Piesa[3].Location = new Point(Piesa[2].Location.X - 31, Piesa[2].Location.Y);
                    Piesa[0].Location = new Point(Piesa[3].Location.X, Piesa[3].Location.Y + 31);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 3)
            {
                if (Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y + 31) / 31] == 0 && Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y - 31) / 31] == 0 &&
                    Cadran[(Piesa[3].Location.X - 31) / 31, Piesa[3].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 4;
                    Piesa[1].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y + 31);
                    Piesa[3].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y - 31);
                    Piesa[0].Location = new Point(Piesa[3].Location.X - 31, Piesa[3].Location.Y);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 4)
            {
                if (Cadran[(Piesa[2].Location.X - 31) / 31, Piesa[2].Location.Y / 31] == 0 && Cadran[(Piesa[2].Location.X + 31) / 31, Piesa[2].Location.Y / 31] == 0 &&
                    Cadran[Piesa[3].Location.X / 31, (Piesa[3].Location.Y - 31) / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 1;
                    Piesa[1].Location = new Point(Piesa[2].Location.X - 31, Piesa[2].Location.Y);
                    Piesa[3].Location = new Point(Piesa[2].Location.X + 31, Piesa[2].Location.Y);
                    Piesa[0].Location = new Point(Piesa[3].Location.X, Piesa[3].Location.Y - 31);
                }
            }
        }

        public void Rotatie_Tetromino_S()
        {
            if (Setari_Tetris.Rotatie_Curenta == 1)
            {
                if (Cadran[Piesa[2].Location.X / 31, Piesa[2].Location.Y / 31] == 0 && Cadran[(Piesa[1].Location.X + 31) / 31, Piesa[1].Location.Y / 31] == 0 &&
                    Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y + 31) / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 2;
                    Piesa[0].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y);
                    Piesa[2].Location = new Point(Piesa[1].Location.X + 31, Piesa[1].Location.Y);
                    Piesa[3].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y + 31);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 2)
            {
                if (Cadran[Piesa[2].Location.X / 31, Piesa[2].Location.Y / 31] == 0 && Cadran[Piesa[1].Location.X / 31, (Piesa[1].Location.Y + 31) / 31] == 0 &&
                    Cadran[(Piesa[2].Location.X - 31) / 31, Piesa[2].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 3;
                    Piesa[0].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y);
                    Piesa[2].Location = new Point(Piesa[1].Location.X, Piesa[1].Location.Y + 31);
                    Piesa[3].Location = new Point(Piesa[2].Location.X - 31, Piesa[2].Location.Y);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 3)
            {
                if (Cadran[Piesa[2].Location.X / 31, Piesa[2].Location.Y / 31] == 0 && Cadran[(Piesa[1].Location.X - 31) / 31, Piesa[1].Location.Y / 31] == 0 &&
                    Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y - 31) / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 4;
                    Piesa[0].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y);
                    Piesa[2].Location = new Point(Piesa[1].Location.X - 31, Piesa[1].Location.Y);
                    Piesa[3].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y - 31);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 4)
            {
                if (Cadran[Piesa[2].Location.X / 31, Piesa[2].Location.Y / 31] == 0 && Cadran[Piesa[1].Location.X / 31, (Piesa[1].Location.Y - 31) / 31] == 0 &&
                    Cadran[(Piesa[2].Location.X + 31) / 31, Piesa[2].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 1;
                    Piesa[0].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y);
                    Piesa[2].Location = new Point(Piesa[1].Location.X, Piesa[1].Location.Y - 31);
                    Piesa[3].Location = new Point(Piesa[2].Location.X + 31, Piesa[2].Location.Y);
                }
            }
        }

        public void Rotatie_Tetromino_Z()
        {
            if (Setari_Tetris.Rotatie_Curenta == 1)
            {
                if (Cadran[Piesa[1].Location.X / 31, (Piesa[1].Location.Y - 31) / 31] == 0 && Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y + 31) / 31] == 0 &&
                    Cadran[Piesa[3].Location.X / 31, Piesa[3].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 2;
                    Piesa[1].Location = new Point(Piesa[3].Location.X, Piesa[3].Location.Y);
                    Piesa[0].Location = new Point(Piesa[1].Location.X, Piesa[1].Location.Y - 31);
                    Piesa[3].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y + 31);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 2)
            {
                if (Cadran[(Piesa[1].Location.X + 31) / 31, Piesa[1].Location.Y / 31] == 0 && Cadran[(Piesa[2].Location.X - 31) / 31, Piesa[2].Location.Y / 31] == 0 &&
                    Cadran[Piesa[3].Location.X / 31, Piesa[3].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 3;
                    Piesa[1].Location = new Point(Piesa[3].Location.X, Piesa[3].Location.Y);
                    Piesa[0].Location = new Point(Piesa[1].Location.X + 31, Piesa[1].Location.Y);
                    Piesa[3].Location = new Point(Piesa[2].Location.X - 31, Piesa[2].Location.Y);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 3)
            {
                if (Cadran[Piesa[1].Location.X / 31, (Piesa[1].Location.Y + 31) / 31] == 0 && Cadran[Piesa[2].Location.X / 31, (Piesa[2].Location.Y - 31) / 31] == 0 &&
                    Cadran[Piesa[3].Location.X / 31, Piesa[3].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 4;
                    Piesa[1].Location = new Point(Piesa[3].Location.X, Piesa[3].Location.Y);
                    Piesa[0].Location = new Point(Piesa[1].Location.X, Piesa[1].Location.Y + 31);
                    Piesa[3].Location = new Point(Piesa[2].Location.X, Piesa[2].Location.Y - 31);
                }
            }
            else if (Setari_Tetris.Rotatie_Curenta == 4)
            {
                if (Cadran[(Piesa[1].Location.X - 31) / 31, Piesa[1].Location.Y / 31] == 0 && Cadran[(Piesa[2].Location.X + 31) / 31, Piesa[2].Location.Y / 31] == 0
                    && Cadran[Piesa[3].Location.X / 31, Piesa[3].Location.Y / 31] == 0)
                {
                    Setari_Tetris.Rotatie_Curenta = 1;
                    Piesa[1].Location = new Point(Piesa[3].Location.X, Piesa[3].Location.Y);
                    Piesa[0].Location = new Point(Piesa[1].Location.X - 31, Piesa[1].Location.Y);
                    Piesa[3].Location = new Point(Piesa[2].Location.X + 31, Piesa[2].Location.Y);
                }
            }
        }

        private void Curatare_Linie()
        {
            int Linii = 0;
            for(int i=0;i<20;i++)
            {
                bool OK = true;
                for (int j = 0; j < 10 && OK == true; j++)
                    if (Cadran[j, i] == 0) OK = false;
                if(OK==true)
                {
                    Linii++;
                    for (int j = 0; j < 10; j++)
                        Cadran[j, i] = 0;
                    for (int k = i; k > 0; k--)
                        for (int j = 0; j < 10; j++)
                        {
                            Cadran[j, k] = Cadran[j, k - 1];
                            Mapa[j, k].Image = Mapa[j, k - 1].Image;
                        }
                }
            }

            if (Linii == 1) Setari_Tetris.Punctaj += 100 * Setari_Tetris.Nivel;
            else if (Linii == 2) Setari_Tetris.Punctaj += 400 * Setari_Tetris.Nivel;
            else if (Linii == 3) Setari_Tetris.Punctaj += 900 * Setari_Tetris.Nivel;
            else if (Linii == 4) Setari_Tetris.Punctaj += 2000 * Setari_Tetris.Nivel;
            Setari_Tetris.Linii_Curatate += Linii;
        }

        public void Moarte()
        {
            Suprafata_de_Joc.Controls.Clear();
            Piesa_Urmatoare.Controls.Clear();
            Scris.Text = Terminat + Incepe;
            this.Controls.Add(Seteaza);
        }

        public void Urmatoarea_Piesa()
        {
            int x = -13, y = 48;
            string Poza = "/" + Setari_Tetris.Urmatoarea_Culoare.ToString() + " Tetris Game.png";

            for (int i = 0; i < 4; i++)
                Piesa_Next[i].Image = Image.FromFile(Application.StartupPath + Poza);

            if (Setari_Tetris.Urmatoarea_Piesa == 1) // I
            {
                x = -22;
                for (int i = 0; i < 4; i++)
                    Piesa_Next[i].Location = new Point(x += 30, y);
            }
            else if (Setari_Tetris.Urmatoarea_Piesa == 2) // O
            {
                x = 9;
                y = 25;
                Piesa_Next[0].Location = new Point(x += 30, y);
                Piesa_Next[1].Location = new Point(x += 30, y);
                Piesa_Next[3].Location = new Point(x, y += 30);
                Piesa_Next[2].Location = new Point(x -= 30, y);
            }
            else if (Setari_Tetris.Urmatoarea_Piesa == 3) // T
            {
                for (int i = 0; i < 3; i++)
                    Piesa_Next[i].Location = new Point(x += 30, y);
                Piesa_Next[3].Location = new Point(x -= 30, y -= 30);
            }
            else if (Setari_Tetris.Urmatoarea_Piesa == 4) // J
            {
                for (int i = 1; i < 4; i++)
                    Piesa_Next[i].Location = new Point(x += 30, y);
                Piesa_Next[0].Location = new Point(x -= 60, y -= 30);
            }
            else if (Setari_Tetris.Urmatoarea_Piesa == 5) // L
            {
                for (int i = 1; i < 4; i++)
                    Piesa_Next[i].Location = new Point(x += 30, y);
                Piesa_Next[0].Location = new Point(x, y -= 30);
            }
            else if (Setari_Tetris.Urmatoarea_Piesa == 6) // S
            {
                Piesa_Next[0].Location = new Point(x += 30, y);
                Piesa_Next[1].Location = new Point(x += 30, y);
                Piesa_Next[2].Location = new Point(x, y -= 30);
                Piesa_Next[3].Location = new Point(x += 30, y);
            }
            else if (Setari_Tetris.Urmatoarea_Piesa == 7) // Z
            {
                y = 25;
                Piesa_Next[0].Location = new Point(x += 30, y);
                Piesa_Next[1].Location = new Point(x += 30, y);
                Piesa_Next[2].Location = new Point(x, y += 30);
                Piesa_Next[3].Location = new Point(x += 30, y);
            }
        }

        private void Seteaza_Click(object sender, EventArgs e)
        {
            Form4 Salveaza;
            Salveaza = new Form4();
            Salveaza.Show();
            Meniu.OK_Tetris = false;
            Meniu.OK_Snake = false;
            this.Location = new Point(900, 300);
            this.Controls.Remove(Seteaza);
            this.Location = new Point(100, 50);
            Meniu.Salveaza_Tetris = true;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            new Meniu();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            VerificareTaste.Apasat(e.KeyCode, true);
        }

        private void Timer_Cadere_Tick(object sender, EventArgs e)
        {

        }

        private void Form3_KeyUp(object sender, KeyEventArgs e)
        {
            VerificareTaste.Apasat(e.KeyCode, false);
        }
    }
}
