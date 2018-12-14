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
    public partial class gallery : Form
    {
        Graphics g;
        Form1 parent;
        int menu = 1;
        //1 boss
        //2 weapon
        //3 zombie
        Image iconBoss = Image.FromFile("image/background/bg_gallery_boss.png");
        Image iconWeapon = Image.FromFile("image/background/bg_gallery_weapon.png");
        Image iconZombie = Image.FromFile("image/background/bg_gallery_zombie.png");
        public gallery()
        {
            InitializeComponent();
        }
        public void repaint()
        {
            menu = 1;
            parent = (Form1)this.MdiParent;
            Invalidate();
        }
        private void gallery_Load(object sender, EventArgs e)
        {
            this.Size = new Size(800, 600);
            this.DoubleBuffered = true;
        }

        public void next()
        {
            menu++;
            if(menu > 3)
            {
                menu = 1;
            }
            Invalidate();
        }

        public void prev()
        {
            menu--;
            if(menu < 1)
            {
                menu = 3;
            }
            Invalidate();
        }

        private void gallery_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Image bg = Image.FromFile("image/background/bg_gallery.jpg");

            g.DrawImage(bg, 0, 0, 800, 600);
            if(menu == 1)
            {
                g.DrawImage(iconBoss, 0, 0, 800, 600);
            }else if(menu == 2)
            {
                g.DrawImage(iconWeapon, 0, 0, 800, 600);
            }
            else if (menu == 3)
            {
                g.DrawImage(iconZombie, 0, 0, 800, 600);
            }

            Font f = new Font("Microsoft Sans Serif", 16);
            Brush b = new SolidBrush(Color.White);
            String word = "Welcome to the Database Center,\nSir ";
            g.DrawString(word + parent.player.nama, f, b, 420, 20);

            String desc = "";
            if(menu ==2)
            {
                desc = "Weapon Box\nLook inside to see the collection of\nweapon that you've used";
            }else if(menu == 3)
            {
                desc = "Zombie Prison\nLook at all the collection of zombie\nthat you've captured";
            }else if(menu == 1)
            {
                desc = "Boss Data\nSee the description of the boss you've\nmet";
            }

            g.DrawString(desc, f, b, 420, 90);
        }
    }
}
