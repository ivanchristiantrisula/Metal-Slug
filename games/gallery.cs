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
        public void back()
        {
            if(ctrGall == 0)
            {
                parent.goForm(3);
            }
            else
            {
                ctrGall = 0;
                ctrZombie = 0;
                ctrWeapon = 0;
                ctrBoss = 0;
                Invalidate();
            }
        }
        private void gallery_Load(object sender, EventArgs e)
        {
            this.Size = new Size(800, 600);
            this.DoubleBuffered = true;
        }

        int ctrGall = 0;
        int ctrBoss = -1;
        int ctrWeapon = 0;
        int ctrZombie = 0;
        //0 Main
        //1 boss
        //2 weapon
        //3 zombie

        public void enter()
        {
            if(ctrGall == 0)
            {
                ctrGall = menu;
                if(menu == 1)
                {
                    if(parent.player.boss > 0)
                    {
                        ctrBoss = 0;
                    }
                }
            }
            Invalidate();
        }

        public void next()
        {
            if(ctrGall == 0)
            {
                menu++;
                if (menu > 3)
                {
                    menu = 1;
                }
                Invalidate();
            }else if(ctrGall == 1)
            {
                ctrBoss++;
                if(ctrBoss > parent.player.boss-1)
                {
                    if(parent.player.boss > 0)
                    {
                        ctrBoss = 0;
                    }
                    else
                    {
                        ctrBoss = -1;
                    }
                }
                Invalidate();
            }else if(ctrGall == 2)
            {
                ctrWeapon++;
                if(ctrWeapon > 5)
                {
                    ctrWeapon = 0;
                }
                Invalidate();
            }else if(ctrGall == 3)
            {
                ctrZombie++;
                if(ctrZombie > parent.player.stage-1)
                {
                    ctrZombie = 0;
                }
                Invalidate();
            }
        }

        public void prev()
        {
            if (ctrGall == 0)
            {
                menu--;
                if (menu < 1)
                {
                    menu = 3;
                }
                Invalidate();
            }
            else if (ctrGall == 1)
            {
                ctrBoss--;
                if (ctrBoss < 0)
                {
                    ctrBoss = parent.player.boss - 1;
                }
                Invalidate();
            }
            else if (ctrGall == 2)
            {
                ctrWeapon--;
                if (ctrWeapon < 0)
                {
                    ctrWeapon = 5;
                }
                Invalidate();
            }
            else if (ctrGall == 3)
            {
                ctrZombie--;
                if (ctrZombie < 0)
                {
                    ctrZombie = parent.player.stage - 1;
                }
                Invalidate();
            }
        }

        private void gallery_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            if(ctrGall == 0)
            {
                Image bg = Image.FromFile("image/background/bg_gallery.jpg");

                g.DrawImage(bg, 0, 0, 800, 600);
                if (menu == 1)
                {
                    g.DrawImage(iconBoss, 0, 0, 800, 600);
                }
                else if (menu == 2)
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
                if (menu == 2)
                {
                    desc = "Weapon Box\nLook inside to see the collection of\nweapon that you've used";
                }
                else if (menu == 3)
                {
                    desc = "Zombie Prison\nLook at all the collection of zombie\nthat you've captured";
                }
                else if (menu == 1)
                {
                    desc = "Boss Data\nSee the description of the boss you've\nmet";
                }

                g.DrawString(desc, f, b, 420, 90);
            }else if(ctrGall == 1)
            {
                //BOSS
                Image[] gal_boss = new Image[6];
                gal_boss[0] = Image.FromFile("image/gallery/boss/gal_boss1.jpg");
                gal_boss[1] = Image.FromFile("image/gallery/boss/gal_boss2.jpg");
                gal_boss[2] = Image.FromFile("image/gallery/boss/gal_boss2.jpg");
                gal_boss[3] = Image.FromFile("image/gallery/boss/gal_boss4.jpg");
                gal_boss[4] = Image.FromFile("image/gallery/boss/gal_boss5.jpg");
                gal_boss[5] = Image.FromFile("image/gallery/boss/gal_boss5.jpg");
                if(ctrBoss < 0)
                {
                    Font f = new Font("Microsoft Sans Serif", 42);
                    Brush b = new SolidBrush(Color.Black);
                    g.DrawString("NO DATA FOUND", f, b, 160, 200);
                }
                else
                {
                    g.DrawImage(gal_boss[ctrBoss], 0, 0, 800, 600);
                }
            }
            else if(ctrGall == 2)
            {
                //WEAPON
                Image[] gal_weapon = new Image[6];
                gal_weapon[0] = Image.FromFile("image/gallery/weapon/gal_weapon1.jpg");
                gal_weapon[1] = Image.FromFile("image/gallery/weapon/gal_weapon2.jpg");
                gal_weapon[2] = Image.FromFile("image/gallery/weapon/gal_weapon3.jpg");
                gal_weapon[3] = Image.FromFile("image/gallery/weapon/gal_weapon4.jpg");
                gal_weapon[4] = Image.FromFile("image/gallery/weapon/gal_weapon5.jpg");
                gal_weapon[5] = Image.FromFile("image/gallery/weapon/gal_weapon6.jpg");
                g.DrawImage(gal_weapon[ctrWeapon], 0, 0, 800, 600);

            }
            else if(ctrGall == 3)
            {
                //ZOMBIE
                Image[] gal_zombie = new Image[6];
                gal_zombie[0] = Image.FromFile("image/gallery/zombie/gal_infected.jpg");
                gal_zombie[1] = Image.FromFile("image/gallery/zombie/gal_mutant.jpg");
                gal_zombie[2] = Image.FromFile("image/gallery/zombie/gal_mummy.jpg");
                gal_zombie[3] = Image.FromFile("image/gallery/zombie/gal_clown.jpg");
                gal_zombie[4] = Image.FromFile("image/gallery/zombie/gal_soldier.jpg");
                gal_zombie[5] = Image.FromFile("image/gallery/zombie/gal_frankestein.jpg");
                g.DrawImage(gal_zombie[ctrZombie], 0, 0, 800, 600);
            }
        }
    }
}
