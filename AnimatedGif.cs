using System;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TicTacToe
{
    public class AnimatedGif
    {
        public Timer anim_t;
        public AnimatedGif(PictureBox pic_box, String s)
        {
            if (s == "Player")
            {
                Create("TicTacToe.giphy.gif", pic_box, "Player");
            }
            else if (s == "Computer") 
            {
                Create("TicTacToe.giphyy.gif", pic_box, "Computer");

            }
        }

        private void Create(String s, PictureBox pic_box, String ss)
        {
            Image gif;
            FrameDimension frm_d;
            Assembly assembly;
            Stream stream;
            Bitmap img;
            int currentFrame = 0;
            assembly = Assembly.GetExecutingAssembly();

            stream = assembly.GetManifestResourceStream(s);

            gif = Image.FromStream(stream);
            frm_d = new FrameDimension(gif.FrameDimensionsList[0]);

            anim_t = new Timer();
            anim_t.Interval = 45;
            anim_t.Start();
            anim_t.Tick += new EventHandler(delegate
            {
                if (currentFrame < gif.GetFrameCount(frm_d))
                {
                    gif.SelectActiveFrame(frm_d, currentFrame);
                    img = new Bitmap(gif);
                    pic_box.Image = img;
                    currentFrame++;
                }
                else
                {
                    anim_t.Dispose();
                }
            });
            pic_box.Tag = ss;
        }
    }
}
