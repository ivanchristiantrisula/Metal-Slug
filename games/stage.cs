using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace games
{
    public partial class stage : Form
    {
        Graphics g;
        int level = 1;
        Image lvl1 = Image.FromFile("image/background/bg_stage_1.png");
        Image lvl2 = Image.FromFile("image/background/bg_stage_2.png");
        Image lvl3 = Image.FromFile("image/background/bg_stage_3.png");
        Image lvl4 = Image.FromFile("image/background/bg_stage_4.png");
        Image lvl5 = Image.FromFile("image/background/bg_stage_5.png");
        Image lvl6 = Image.FromFile("image/background/bg_stage_6.png");

        stage1 s1 = new stage1();
        Form1 parent;
        public stage()
        {
            InitializeComponent();
            this.Size = new Size(800, 600);
            this.DoubleBuffered = true;
        }

        public void repaint()
        {
            level = 1;
            Invalidate();
        }

        public void next()
        {
            
            int max = parent.player.stage;
            level++;
            if(level > max)
            {
                level = 1;
            }
            Invalidate();
        }

        public void prev()
        {
            Form1 parent = (Form1)this.MdiParent;
            int max = parent.player.stage;
            level--;
            if (level < 1)
            {
                level = max;
            }
            Invalidate();
        }

        private void stage_Load(object sender, EventArgs e)
        {
            parent =  (Form1)this.MdiParent;
        }

        private void stage_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Image bg = Image.FromFile("image/background/bg_stage.jpg");

            g.DrawImage(bg, 0, 0, 800, 600);
            if(level == 1)
            {
                g.DrawImage(lvl1, 0, 0, 800, 600);
            }else if (level == 2)
            {
                g.DrawImage(lvl2, 0, 0, 800, 600);
            }
            else if (level == 3)
            {
                g.DrawImage(lvl3, 0, 0, 800, 600);
            }
            else if (level == 4)
            {
                g.DrawImage(lvl4, 0, 0, 800, 600);
            }
            else if (level == 5)
            {
                g.DrawImage(lvl5, 0, 0, 800, 600);
            }
            else if (level == 6)
            {
                g.DrawImage(lvl6, 0, 0, 800, 600);
            }
        }

        private void stage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(level > parent.player.stage)
                {
                    MessageBox.Show("LEVEL LOCKED!");
                }
                else
                {
                    parent.setLevel(level);
                    parent.goForm(6);
                    if(level == 1)
                    {
                        parent.bgplay(2);
                    }else if(level == 2)
                    {
                        parent.bgplay(4);
                    }else if(level == 3)
                    {
                        parent.bgplay(6);
                    }else if(level == 4)
                    {
                        parent.bgplay(8);
                    }else if(level == 5)
                    {
                        parent.bgplay(10);
                    }else if(level == 6)
                    {
                        parent.bgplay(12);
                    }
                }
                
            }
        }
    }
}
