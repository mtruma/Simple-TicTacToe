using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace TicTacToe
{
    public class MainWindow : Init
    {
        private int moveCount = 0;
        private int PosX, PosY;
        private bool playerOnTheMove = true, playGame = true;
        private Random randNum = new Random();

        public MainWindow()
        {
            this.Resize += MainWindow_Resize;

            GamePlay();
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.Size = new Size(control.Size.Width, control.Size.Height);

            w = control.Size.Width;
            h = control.Size.Height;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (w >= h)
                    {
                        pb[i, j].Size = new Size(h / 3, h / 3);
                        pb[i, j].Location = new Point((w - h) / 2 + i * h / 3, j * h / 3);
                    }
                    else
                    {
                        pb[i, j].Size = new Size(w / 3, w / 3);
                        pb[i, j].Location = new Point(j * w / 3, (h - w) / 2 + i * w / 3);
                    }
                }
            }
            //pb_back.Size = new Size(w, h);
        }
        private void Player()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3;j++)
                {
                    if (pb[i,j].Image==null)
                        pb[i, j].MouseClick += Player_Click;
                }
                    
        }
        private void Player_Click(object sender, MouseEventArgs e)
        {
            if (playGame)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (sender.Equals(pb[i, j]))
                        {
                            PosX = i;
                            PosY = j;
                        }
                    }
                }
                if (playerOnTheMove)
                {
                    PictureBox pb_temp = (PictureBox)sender;
                    /*For animated gif*/
                    //anim[moveCount] = new AnimatedGif(pb_temp, "Player");
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    Stream stream = assembly.GetManifestResourceStream("TicTacToe.X.png");
                    Image img = Image.FromStream(stream);
                    //pb_temp.BackColor = Color.Green;
                    pb_temp.Image = new Bitmap(img);

                    pb_temp.Tag = "Player";

                    moveCount++;
                    playerOnTheMove = false;
                    GameFinished();
                    cTimer.Start();
                    pTimer.Stop();
                    pb_temp.MouseClick -= Player_Click;
                }
            }
        }

        private void Check_available_place()
        {
            RandFreePlace();
            for (int i = 0; i < 3;i++)
                CheckFreePlace(i);
            CheckDiagonals();
        }

        int temp0 = 0, temp1 = 1, temp2 = 2;
        private void CheckFreePlace(int a)
        {
            //Check Rows
            if (pb[a, temp0].Image == null && pb[a, temp1].Image != null && pb[a, temp2].Image != null)
            {
                if (pb[a, temp1].Tag.ToString() == "Player" && pb[a, temp2].Tag.ToString() == "Player")
                {
                    PosX = a;
                    PosY = temp0;
                }
                if (pb[a, temp1].Tag.ToString() == "Computer" && pb[a, temp2].Tag.ToString() == "Computer")
                {
                    PosX = a;
                    PosY = temp0;
                }
            }
            if (pb[a, temp0].Image != null && pb[a, temp1].Image == null && pb[a, temp2].Image != null)
            {
                if (pb[a, temp0].Tag.ToString() == "Player" && pb[a, temp2].Tag.ToString() == "Player")
                {
                    PosX = a;
                    PosY = temp1;
                }
                if (pb[a, temp0].Tag.ToString() == "Computer" && pb[a, temp2].Tag.ToString() == "Computer")
                {
                    PosX = a;
                    PosY = temp1;
                }
            }
            if (pb[a, temp0].Image != null && pb[a, temp1].Image != null && pb[a, temp2].Image == null)
            {
                if (pb[a, temp0].Tag.ToString() == "Player" && pb[a, temp1].Tag.ToString() == "Player")
                {
                    PosX = a;
                    PosY = temp2;
                }
                if (pb[a, temp0].Tag.ToString() == "Computer" && pb[a, temp1].Tag.ToString() == "Computer")
                {
                    PosX = a;
                    PosY = temp2;
                }
            }
            //Check Columns
            if (pb[temp0, a].Image == null && pb[temp1, a].Image != null && pb[temp2, a].Image != null)
            {
                if (pb[temp1, a].Tag.ToString() == "Player" && pb[temp2, a].Tag.ToString() == "Player")
                {
                    PosX = temp0;
                    PosY = a;
                }
                if (pb[temp1, a].Tag.ToString() == "Computer" && pb[temp2, a].Tag.ToString() == "Computer")
                {
                    PosX = temp0;
                    PosY = a;
                }
            }
            if (pb[temp0, a].Image != null && pb[temp1, a].Image == null && pb[temp2, a].Image != null)
            {
                if (pb[temp0, a].Tag.ToString() == "Player" && pb[temp2, a].Tag.ToString() == "Player")
                {
                    PosX = temp1;
                    PosY = a;
                }
                if (pb[temp0, a].Tag.ToString() == "Computer" && pb[temp2, a].Tag.ToString() == "Computer")
                {
                    PosX = temp1;
                    PosY = a;
                }
            }
            if (pb[temp0, a].Image != null && pb[temp1, a].Image != null && pb[temp2, a].Image == null)
            {
                if (pb[temp0, a].Tag.ToString() == "Player" && pb[temp1, a].Tag.ToString() == "Player")
                {
                    PosX = temp2;
                    PosY = a;
                }
                if (pb[temp0, a].Tag.ToString() == "Computer" && pb[temp1, a].Tag.ToString() == "Computer")
                {
                    PosX = temp2;
                    PosY = a;
                }
            }
        }
        private void CheckDiagonals()
        {
            if (pb[0, 0].Image == null && pb[1, 1].Image != null && pb[2, 2].Image != null)
            {
                if (pb[1, 1].Tag.ToString() == "Player" && pb[2, 2].Tag.ToString() == "Player")
                {
                    PosX = 0;
                    PosY = 0;
                }
            }
            if (pb[0, 0].Image != null && pb[1, 1].Image == null && pb[2, 2].Image != null)
            {
                if (pb[0, 0].Tag.ToString() == "Player" && pb[2, 2].Tag.ToString() == "Player")
                {
                    PosX = 1;
                    PosY = 1;
                }
            }
            if (pb[0, 0].Image != null && pb[1, 1].Image != null && pb[2, 2].Image == null)
            {
                if (pb[0, 0].Tag.ToString() == "Player" && pb[1, 1].Tag.ToString() == "Player")
                {
                    PosX = 2;
                    PosY = 2;
                }
            }
            if (pb[0, 2].Image == null && pb[1, 1].Image != null && pb[2, 0].Image != null)
            {
                if (pb[1, 1].Tag.ToString() == "Player" && pb[2, 0].Tag.ToString() == "Player")
                {
                    PosX = 0;
                    PosY = 2;
                }
            }
            if (pb[0, 2].Image != null && pb[1, 1].Image == null && pb[2, 0].Image != null)
            {
                if (pb[0, 2].Tag.ToString() == "Player" && pb[2, 0].Tag.ToString() == "Player")
                {
                    PosX = 1;
                    PosY = 1;
                }
            }
            if (pb[0, 2].Image != null && pb[1, 1].Image != null && pb[2, 0].Image == null)
            {
                if (pb[0, 2].Tag.ToString() == "Player" && pb[1, 1].Tag.ToString() == "Player")
                {
                    PosX = 2;
                    PosY = 0;
                }
            }

        }

        private void CheckCorners ()
        {
            if (pb[0, 0].Image == null)
            {
                
                PosX = 0;
                PosY = 0;
            }
            else if (pb[0, 2].Image == null)
            {
                PosX = 0;
                PosY = 2;
            }
            else if (pb[2, 2].Image == null)
            {
                PosX = 2;
                PosY = 2;
            }
            else if (pb[2, 0].Image == null)
            {
                PosX = 2;
                PosY = 0;
            }
        }
        private void RandFreePlace()
        {
            for (int i = 0; i < 3;i++)
                for (int j = 0; j < 3;j++)
                    if (pb[i,j].Image == null)
                    {
                        PosX = i;
                        PosY = j;
                    }
        }

        private String FindWinner()
        {
            for (int i = 0; i < 3;i++)
            {
                //Rows
                if (pb[i,0].Tag.ToString()=="Player" && pb[i, 1].Tag.ToString() == "Player" && pb[i, 2].Tag.ToString() == "Player")
                    return "Player";
                
                if (pb[i, 0].Tag.ToString() == "Computer" && pb[i, 1].Tag.ToString() == "Computer" && pb[i, 2].Tag.ToString() == "Computer")
                    return "Computer";
                
                //Columns
                if (pb[0, i].Tag.ToString() == "Player" && pb[1, i].Tag.ToString() == "Player" && pb[2, i].Tag.ToString() == "Player")
                    return "Player";
                
                if (pb[0, i].Tag.ToString() == "Computer" && pb[1, i].Tag.ToString() == "Computer" && pb[2, i].Tag.ToString() == "Computer")
                    return "Computer";
            }
            //Diagonals
            if (pb[0, 0].Tag.ToString() == "Player" && pb[1, 1].Tag.ToString() == "Player" && pb[2, 2].Tag.ToString() == "Player")
                return "Player";
            
            if (pb[0, 0].Tag.ToString() == "Computer" && pb[1, 1].Tag.ToString() == "Computer" && pb[2, 2].Tag.ToString() == "Computer")
                return "Computer";

            if (pb[0, 2].Tag.ToString() == "Player" && pb[1, 1].Tag.ToString() == "Player" && pb[2, 0].Tag.ToString() == "Player")
                return "Player";
            
            if (pb[0, 2].Tag.ToString() == "Computer" && pb[1, 1].Tag.ToString() == "Computer" && pb[2, 0].Tag.ToString() == "Computer")
                return "Computer";

            if (moveCount < 9)
                return "-1";
            else
                return "Draw";
            
        }

        int posx = -1, posy = -1;
        private void ComputerAI (object sender, EventArgs e)
        {
            if (!playerOnTheMove && playGame)
            {
                if (moveCount == 1)
                {

                    posx = randNum.Next(0, 3);
                    posy = randNum.Next(0, 3);
                    if (posx == PosX && PosY == posy)
                    {
                        posx = randNum.Next(0, 3);
                        posy = randNum.Next(0, 3);
                    }
                    else
                    {
                        /*For animated gif*/
                        //anim[moveCount] = new AnimatedGif(pb[posx, posy], "Computer");
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        Stream stream = assembly.GetManifestResourceStream("TicTacToe.O.png");
                        Image img = Image.FromStream(stream);

                        pb[posx, posy].Image = new Bitmap(img);
                        pb[posx, posy].Tag = "Computer";

                        pb[posx, posy].MouseClick -= Player_Click;
                        moveCount++;
                        cTimer.Stop();
                        pTimer.Start();
                        playerOnTheMove = true;
                    }
                }
                else if (moveCount<9)
                {
                    Check_available_place();
                    /*For animated gif*/
                    //anim[moveCount] = new AnimatedGif(pb[PosX, PosY], "Computer");
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    Stream stream = assembly.GetManifestResourceStream("TicTacToe.O.png");
                    Image img = Image.FromStream(stream);

                    pb[PosX, PosY].Image = new Bitmap(img);
                    pb[PosX, PosY].Tag = "Computer";

                    pb[PosX, PosY].MouseClick -= Player_Click;
                    moveCount++;
                    cTimer.Stop();
                    pTimer.Start();
                    playerOnTheMove = true;
                    GameFinished();
                }
            }
        }

        private void GameFinished()
        {
            

            switch(FindWinner())
            {
                case "Player":
                    playGame = false;
                    lbl.Text = "Player Wins";
                    break;
                case "Computer":
                    playGame = false;
                    lbl.Text = "Computer Wins";
                    break;
                case "Draw":
                    playGame = false;
                    lbl.Text = "Draw";
                    break;
                default:
                    playGame = true;
                    break;
            }
        }

        private void GamePlay()
        {
            Player();
            cTimer.Tick += new EventHandler(ComputerAI);
        }
	}
}

