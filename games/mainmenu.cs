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
    public partial class mainmenu : Form
    {
        int menu = 1;
        Graphics g;
        Image bg;
        Image logo;
        int x = 0;
        int dx = -1;
        Form1 parent;
        public void next()
        {
            menu++;
            label1.Text = "Stage";
            label2.Text = "Exit";
            label3.Text = "Highscore";
            label4.Text = "Gallery";
            if(menu > 4)
            {
                menu = 1;
            }
            //1432
            if(menu == 1)
            {
                label1.Text += " <";
            }else if(menu == 2)
            {
                label4.Text += " <";
            }else if(menu == 3)
            {
                label3.Text += " <";
            }else if(menu == 4)
            {
                label2.Text += " <";
            }
        }

        public void enter()
        {
            parent = (Form1)this.MdiParent;
            if(menu == 4)
            {
                if (MessageBox.Show("Are you sure ?", "EXIT", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    parent.goForm(1);
            }else if(menu == 2)
            {
                parent.goForm(4);
            }else if(menu == 1)
            {
                parent.goForm(5);
            }else if(menu == 3)
            {
                parent.goForm(7);
            }
        }
        public void prev()
        {
            menu--;
            label1.Text = "Stage";
            label2.Text = "Exit";
            label3.Text = "Highscore";
            label4.Text = "Gallery";
            if (menu < 1)
            {
                menu = 4;
            }
            //1432
            if (menu == 1)
            {
                label1.Text += " <";
            }
            else if (menu == 2)
            {
                label4.Text += " <";
            }
            else if (menu == 3)
            {
                label3.Text += " <";
            }
            else if (menu == 4)
            {
                label2.Text += " <";
            }
        }
        public mainmenu()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            this.Size = new Size(800, 600);
            bg = Image.FromFile("image/background/bg_mainmenu.jpg");
            logo = Image.FromFile("image/game_logo.png");
        }

        private void mainmenu_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.MdiParent;
        }

        public void repaint()
        {
            timer1.Stop();
            x = -2;
            dx = -1;
            Invalidate();
            timer1.Start();
            parent.saveXML();
        }

        private void mainmenu_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.DrawImage(bg,new Rectangle(x,0,1200,600),new Rectangle(0,0,1200,600),GraphicsUnit.Pixel);
            g.DrawImage(logo, 50, 10, 200, 200);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            x += dx;
            if(x > -2 || x < -120)
            {
                dx *= -1;
            }
            Invalidate();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
