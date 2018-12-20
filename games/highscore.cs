using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace games
{
    public partial class highscore : Form
    {
        Graphics g;
        Form1 parent;
        int[] data = new int[6];
        public highscore()
        {
            InitializeComponent();
            parent = (Form1)this.MdiParent;
        }

        public void relaunch()
        {
            parent = (Form1)this.MdiParent;
            Invalidate();
        }

        private void highscore_Load(object sender, EventArgs e)
        {
            this.Size = new Size(800, 600);
            
        }

        private void highscore_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Brush b = new SolidBrush(Color.Black);
            Font f = new Font("Microsoft Sans Serif", 24);
            int m = 0;
            Image bg = Image.FromFile("image/background/bg_highscore.png");
            g.DrawImage(bg, -200, -200, 1500, 1500);
            g.DrawImage(bg, 0, 0, 800, 600);
            for (int i = 0; i < 6; i++)
            {
                int y = 0;
                if(i < 3)
                {
                    y = 100;
                }
                else
                {
                    y = 300;
                }
                String line = "Level " + (i + 1) + "\n=======\n" + parent.player.highscore[i];

                
                g.DrawString(line, f, b, m * 200+130, y);
                m++;
                if(m > 2)
                {
                    m = 0;
                }
            }
        }
    }
}
