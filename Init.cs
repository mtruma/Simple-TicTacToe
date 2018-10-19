using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace TicTacToe
{
    public class Init : Form
    {
        //Declarations
        public PictureBox[,] pb;
        /*For animated Gif*/
        //public AnimatedGif[] anim;
        public Timer pTimer;
        public Timer cTimer;
        public int animationSpeed;
        public int w = 800, h = 800;
        public Label lbl;

        public Init()
        {
            //Window properties
            this.Text = "TicTacToe";
            this.Size = new Size(w, h);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream("TicTacToe.Table.png");
            Image img = Image.FromStream(stream);

            this.BackgroundImage = new Bitmap(img);
            this.BackgroundImageLayout = ImageLayout.Stretch;

            lbl = new Label();
            //lbl.BringToFront();
            lbl.Font = new Font("Arial", 18, FontStyle.Bold);
            lbl.Text = "";
            lbl.ForeColor = Color.White;
            lbl.BackColor = Color.Transparent;
            lbl.Location = new Point(5,0);
            lbl.Size = new Size(300, 35);
            this.Controls.Add(lbl);
            pTimer = new Timer();
            pTimer.Interval = 33;
            pTimer.Start();

            cTimer = new Timer();
            cTimer.Interval = 800;
            cTimer.Start();

            /*For animated Gif*/
            //anim = new AnimatedGif[15];

            pb = new PictureBox[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    pb[i, j] = new PictureBox();
                    this.Controls.Add(pb[i, j]);
                    pb[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    pb[i, j].Tag = "";//default tag
                    if (w >= h)
                    {
                        pb[i, j].Size = new Size(h / 3, h / 3);
                        pb[i, j].Location = new Point((w - h) / 2 + j * h / 3, i * h / 3);
                        pb[i, j].BackColor = Color.Transparent;
                    }
                    else
                    {
                        pb[i, j].Size = new Size(w / 3, w / 3);
                        pb[i, j].Location = new Point((h - w) / 2 + i * w / 3, j * w / 3);
                        pb[i, j].BackColor = Color.Transparent;
                    }
                }
            }
        }
    }
}
