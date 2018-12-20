using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary1;
using AxWMPLib;
using System.Media;
using System.Drawing.Imaging;

namespace games
{
    public partial class stage1 : Form
    {
        Image bg;
        Image[] img_drop = new Image[6];
        Image currWeapon;
        int weaponCode = 0;
        List<Image> animPlayer = new List<Image>();
        Image currAnim;
        Random r = new Random();
        List<zombie> bot = new List<zombie>();
        List<int> ctrZom = new List<int>();
        player p = new player();
        List<Image> weaponImages = new List<Image>();
        List<Image> ammoImages = new List<Image>();
        List<ammo> shot = new List<ammo>();
        Image[] map = new Image[6];
        Image[,] spriteZombie = new Image[6, 6];
        Image life;
        Image[] img_weapon = new Image[8];
        Image[] peasant_esc = new Image[6];
        Image[] peasant_idle = new Image[2];
        Image[] peasant_move = new Image[3];
        Image[] child = new Image[10];
        Image[] boss4 = new Image[11];
        Image boss5_ammo;
        Image boss5_ammocoll;
        Image[] boss5 = new Image[7];
        List<item> drop = new List<item>();

        bool ctrPeasant = false;
        bool ctrChild = false;
        int x_child = 800;
        int y_child = 400;
        int x_peasant = 800;
        int y_peasant = 400;
        bool bossFight = false;

        int dx = 10;
        int x = 10;
        int y = 400;
        int x_map=0;
        int hadap = 1;
        int level = 0;
        int stage = 3;
        int nenemy = 0;
        int enemyNow = 0;
        bool keyDisabled = false;
        Image lootBox = Image.FromFile("image/icon/item_weaponbox.png");
        int x_lootbox = 0;
        float y_lootbox = 0;
        bool lootOpened = false;
        Form1 parent;
        SoundPlayer[] sfx_weapon = new SoundPlayer[7];
        Image currBossAnim = null;
        bool done = false;
        List<Image> listFlash = new List<Image>();
        List<Image> imageJugger = new List<Image>();
        public void gameover()
        {
            
            if(p.life <= 0 || done == true)
            {
                timerMove.Stop();
                timerShot.Stop();
                timerZombie.Stop();
                timerBonus.Stop();
                timerChild.Stop();
                timerImmortal.Stop();
                keyDisabled = true;
                resetBoss();
                MessageBox.Show("GAME OVER!");
                if (done == true)
                {
                    parent = (Form1)this.MdiParent;
                    parent.player.stage = this.level + 1;
                    p.score += 5000;
                }
                if (parent.player.highscore[level] < p.score)
                {
                    MessageBox.Show("Congratulation you get a new highscore!");
                    parent.player.highscore[level-1] = p.score;
                }
                parent.goForm(3);

                

                this.Close();
            }
        }
        public void nextStage()
        {
            keyDisabled = true;
            nenemy = r.Next(15, 30);
            enemyNow = 0;
            hadap = 1;
            stage++;

            timerZombie.Stop();
            timerMove.Stop();
            timerNextStage.Start();
            timerBonus.Stop();

            if(stage == 6)
            {
                done = true;
                gameover();
            }
        }

        List<int> xblood = new List<int>();
        List<int> yblood = new List<int>();
        List<int> ctrblood = new List<int>();

        public void relaunch(int lvl)
        {
            parent = (Form1)this.MdiParent;
            level = lvl;
            stage = -1;
            x_map = 0;
            x_child = 800;
            x = 10;
            ctrLootBox = 0;
            lootOpened = false;
            timerLootBox.Start();
            
            p = new player();
            if (level == 1)
            {
                bg = Image.FromFile("image/background/bg_mainmenu.jpg");
            }
            timerChild.Stop();
            timerImmortal.Stop();

            nextStage();
        }
        

        public stage1()
        {
            //boss4
            boss4[0] = Image.FromFile("image/boss/boss4_idle1.png");
            boss4[1] = Image.FromFile("image/boss/boss4_idle2.png");
            boss4[2] = Image.FromFile("image/boss/boss4_idle3.png");
            boss4[3] = Image.FromFile("image/boss/boss4_shot1.png");
            boss4[4] = Image.FromFile("image/boss/boss4_shot2.png");
            boss4[5] = Image.FromFile("image/boss/boss4_shot3.png");
            boss4[6] = Image.FromFile("image/boss/boss4_shot4.png");
            boss4[7] = Image.FromFile("image/boss/boss4_shot5.png");
            boss4[8] = Image.FromFile("image/boss/boss4_dead1.png");
            boss4[9] = Image.FromFile("image/boss/boss4_dead2.png");
            boss4[10] = Image.FromFile("image/boss/boss4_dead3.png");


            //boss5
            boss5[0] = Image.FromFile("image/boss/boss5_idle1.png");
            boss5[1] = Image.FromFile("image/boss/boss5_idle2.png");
            boss5[2] = Image.FromFile("image/boss/boss5_idle3.png");
            boss5[3] = Image.FromFile("image/boss/boss5_dead1.png");
            boss5[4] = Image.FromFile("image/boss/boss5_dead2.png");
            boss5[5] = Image.FromFile("image/boss/boss5_dead3.png");
            boss5[6] = Image.FromFile("image/boss/boss5_dead4.png");
            boss5_ammo = Image.FromFile("image/boss/boss5_ammo.png");
            boss5_ammocoll = Image.FromFile("image/boss/boss5_coll.png");


            //KODE WEAPON MBEK AMMO
            /*
            0 = default pistol
            1 = xbow
            2 = shotgun
            3 = submachinegun
            4 = grenade
            5 = sniper
            6 = explode
            */
            InitializeComponent();
            this.Size = new Size(800, 600);
            Location = new Point(0, 0);
            timerIdle.Interval = 500;
            timerJump.Interval = 250;
            timerIdle.Start();
            timerChild.Stop();
            child[0] = Image.FromFile("image/peasant/child_move1.png");
            child[1] = Image.FromFile("image/peasant/child_move2.png");
            child[2] = Image.FromFile("image/peasant/child_move3.png");
            child[3] = Image.FromFile("image/peasant/child_esc1.png");
            child[4] = Image.FromFile("image/peasant/child_esc2.png");
            child[5] = Image.FromFile("image/peasant/child_esc3.png");
            child[6] = Image.FromFile("image/peasant/child_esc4.png");
            child[7] = Image.FromFile("image/peasant/child_esc5.png");
            child[8] = Image.FromFile("image/peasant/child_esc6.png");
            child[9] = Image.FromFile("image/peasant/child_esc7.png");

            //item
            //0 atom
            //1 blackstone
            //2 goldlion
            //3 goldpig
            //4 goldpill
            //5 weaponbox
            img_drop[0] = Image.FromFile("image/icon/item_atom.png");
            img_drop[1] = Image.FromFile("image/icon/item_blackstone.png");
            img_drop[2] = Image.FromFile("image/icon/item_goldlion.png");
            img_drop[3] = Image.FromFile("image/icon/item_goldpig.png");
            img_drop[4] = Image.FromFile("image/icon/item_goldpill.png");
            img_drop[5] = Image.FromFile("image/icon/item_weaponbox.png");

            sfx_weapon[0] = new SoundPlayer("sfx/weapon/shot_smg.wav");
            sfx_weapon[1] = new SoundPlayer("sfx/weapon/shot_xbow.wav");
            sfx_weapon[2] = new SoundPlayer("sfx/weapon/shot_shotgun.wav");
            sfx_weapon[3] = new SoundPlayer("sfx/weapon/shot_smg.wav");
            sfx_weapon[4] = new SoundPlayer("sfx/weapon/shot_grenadelauncher.wav");
            sfx_weapon[5] = new SoundPlayer("sfx/weapon/shot_sniper.wav");
            sfx_weapon[6] = new SoundPlayer("sfx/weapon/shot_explode.wav");
            life = Image.FromFile("image/icon/icon_life.png");
            map[0] = Image.FromFile("image/map/map_1.jpg");
            map[1] = Image.FromFile("image/map/map_2.jpg");
            map[2] = Image.FromFile("image/map/map_3.jpg");
            map[3] = Image.FromFile("image/map/map_4.jpg");
            map[4] = Image.FromFile("image/map/map_5.jpg");
            map[5] = Image.FromFile("image/map/map_6.jpg");
            img_weapon[0] = Image.FromFile("image/icon/icon_default.jpg");
            img_weapon[1] = Image.FromFile("image/icon/icon_xbow.jpg");
            img_weapon[2] = Image.FromFile("image/icon/icon_shotgun.jpg");
            img_weapon[3] = Image.FromFile("image/icon/icon_smg.jpg");
            img_weapon[4] = Image.FromFile("image/icon/icon_grenadelauncher.jpg");
            img_weapon[5] = Image.FromFile("image/icon/icon_sniper.jpg");
            img_weapon[6] = Image.FromFile("image/icon/icon_chainsaw.jpg");
            img_weapon[7] = Image.FromFile("image/icon/icon_knife.jpg");
            animPlayer.Add(Image.FromFile("image/sprite/p_default_idle1.png"));
            animPlayer.Add(Image.FromFile("image/sprite/p_default_idle2.png"));
            animPlayer.Add(Image.FromFile("image/sprite/p_default_move1.png"));
            animPlayer.Add(Image.FromFile("image/sprite/p_default_move2.png"));
            animPlayer.Add(Image.FromFile("image/sprite/p_default_move3.png"));
            animPlayer.Add(Image.FromFile("image/sprite/p_default_jump.png"));
            animPlayer.Add(Image.FromFile("image/sprite/p_death1.png"));
            animPlayer.Add(Image.FromFile("image/sprite/p_death2.png"));
            animPlayer.Add(Image.FromFile("image/sprite/p_death3.png"));
            animPlayer.Add(Image.FromFile("image/sprite/p_death4.png"));
            animPlayer.Add(Image.FromFile("image/sprite/p_death5.png"));
            animPlayer.Add(Image.FromFile("image/sprite/p_death6.png"));
            animPlayer.Add(Image.FromFile("image/sprite/p_death7.png"));
            animPlayer.Add(Image.FromFile("image/sprite/p_default_hit.png"));

            //LIST ZOMBIE
            //0 zombie
            //1 mutant
            //2 mummy
            //3 clown
            //4 soldier
            //5 hulk

            //0 move1
            //1 move2
            //2 move3
            //3 dead1
            //4 dead2
            //5 dead3


            spriteZombie[0, 0] = Image.FromFile("image/sprite/musuh/zombie_move1.png");
            spriteZombie[0, 1] = Image.FromFile("image/sprite/musuh/zombie_move2.png");
            spriteZombie[0, 2] = Image.FromFile("image/sprite/musuh/zombie_move3.png");
            spriteZombie[0, 3] = Image.FromFile("image/sprite/musuh/zombie_dead1.png");
            spriteZombie[0, 4] = Image.FromFile("image/sprite/musuh/zombie_dead2.png");
            spriteZombie[0, 5] = Image.FromFile("image/sprite/musuh/zombie_dead3.png");

            spriteZombie[1, 0] = Image.FromFile("image/sprite/musuh/mutant_move1.png");
            spriteZombie[1, 1] = Image.FromFile("image/sprite/musuh/mutant_move2.png");
            spriteZombie[1, 2] = Image.FromFile("image/sprite/musuh/mutant_move3.png");
            spriteZombie[1, 3] = Image.FromFile("image/sprite/musuh/mutant_dead1.png");
            spriteZombie[1, 4] = Image.FromFile("image/sprite/musuh/mutant_dead2.png");
            spriteZombie[1, 5] = Image.FromFile("image/sprite/musuh/mutant_dead3.png");

            spriteZombie[2, 0] = Image.FromFile("image/sprite/musuh/mummy_move1.png");
            spriteZombie[2, 1] = Image.FromFile("image/sprite/musuh/mummy_move2.png");
            spriteZombie[2, 2] = Image.FromFile("image/sprite/musuh/mummy_move3.png");
            spriteZombie[2, 3] = Image.FromFile("image/sprite/musuh/mummy_dead1.png");
            spriteZombie[2, 4] = Image.FromFile("image/sprite/musuh/mummy_dead2.png");
            spriteZombie[2, 5] = Image.FromFile("image/sprite/musuh/mummy_dead3.png");

            spriteZombie[3, 0] = Image.FromFile("image/sprite/musuh/clown_move1.png");
            spriteZombie[3, 1] = Image.FromFile("image/sprite/musuh/clown_move2.png");
            spriteZombie[3, 2] = Image.FromFile("image/sprite/musuh/clown_move3.png");
            spriteZombie[3, 3] = Image.FromFile("image/sprite/musuh/clown_dead1.png");
            spriteZombie[3, 4] = Image.FromFile("image/sprite/musuh/clown_dead2.png");
            spriteZombie[3, 5] = Image.FromFile("image/sprite/musuh/clown_dead3.png");

            spriteZombie[4, 0] = Image.FromFile("image/sprite/musuh/soldier_move1.png");
            spriteZombie[4, 1] = Image.FromFile("image/sprite/musuh/soldier_move2.png");
            spriteZombie[4, 2] = Image.FromFile("image/sprite/musuh/soldier_move3.png");
            spriteZombie[4, 3] = Image.FromFile("image/sprite/musuh/soldier_dead1.png");
            spriteZombie[4, 4] = Image.FromFile("image/sprite/musuh/soldier_dead2.png");
            spriteZombie[4, 5] = Image.FromFile("image/sprite/musuh/soldier_dead3.png");

            spriteZombie[5, 0] = Image.FromFile("image/sprite/musuh/frankestein_move1.png");
            spriteZombie[5, 1] = Image.FromFile("image/sprite/musuh/frankestein_move2.png");
            spriteZombie[5, 2] = Image.FromFile("image/sprite/musuh/frankestein_move3.png");
            spriteZombie[5, 3] = Image.FromFile("image/sprite/musuh/frankestein_dead1.png");
            spriteZombie[5, 4] = Image.FromFile("image/sprite/musuh/frankestein_dead2.png");
            spriteZombie[5, 5] = Image.FromFile("image/sprite/musuh/frankestein_dead3.png");

            weaponImages.Add(Image.FromFile("image/icon/icon_default.jpg"));
            weaponImages.Add(Image.FromFile("image/icon/icon_xbow.jpg"));
            weaponImages.Add(Image.FromFile("image/icon/icon_shotgun.jpg"));
            weaponImages.Add(Image.FromFile("image/icon/icon_smg.jpg"));
            weaponImages.Add(Image.FromFile("image/icon/icon_grenadelauncher.jpg"));
            weaponImages.Add(Image.FromFile("image/icon/icon_sniper.jpg"));
            weaponImages.Add(Image.FromFile("image/icon/icon_chainsaw.jpg"));
            weaponImages.Add(Image.FromFile("image/icon/icon_knife.jpg"));

            ammoImages.Add(Image.FromFile("image/ammo/ammo_default.png"));
            ammoImages.Add(Image.FromFile("image/ammo/ammo_arrow.png"));
            ammoImages.Add(Image.FromFile("image/ammo/ammo_shotgun.png"));
            ammoImages.Add(Image.FromFile("image/ammo/ammo_smg.png"));
            ammoImages.Add(Image.FromFile("image/ammo/ammo_grenade.png"));
            ammoImages.Add(Image.FromFile("image/ammo/ammo_sniper.png"));
            ammoImages.Add(Image.FromFile("image/ammo/ammo_explode.png"));
            //AMMO CHAINSAW
            //AMMO KNIFE

            listFlash.Add(Image.FromFile("image/boss/boss2_move1.png"));
            listFlash.Add(Image.FromFile("image/boss/boss2_move2.png"));
            listFlash.Add(Image.FromFile("image/boss/boss2_move3.png"));
            listFlash.Add(Image.FromFile("image/boss/boss2_dead1.png"));
            listFlash.Add(Image.FromFile("image/boss/boss2_dead2.png"));
            listFlash.Add(Image.FromFile("image/boss/boss2_dead3.png"));
            listFlash.Add(Image.FromFile("image/boss/boss2_dead4.png"));

            imageJugger.Add(Image.FromFile("image/boss/boss1_move1.png"));
            imageJugger.Add(Image.FromFile("image/boss/boss1_move2.png"));
            imageJugger.Add(Image.FromFile("image/boss/boss1_move3.png"));
            imageJugger.Add(Image.FromFile("image/boss/boss1_hit1.png"));
            imageJugger.Add(Image.FromFile("image/boss/boss1_hit2.png"));
            imageJugger.Add(Image.FromFile("image/boss/boss1_hit3.png"));
            imageJugger.Add(Image.FromFile("image/boss/boss1_hit4.png"));
            imageJugger.Add(Image.FromFile("image/boss/boss1_hit5.png"));
            imageJugger.Add(Image.FromFile("image/boss/boss1_hit6.png"));
            imageJugger.Add(Image.FromFile("image/boss/boss1_dead1.png"));
            imageJugger.Add(Image.FromFile("image/boss/boss1_dead2.png"));
            imageJugger.Add(Image.FromFile("image/boss/boss1_dead3.png"));
            imageJugger.Add(Image.FromFile("image/boss/boss1_dead4.png"));
            imageJugger.Add(Image.FromFile("image/boss/boss1_dead5.png"));
        }

        private void stage1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(map[level - 1], new Rectangle(x_map, 0, 4800, 600), new Rectangle(0, 0, 4800, 600), GraphicsUnit.Pixel);

            if (hadap == 1)
            {
                g.DrawImage(currAnim, x, y, 200, 200);
            }
            else
            {
                g.DrawImage(currAnim, x - 125, y, 200, 200);
            }



            for (int i = 0; i < bot.Count; i++)
            {
                g.DrawImage(spriteZombie[bot[i].jenis, bot[i].ani], bot[i].x, bot[i].y, 200, 200);
            }
            for (int i = 0; i < shot.Count; i++)
            {
                if (shot[i].jenis == 2 || shot[i].jenis == 6)
                {
                    if (shot[i].arah == -1)
                    {
                        Image a = ammoImages[shot[i].jenis];
                        Bitmap bmp = new Bitmap(a);
                        bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                        a = bmp;
                        g.DrawImage(a, shot[i].x, shot[i].y - 100, 200, 200);

                    }
                    else if(shot[i].arah==0)
                    {
                        Image a = ammoImages[shot[i].jenis];
                        Bitmap bmp = new Bitmap(a);
                        bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        a = bmp;
                        g.DrawImage(a, shot[i].x, shot[i].y - 100, 200, 200);
                    }
                    else
                    {
                        g.DrawImage(ammoImages[shot[i].jenis], shot[i].x, shot[i].y - 100, 200, 200);
                    }
                    
                }
                else
                {
                    if (shot[i].arah == -1)
                    {
                        Image a = ammoImages[shot[i].jenis];
                        Bitmap bmp = new Bitmap(a);
                        bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                        a = bmp;
                        g.DrawImage(a, shot[i].x, shot[i].y, 50, 10);

                    }
                    else if (shot[i].arah == 0)
                    {
                        Image a = ammoImages[shot[i].jenis];
                        Bitmap bmp = new Bitmap(a);
                        bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        a = bmp;
                        g.DrawImage(a, shot[i].x, shot[i].y, 10, 50);
                    }
                    else
                    {
                        g.DrawImage(ammoImages[shot[i].jenis], shot[i].x, shot[i].y, 50, 10);
                    }
                    
                }
            }

            g.DrawImage(life, 10, 10, 50, 50);
            g.DrawImage(img_weapon[p.weapon],250, 10, 50, 50);
            Font f = new Font("Microsoft Sans Serif", 24);
            Brush b = new SolidBrush(Color.White);
            g.DrawString("" + p.life, f, b, 70, 15);
            g.DrawString("" + p.score, f, b, 650, 15);
            String tammo = "";
            if(p.ammunition == -1)
            {
                tammo = "INF";
            }
            else
            {
                tammo = p.ammunition + "x";
            }

            g.DrawString("" + p.life, f, b, 70, 15);
            g.DrawString(tammo, f, b, 300, 15);
            if(p.immortal == true)
            {
                b = new SolidBrush(Color.Gold);
                g.DrawString(" "+ctrImmortal+" ", f, b, x, 450);
                g.DrawString("XX~XX", f, b, x, 550);
                g.DrawString("X~X~X", f, b, x, 525);
                g.DrawString("~X~X~", f, b, x, 500);
                g.DrawString("X~X~X", f, b, x, 475);
            }
            if (!lootOpened)
            {
                if (y_lootbox != 550)
                {
                    
                    g.DrawImage(Image.FromFile("image/icon/item_parachute.png"), x_lootbox, y_lootbox-115, 63, 128);
                }
                g.DrawImage(lootBox, x_lootbox, y_lootbox, 50,50);
            }

            g.DrawImage(child[aniChild], x_child, y_child, 200, 200);
            for (int i = 0; i < drop.Count; i++)
            {
                g.DrawImage(img_drop[drop[i].jenis], drop[i].x, 550, 50, 50);
            }

            if(bossFight == true)
            {
                if(level == 2)
                {
                    if (currBossAnim != null)
                    {
                        g.DrawImage(currBossAnim, x_boss, y_boss, 200, 200);
                    }
                }else if(level == 5)
                {
                    g.DrawImage(boss5[aniBoss5], 550, 50, 200, 200);
                    for (int i = 0; i < enemyAmmo.Count; i++)
                    {
                        if(enemyAmmo[i].jenis == -1)
                        {

                            g.DrawImage(boss5_ammo, enemyAmmo[i].x, enemyAmmo[i].y, 50, 50);
                        }
                        else
                        {
                            g.DrawImage(boss5_ammocoll, enemyAmmo[i].x, enemyAmmo[i].y, 50, 50);
                        }
                    }
                }else if(level == 4) {
                    g.DrawImage(boss4[aniBoss4], 550, 100, 500, 500);
                    if(ctrLaser > -1)
                    {
                        Pen p = new Pen(Color.DarkRed,30);
                        g.DrawLine(p, 597, 150, desLaser, 600);
                    }
                }else if (level == 1)
                {
                    if (currBossAnim != null)
                    {
                        g.DrawImage(currBossAnim, x_boss, y_boss-200, 200, 400);
                    }
                }
            }

            for (int i = 0; i < ctrblood.Count; i++)
            {
                Brush bood = new SolidBrush(Color.Red);
                g.FillEllipse(bood, xblood[i]+50, yblood[i], 10, 10);
            }
            
        }

        private void stage1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            timerAmmo.Interval = 100;
            timerAmmo.Start();
            timerBlood.Start();

            x_lootbox = r.Next(100, 500);
            timerLootBox.Start();
        }
        int ctrSmg = 0;
        int ctrIdle = 0;
        private void timerIdle_Tick(object sender, EventArgs e)
        {
            if (hadap == 1)
            {
                if (ctrIdle % 2 == 1)
                {
                    currAnim = animPlayer[0];
                }
                else
                {
                    currAnim = animPlayer[1];
                }
            }
            else
            {
                if (ctrIdle % 2 == 1)
                {
                    Bitmap bmp = new Bitmap(animPlayer[0]);
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                    currAnim = bmp;
                }
                else
                {
                    Bitmap bmp = new Bitmap(animPlayer[1]);
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                    currAnim = bmp;
                }
            }
            
            ctrIdle++;
            Invalidate();
        }

        int ctrJalan = 1;
        private void timerJalan_Tick(object sender, EventArgs e)
        {
            if(hadap == 1)
            {
                if (ctrJalan == 1)
                {
                    currAnim = animPlayer[2];
                }
                if (ctrJalan == 2)
                {
                    currAnim = animPlayer[3];
                }
                if (ctrJalan == 3)
                {
                    currAnim = animPlayer[4];
                    ctrJalan = 0;
                }
                x += dx; 
            }else
            {

                if (ctrJalan == 1)
                {
                    Bitmap bmp = new Bitmap(animPlayer[2]);
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                    currAnim = bmp;
                }
                if (ctrJalan == 2)
                {
                    Bitmap bmp = new Bitmap(animPlayer[3]);
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                    currAnim = bmp;
                }
                if (ctrJalan == 3)
                {
                    Bitmap bmp = new Bitmap(animPlayer[4]);
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                    currAnim = bmp;
                    ctrJalan = 0;
                }
                x += dx*-1;
            }
            
            ctrJalan++;
            if(x > 680)
            {
                x = 680;
            }else if(x < 0)
            {
                x = 0;
            }
            collItem();
            Invalidate();
        }

        private void stage1_KeyUp(object sender, KeyEventArgs e)
        {
            if (ctrPlayerMati == 0)
            {
                timerShotAtas.Stop();
                timerJalan.Stop();
                ctrJalan = 0;
                timerIdle.Start();
                timerShot.Stop();
                if (hadap == 1)
                {
                    currAnim = animPlayer[0];
                }
                else
                {
                    Bitmap bmp = new Bitmap(animPlayer[0]);
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                    currAnim = bmp;
                }

                ctrIdle++;
                Invalidate();
            }
            
        }

        private void stage1_KeyDown(object sender, KeyEventArgs e)
        {
            if(keyDisabled == false)
            {
                if (e.KeyCode == Keys.D)
                {
                    if (hadap == -1)
                    {
                        currAnim = animPlayer[2];
                    }

                    
                    hadap = 1;
                    timerIdle.Stop();
                    ctrIdle = 0;
                    timerJalan.Start();
                }
                if (e.KeyCode == Keys.Space)
                {
                    if (ctrJump == 1)
                    {
                        ctrJump = 1;
                        timerJump.Start();
                        timerIdle.Stop();
                        timerJalan.Stop();
                    }
                }
                if (e.KeyCode == Keys.A)
                {
                    if (hadap == 1)
                    {
                        Bitmap bmp = new Bitmap(animPlayer[2]);
                        bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                        currAnim = bmp;
                    }
                    hadap = -1;
                    timerIdle.Stop();
                    ctrIdle = 0;
                    timerJalan.Start();
                }
                if (e.KeyCode == Keys.K)
                {
                    
                    timerShot.Start();
                }
                if (e.KeyCode == Keys.W)
                {
                    timerShotAtas.Start();
                }
                Invalidate();
                //cek nabrak lootbox gak
                if (x > x_lootbox - 50 && x < x_lootbox + 50 && y_lootbox == 550 && !lootOpened)
                {
                    lootOpened = true;

                    //BUAT RANDOM WEAPON DARI LOOTBOX
                    int a = r.Next(1, 6);
                    p.weapon = a;
                    
                    if(p.weapon == 1)
                    {
                        cdWeapon = 1000;
                        p.ammunition = 10;
                    }else if(p.weapon == 2)
                    {
                        cdWeapon = 2000;
                        p.ammunition = 15;
                    }else if(p.weapon == 3)
                    {
                        cdWeapon = 100;
                        p.ammunition = 200;
                    }else if(p.weapon == 4)
                    {
                        cdWeapon = 3000;
                        p.ammunition = 15;
                    }else if(p.weapon == 5)
                    {
                        cdWeapon = 3000;
                        p.ammunition = 10;
                    }
                    ctrWeapon = cdWeapon;
                }
            }
        }

        int ctrJump = 1;
        private void timerJump_Tick(object sender, EventArgs e)
        {
            if (hadap == 1)
            {
                if (ctrJump == 1)
                {
                    y -= 250;
                    x += 200;
                    currAnim = animPlayer[5];
                    ctrJump++;
                    if(x > 680)
                    {
                        x = 680;
                    }
                    Invalidate();
                }
                else
                {
                    y += 250;
                    x += 200;
                    currAnim = animPlayer[2];
                    if (x > 680)
                    {
                        x = 680;
                    }
                    Invalidate();
                    ctrJump = 1;
                    timerJump.Stop();
                }
            }
            else
            {
                if (ctrJump == 1)
                {
                    y -= 250;
                    x -= 200;

                    Bitmap bmp = new Bitmap(animPlayer[5]);
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                    currAnim = bmp;
                    if(x < 0)
                    {
                        x = 0;
                    }
                    ctrJump++;
                    Invalidate();
                }
                else
                {
                    y += 250;
                    x -= 200;

                    Bitmap bmp = new Bitmap(animPlayer[2]);
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                    currAnim = bmp;
                    if(x < 0)
                    {
                        x = 0;
                    }
                    Invalidate();
                    ctrJump = 1;
                    timerJump.Stop();
                }
            }
            
            
        }

        private void timerZombie_Tick(object sender, EventArgs e)
        {
            if(enemyNow < nenemy)
            {
                enemyNow++;
                int jenis = r.Next(1, level + 1);
                bot.Add(new zombie(jenis-1));
            }
            if(enemyNow == nenemy)
            {
                if(bot.Count == 0)
                {
                    nextStage();
                }
            }
            
            Invalidate();
        }

        private void timerAmmo_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < shot.Count; i++)
            {
                if(shot[i].jenis == 2)
                {
                    ctrShotgun++;
                    if(ctrShotgun == 5)
                    {
                        shot.RemoveAt(i);
                        ctrShotgun = 0;
                    }
                }else if(shot[i].jenis == 6)
                {
                    ctrExplode++;
                    if(ctrExplode == 5)
                    {
                        shot.RemoveAt(i);
                        ctrExplode = 0;
                    }
                }
                else
                {
                    if (shot[i].arah == 1)
                    {
                        shot[i].x += 25;
                    }
                    else if (shot[i].arah == -1)
                    {
                        shot[i].x -= 25;
                    }else if (shot[i].arah == 0)
                    {
                        shot[i].y -= 25;
                    }
                }
            }
            boss5_ammoMove();
            ammoColl();
            Invalidate();
        }
        int cdWeapon = 500;
        int ctrWeapon = 500;

        int flash_life = 250;
        int jugger_life = 500;

        public void ammoColl()
        {
            for (int i = 0; i < shot.Count; i++)
            {
                for (int j = 0; j < bot.Count; j++)
                {
                    int tempX = shot[i].x, tempY = shot[i].y;
                    int tempBotX = bot[j].x, tempBotY = bot[j].y;
                    if(shot[i].arah == 1)
                    {
                        tempX += 50; 
                    }
                    if(bot[j].arah == 1)
                    {
                        tempBotX += 200;
                    }

                    if (bot[j].life > 0)
                    {
                        if (shot[i].jenis == 0 || shot[i].jenis == 3)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(tempBotX, tempBotY, 120, 160);
                            
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                xblood.Add(tempX);
                                yblood.Add(tempY);
                                ctrblood.Add(0);
                                bot[j].life -= shot[i].dmg;
                                shot.RemoveAt(i);
                                if (bot[j].life <= 0)
                                {
                                    
                                    bot[j].ani = 3;
                                }
                                break;
                            }
                        } else if (shot[i].jenis == 1 || shot[i].jenis == 5)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(tempBotX, tempBotY, 120, 160);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                xblood.Add(tempX);
                                yblood.Add(tempY);
                                ctrblood.Add(0);
                                bot[j].life -= shot[i].dmg;
                                if (bot[j].life <= 0)
                                {
                                    bot[j].ani = 3;
                                }
                                break;
                            }
                        }else if(shot[i].jenis == 2)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY-100, 200, 200);
                            Rectangle rZombie = new Rectangle(tempBotX, tempBotY, 120, 160);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                bot[j].life -= shot[i].dmg;
                                if (bot[j].life <= 0)
                                {
                                    bot[j].ani = 3;
                                }
                                break;
                            }
                        }else if(shot[i].jenis == 4)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(tempBotX, tempBotY, 120, 160);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                shot.Add(new ammo(6,shot[i].x, shot[i].y, shot[i].arah));
                                sfx_weapon[6].Play();
                                shot.RemoveAt(i);
                            }
                        }else if(shot[i].jenis == 6)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY - 100, 200, 200);
                            Rectangle rZombie = new Rectangle(tempBotX, tempBotY, 120, 160);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                bot[j].life -= shot[i].dmg;
                                if (bot[j].life <= 0)
                                {
                                    bot[j].ani = 3;
                                }
                                break;
                            }
                        }
                    }
                    

                    
                }
            }
            for (int i = 0; i < shot.Count; i++)
            {
                if(shot[i].x > 800 || shot[i].x+50 < 0)
                {
                    shot.RemoveAt(i);
                }
            }

            //tembak boss
            if(bossFight == true)
            {
                if(level == 5)
                {
                    for (int i = 0; i < shot.Count; i++)
                    {
                        int tempX = shot[i].x;
                        int tempY = shot[i].y;
                        if (shot[i].jenis == 0 || shot[i].jenis == 3)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(550, 50, 200, 200);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                xblood.Add(tempX);
                                yblood.Add(tempY);
                                ctrblood.Add(0);
                                hp_boss5 -= shot[i].dmg;
                                shot.RemoveAt(i);
                                break;
                            }
                        }
                        else if (shot[i].jenis == 1 || shot[i].jenis == 5)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(550, 50, 200, 200);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                xblood.Add(tempX);
                                yblood.Add(tempY);
                                ctrblood.Add(0);
                                hp_boss5 -= shot[i].dmg;
                                break;
                            }
                        }
                        else if (shot[i].jenis == 2)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY - 100, 200, 200);
                            Rectangle rZombie = new Rectangle(550, 50, 200, 200);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                hp_boss5 -= shot[i].dmg;
                                break;
                            }
                        }
                        else if (shot[i].jenis == 4)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(550, 50, 200, 200);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                shot.Add(new ammo(6, shot[i].x, shot[i].y, shot[i].arah));
                                sfx_weapon[6].Play();
                                shot.RemoveAt(i);
                                break;
                            }
                        }
                        else if (shot[i].jenis == 6)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY - 100, 200, 200);
                            Rectangle rZombie = new Rectangle(550, 50, 200, 200);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                hp_boss5 -= shot[i].dmg;
                                break;
                            }
                        }
                    }


                    if(hp_boss5 <= 0)
                    {
                        nextStage();
                    }
                }
                if (level == 2)
                {
                    for (int i = 0; i < shot.Count; i++)
                    {
                        int tempX = shot[i].x;
                        int tempY = shot[i].y;
                        if (shot[i].jenis == 0 || shot[i].jenis == 3)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(Convert.ToInt32(x_boss), y_boss, 200, 200);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                xblood.Add(tempX);
                                yblood.Add(tempY);
                                ctrblood.Add(0);
                                flash_life -= shot[i].dmg;
                                shot.RemoveAt(i);
                                break;
                            }
                        }
                        else if (shot[i].jenis == 1 || shot[i].jenis == 5)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(Convert.ToInt32(x_boss), y_boss, 200, 200);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                xblood.Add(tempX);
                                yblood.Add(tempY);
                                ctrblood.Add(0);
                                flash_life -= shot[i].dmg;
                                break;
                            }
                        }
                        else if (shot[i].jenis == 2)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(Convert.ToInt32(x_boss), y_boss, 200, 200);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                flash_life -= shot[i].dmg;
                                break;
                            }
                        }
                        else if (shot[i].jenis == 4)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(Convert.ToInt32(x_boss), y_boss, 200, 200);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                shot.Add(new ammo(6, shot[i].x, shot[i].y, shot[i].arah));
                                sfx_weapon[6].Play();
                                shot.RemoveAt(i);
                                break;
                            }
                        }
                        else if (shot[i].jenis == 6)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(Convert.ToInt32(x_boss), y_boss, 200, 200);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                flash_life -= shot[i].dmg;
                                break;
                            }
                        }
                    }


                    if (flash_life <= 0)
                    {
                        nextStage();
                        bossFight = false;
                    }
                }else if(level == 4)
                {
                    for (int i = 0; i < shot.Count; i++)
                    {
                        int tempX = shot[i].x;
                        int tempY = shot[i].y;
                        if (shot[i].jenis == 0 || shot[i].jenis == 3)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(550, 100, 500, 500);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                xblood.Add(tempX);
                                yblood.Add(tempY);
                                ctrblood.Add(0);
                                hp_boss4 -= shot[i].dmg;
                                shot.RemoveAt(i);
                                break;
                            }
                        }
                        else if (shot[i].jenis == 1 || shot[i].jenis == 5)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(550, 100, 500, 500);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                xblood.Add(tempX);
                                yblood.Add(tempY);
                                ctrblood.Add(0);
                                hp_boss4 -= shot[i].dmg;
                                break;
                            }
                        }
                        else if (shot[i].jenis == 2)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY - 100, 200, 200);
                            Rectangle rZombie = new Rectangle(550, 100, 400, 400);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                hp_boss4 -= shot[i].dmg;
                                break;
                            }
                        }
                        else if (shot[i].jenis == 4)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(550, 100, 500, 500);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                shot.Add(new ammo(6, shot[i].x, shot[i].y, shot[i].arah));
                                sfx_weapon[6].Play();
                                shot.RemoveAt(i);
                                break;
                            }
                        }
                        else if (shot[i].jenis == 6)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY - 100, 200, 200);
                            Rectangle rZombie = new Rectangle(550, 100, 500, 500);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                hp_boss4 -= shot[i].dmg;
                                break;
                            }
                        }
                    }


                    if (hp_boss4 <= 0)
                    {
                        aniBoss4 = 8;
                        nextStage();
                    }
                }
                if (level == 1)
                {
                    for (int i = 0; i < shot.Count; i++)
                    {
                        int tempX = shot[i].x;
                        int tempY = shot[i].y;
                        if (shot[i].jenis == 0 || shot[i].jenis == 3)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(Convert.ToInt32(x_boss), y_boss-200, 200, 400);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                xblood.Add(tempX);
                                yblood.Add(tempY);
                                ctrblood.Add(0);
                                jugger_life -= shot[i].dmg;
                                shot.RemoveAt(i);
                                break;
                            }
                        }
                        else if (shot[i].jenis == 1 || shot[i].jenis == 5)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(Convert.ToInt32(x_boss), y_boss - 200, 200, 400);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                xblood.Add(tempX);
                                yblood.Add(tempY);
                                ctrblood.Add(0);
                                jugger_life -= shot[i].dmg;
                                break;
                            }
                        }
                        else if (shot[i].jenis == 2)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(Convert.ToInt32(x_boss), y_boss - 200, 200, 400);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                jugger_life -= shot[i].dmg;
                                break;
                            }
                        }
                        else if (shot[i].jenis == 4)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(Convert.ToInt32(x_boss), y_boss - 200, 200, 400);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                shot.Add(new ammo(6, shot[i].x, shot[i].y, shot[i].arah));
                                sfx_weapon[6].Play();
                                shot.RemoveAt(i);
                                break;
                            }
                        }
                        else if (shot[i].jenis == 6)
                        {
                            Rectangle rAmmo = new Rectangle(tempX, tempY, 5, 5);
                            Rectangle rZombie = new Rectangle(Convert.ToInt32(x_boss), y_boss - 200, 200, 400);
                            if (rAmmo.IntersectsWith(rZombie))
                            {
                                jugger_life -= shot[i].dmg;
                                break;
                            }
                        }
                    }


                    if (jugger_life <= 0)
                    {
                        bossFight = false;
                        nextStage();
                    }
                }
            }
            Invalidate();
        }

        private void timerShot_Tick(object sender, EventArgs e)
        {
            nembak(0);
        }

        private void nembak(int a)
        {
            //nembak kiri/kanan
            if (a == 0)
            {
                if (ctrWeapon >= cdWeapon)
                {
                    if (hadap == 1)
                    {
                        currAnim = animPlayer[13];
                    }
                    else
                    {
                        Bitmap bmp = new Bitmap(animPlayer[13]);
                        bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                        currAnim = bmp;
                    }
                    ctrWeapon = 0;
                    int tempX = x;
                    if (hadap == 1)
                    {
                        tempX += 90;
                    }
                    else
                    {
                        tempX -= 90;
                    }
                    int addSmg = 0;
                    if (p.weapon == 3)
                    {
                        if (ctrSmg == 0)
                        {
                            ctrSmg = 1;
                            addSmg = 5;
                        }
                        else
                        {
                            ctrSmg = 0;
                            addSmg = -5;
                        }
                    }
                    shot.Add(new ammo(p.weapon, tempX, y + 100 + addSmg, hadap));
                    p.ammunition--;

                    if (p.ammunition <= 0)
                    {
                        p.ammunition = -1;
                        p.weapon = 0;
                        cdWeapon = 500;
                    }
                    sfx_weapon[p.weapon].Play();
                }
            }//nembak atas
            else
            {
                if (ctrWeapon >= cdWeapon)
                {
                    if (hadap == 1)
                    {
                        currAnim = animPlayer[13];
                    }
                    else
                    {
                        Bitmap bmp = new Bitmap(animPlayer[13]);
                        bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                        currAnim = bmp;
                    }
                    ctrWeapon = 0;
                    int tempX = x;
                    if (hadap == 1)
                    {
                        tempX += 90;
                    }
                    else
                    {
                        tempX -= 90;
                    }
                    int addSmg = 0;
                    if (p.weapon == 3)
                    {
                        if (ctrSmg == 0)
                        {
                            ctrSmg = 1;
                            addSmg = 5;
                        }
                        else
                        {
                            ctrSmg = 0;
                            addSmg = -5;
                        }
                    }
                    shot.Add(new ammo(p.weapon, tempX, y + 100 + addSmg, 0));
                    p.ammunition--;

                    if (p.ammunition <= 0)
                    {
                        p.ammunition = -1;
                        p.weapon = 0;
                        cdWeapon = 500;
                    }
                    sfx_weapon[p.weapon].Play();
                }
            }
            
        }
        int ctrShotgun = 0;
        int ctrExplode = 0;
        private void timerMove_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < bot.Count; i++)
            {
                if (bot[i].life > 0)
                {
                    if(bot[i].jenis == 0 || bot[i].jenis == 2)
                    {
                        bot[i].x -= 20;
                    }else if(bot[i].jenis == 1 || bot[i].jenis == 4)
                    {
                        bot[i].x -= 40;
                    }else if(bot[i].jenis == 3 || bot[i].jenis == 5)
                    {
                        bot[i].x -= 30;
                    }
                    {

                    }
                    bot[i].ani++;
                    if (bot[i].ani > 2)
                    {
                        bot[i].ani = 0;
                    }
                }
                else
                {
                    bot[i].ani++;
                    if (bot[i].ani > 5)
                    {
                        if(bot[i].jenis == 0)
                        {
                            p.score += 10;
                        }else if(bot[i].jenis == 1)
                        {
                            p.score += 30;
                        }else if(bot[i].jenis == 2)
                        {
                            p.score += 60;
                        }else if(bot[i].jenis == 3)
                        {
                            p.score += 80;
                        }else if(bot[i].jenis == 4)
                        {
                            p.score += 100;
                        }else if(bot[i].jenis == 5)
                        {
                            p.score += 150;
                        }
                        bot.RemoveAt(i);
                        break;
                    }
                }

                if (bot.Count > 0)
                {
                    if (bot[i].x < x + 50 && bot[i].x > x - 50)
                    {
                        if (p.life > -1 && ctrPlayerMati == 0)
                        {
                            if (!p.immortal)
                            {
                                keyDisabled = true;
                                timerIdle.Stop();
                                timerJalan.Stop();
                                timerJump.Stop();
                                timerPlayerMati.Start();
                                p.life--;
                                ctrImmortal = 0;
                                p.immortal = true;
                                timerImmortal.Start();
                            }
                        }
                    }
                }
                
            }
            Invalidate();
        }
        
        private void timerNextStage_Tick(object sender, EventArgs e)
        {
            if (x_map == -800 * stage)
            {
                if(stage < 5)
                {
                    timerNextStage.Stop();
                    timerZombie.Start();
                    timerMove.Start();
                    timerBonus.Start();
                    keyDisabled = false;
                }
                else
                {
                    //BOSS STAGE
                    if(level == 1)
                    {
                        parent.bgplay(3);
                    }else if(level == 2)
                    {
                        parent.bgplay(5);
                    }else if(level == 3)
                    {
                        parent.bgplay(7);
                    }else if(level == 4)
                    {
                        parent.bgplay(9);
                    }else if(level == 5)
                    {
                        parent.bgplay(11);
                    }else if(level == 6)
                    {
                        parent.bgplay(13);
                    }
                    timerNextStage.Stop();
                    timerBonus.Start();
                    keyDisabled = false;
                    bossFight = true;
                    timerBoss.Start();

                    if (level == 1 && stage == 5)
                    {
                        timer1Detik.Start();
                    }
                    
                }
            }
            else
            {
                x_map-=10;
                x -= 10;
                if(x < 0)
                {
                    x = 0;
                }
                Invalidate();
            }

            //GERAKIN PLAYER SAMBIL ARENA GERAK
            if (ctrJalan == 1)
            {
                currAnim = animPlayer[2];
            }
            if (ctrJalan == 2)
            {
                currAnim = animPlayer[3];
            }
            if (ctrJalan == 3)
            {
                currAnim = animPlayer[4];
                ctrJalan = 0;
            }
            ctrJalan++;
            Invalidate();
        }
        int ctrPlayerMati = 0;
        private void timerPlayerMati_Tick(object sender, EventArgs e)
        {
            keyDisabled = true;
            if (hadap == 1)
            {
                currAnim = animPlayer[ctrPlayerMati + 6];
            }
            else
            {
                Bitmap bmp = new Bitmap(animPlayer[ctrPlayerMati + 6]);
                bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                currAnim = bmp;
            }
            

            if (ctrPlayerMati == 6)
            {
                currAnim = animPlayer[0] ;
                timerPlayerMati.Stop();
                keyDisabled = false;
                ctrPlayerMati = -1;
                bot.Clear();
                enemyNow = 0;
                gameover();
            }
            ctrPlayerMati++;
            Invalidate();
        }
        float ctrLootBox = 0;
        private void timerLootBox_Tick(object sender, EventArgs e)
        {
            y_lootbox = ctrLootBox * 100;

            if (ctrLootBox == 5.5)
            {
                timerLootBox.Stop();
                ctrLootBox = 0;
            }
            Invalidate();
            ctrLootBox+=(float)0.5;
        }

        private void timerCooldown_Tick(object sender, EventArgs e)
        {
            ctrWeapon += 100;
        }

        private void timerBonus_Tick(object sender, EventArgs e)
        {
            int jenis = r.Next(2);
            if(jenis == 0)
            {
                y_lootbox = 0;
                ctrLootBox = 0;
                x_lootbox = r.Next(100, 500);
                lootOpened = false;
                timerLootBox.Start();
            }else if(jenis == 1)
            {
                timerChild.Start();
            }
        }

        int aniChild = 0;
        //0-2= move
        //3-9 = esc
        public void collChild()
        {
            if (x > x_child - 50 && x < x_child + 50 && !ctrChild)
            {
                aniChild = 3;
                timerChild.Interval = 250;
                ctrChild = true;
            }
        }
        int ctrImmortal = 0;
        public void collItem()
        {
            for (int i = 0; i < drop.Count; i++)
            {
                if (x > drop[i].x - 50 && x < drop[i].x + 50)
                {
                    if(drop[i].jenis == 0)
                    {
                        for (int j = 0; j < bot.Count; j++)
                        {
                            bot[j].life = 0;
                            bot[j].ani = 3;
                        }
                    }else if(drop[i].jenis == 1)
                    {
                        p.life++;
                    }else if(drop[i].jenis == 2)
                    {
                        p.score += 1000;
                    }else if(drop[i].jenis == 3)
                    {
                        p.score += 500;
                    }else if(drop[i].jenis == 4)
                    {
                        ctrImmortal = 0;
                        p.immortal = true;
                        timerImmortal.Start();
                    }else if(drop[i].jenis == 5)
                    {
                        int a = r.Next(1, 6);
                        p.weapon = a;

                        if (p.weapon == 1)
                        {
                            cdWeapon = 1000;
                            p.ammunition = 10;
                        }
                        else if (p.weapon == 2)
                        {
                            cdWeapon = 2000;
                            p.ammunition = 15;
                        }
                        else if (p.weapon == 3)
                        {
                            cdWeapon = 100;
                            p.ammunition = 200;
                        }
                        else if (p.weapon == 4)
                        {
                            cdWeapon = 3000;
                            p.ammunition = 15;
                        }
                        else if (p.weapon == 5)
                        {
                            cdWeapon = 3000;
                            p.ammunition = 10;
                        }
                    }

                    drop.RemoveAt(i);
                }
            }
        }

        private void timerChild_Tick(object sender, EventArgs e)
        {
            //CHECKPOINT
            if (aniChild < 3)
            {
                x_child -= 10;
                aniChild++;
                if(aniChild > 2)
                {
                    aniChild = 0;
                }
                collChild();
            }
            else
            {
                aniChild++;
                if(aniChild == 10)
                {
                    aniChild = 0;
                    timerChild.Interval = 100;
                    //drop barang
                    int r_jenis = r.Next(6);
                    drop.Add(new item(0, x_child));
                }
            }
            if(x_child < -200)
            {
                timerChild.Stop();
                x_child = 800;
                ctrChild = false;
                aniChild = 0;
            }
            Invalidate();
        }

        private void timerImmortal_Tick(object sender, EventArgs e)
        {
            ctrImmortal++;
            if(ctrImmortal > 10)
            {
                p.immortal = false;
                timerImmortal.Stop();
            }
        }
        
        //VARIABLE BOSS 2
        int ctrTimerBoss = 0;
        float x_boss = 599;
        int y_boss = 400;
        float boss_hadap = -1;

        //VARIABLE BOSS 5
        List<ammo> enemyAmmo = new List<ammo>();
        int ctrBoss5 = 0;
        int aniBoss5 = 0;
        int hp_boss5 = 650;

        //VARIABLE BOSS 4
        int ctrBoss4 = 0;
        int aniBoss4 = 0;
        int hp_boss4 = 500;
        int ctrLaser = -1;
        int desLaser = 0;
        

        public void resetBoss()
        {
            //VARIABLE BOSS 2
            ctrTimerBoss = 0;
            flash_life = 250;
            jugger_life = 500;
            bossFight = false;
            x_boss = 599;
            y_boss = 400;
            boss_hadap = -1;
            currBossAnim = null;

            //VARIABLE BOSS 5
            enemyAmmo = new List<ammo>();
            ctrBoss5 = 0;
            aniBoss5 = 0;
            hp_boss5 = 650;
            bossFight = false;

            //VARIABLE BOSS 4
            ctrBoss4 = 0;
            aniBoss4 = 0;
            hp_boss4 = 500;
            ctrLaser = -1;
            desLaser = 0;

            //var 1
            timerJuggerAtt.Stop();
            timer1Detik.Stop();
            jugger_life = 500;
            ctrJuggerAtt = 0;
            ctrTimerBoss = 0;


            timerBoss.Stop();
        }

        private void timerBoss_Tick(object sender, EventArgs e)
        {
            if (level == 2)
            {
                if (boss_hadap == 1)
                {
                    currBossAnim = listFlash[ctrTimerBoss];
                }
                else
                {
                    Bitmap bmp = new Bitmap(listFlash[ctrTimerBoss]);
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                    currBossAnim = bmp;
                }
                x_boss -= 10 * boss_hadap;

                ctrTimerBoss++;
                if (ctrTimerBoss == 3)
                {
                    ctrTimerBoss = 0;
                }


                if (x_boss <= 0 || x_boss >= 600)
                {
                    boss_hadap *= -1;
                }

                collBoss2();
            }
            else if (level == 5)
            {
                ctrBoss5++;
                if (ctrBoss5 == 100)
                {
                    enemyAmmo.Add(new ammo(-2, 667 - 20, 210 - 70, x));
                }
                else if (ctrBoss5 == 140)
                {
                    enemyAmmo.Add(new ammo(-2, 690 - 20, 213 - 70, x));
                }
                else if (ctrBoss5 == 180)
                {
                    enemyAmmo.Add(new ammo(-2, 710 - 20, 211 - 70, x));
                    ctrBoss5 = 0;
                }

                aniBoss5++;
                if (aniBoss5 > 2)
                {
                    aniBoss5 = 0;
                }

                Invalidate();
            }else if(level == 4)
            {
                ctrBoss4++;
                if(ctrBoss4 == 100 && hp_boss4 > 0)
                {
                    ctrBoss4 = 0;
                    desLaser = x;
                    aniBoss4 = 3;
                }

                if(aniBoss4 < 3)
                {
                    aniBoss4++;
                    if(aniBoss4 > 2)
                    {
                        aniBoss4 = 0;
                    }
                }
                else if(aniBoss4 > 3 && hp_boss4 > 0)
                {
                    ctrBoss4++;
                    if(ctrBoss4 > 10)
                    {
                        ctrBoss4 = 0;
                        aniBoss4++;
                        if(aniBoss4 == 4)
                        {
                            desLaser = x;
                        }
                        if (aniBoss4 > 6)
                        {
                            ctrLaser = 0;
                            Invalidate();
                            boss4Shot.Start();
                        }
                        if(aniBoss4 > 7)
                        {
                            aniBoss4 = 0;
                        }
                    }

                }
                else
                {
                    ctrBoss4++;
                    if(ctrBoss4 > 10)
                    {
                        ctrBoss4 = 0;
                        aniBoss4++;
                        if(aniBoss4 > 10)
                        {
                            bossFight = false;
                            done = true;
                        }
                        Invalidate();
                    }
                }
                collLaser();
            }else if (level == 1)
            {
                timerBoss.Interval = 500;
                if (boss_hadap == -1)
                {
                    currBossAnim = imageJugger[ctrTimerBoss];
                }
                else
                {
                    Bitmap bmp = new Bitmap(imageJugger[ctrTimerBoss]);
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                    currBossAnim = bmp;
                }

                x_boss -= 10 * boss_hadap;

                if (x_boss <= 0 || x_boss >= 600)
                {
                    boss_hadap *= -1;
                }
                
                ctrTimerBoss++;
                if (ctrTimerBoss == 3)
                {
                    ctrTimerBoss = 0;
                }

                if (boss_hadap == 1)
                {
                    if (x > x_boss)
                    {
                        boss_hadap *= -1;
                    }
                }else if (boss_hadap == -1)
                {
                    if (x < x_boss)
                    {
                        boss_hadap *= -1;
                        
                    }
                }
            }
        }
        //BOSS 4 PUNYA AING
        public void boss4_shot()
        {
            boss4Shot.Start();
        }



        public void collLaser()
        {
            Rectangle rplayer = new Rectangle(x, y + 40, 120, 160);
            Rectangle hitarea = new Rectangle(desLaser, 500, 30, 30);

            if(ctrLaser > -1)
            {
                if(p.immortal == false)
                {
                    if (rplayer.IntersectsWith(hitarea) == true)
                    {
                        timerPlayerMati.Start();
                        ctrImmortal = 0;
                        p.immortal = true;
                        timerImmortal.Start();
                        p.life--;
                    }
                }
            }
        }

        private void boss4Shot_Tick(object sender, EventArgs e)
        {
            ctrLaser++;
            if(ctrLaser > 0)
            {
                ctrLaser = -1;
                boss4Shot.Stop();
            }
            Invalidate();
        }

        //BOSS 2 PUNYA YAAA
        public void collBoss2()
        {
            if(p.immortal == false)
            {
                if (boss_hadap == -1)
                {
                    if (x <= x_boss + 250 && x >= x_boss + 150 && y == 400)
                    {
                        ctrImmortal = 0;
                        p.immortal = true;
                        timerImmortal.Start();
                        timerPlayerMati.Start();
                        p.life--;
                    }
                }
                else
                {
                    if (x >= x_boss - 100 && x <= x_boss && y == 400)
                    {
                        timerPlayerMati.Start();
                        ctrImmortal = 0;
                        p.immortal = true;
                        timerImmortal.Start();
                        p.life--;
                    }
                }
                
            }
            
        }
        
        //BOSS 5 PUNYA JANGAN DIUBAH BARISAN
        public void boss5_ammoColl()
        {
            for (int i = 0; i < enemyAmmo.Count; i++)
            {
                Rectangle rplayer = new Rectangle(x, y + 40, 120, 160);
                if (hadap == -1)
                {
                    rplayer.X -= 30;
                }
                Rectangle rammo = new Rectangle(enemyAmmo[i].x, enemyAmmo[i].y, 50, 50);

                if (p.immortal == false)
                {
                    if (rplayer.IntersectsWith(rammo))
                    {
                        timerPlayerMati.Start();
                        ctrImmortal = 0;
                        p.immortal = true;
                        timerImmortal.Start();
                        p.life--;
                        enemyAmmo[i].jenis = -3;
                    }
                }

                if (enemyAmmo[i].y > 600)
                {
                    enemyAmmo.RemoveAt(i);
                    break;
                }
            }
        }

        public void boss5_ammoMove()
        {
            for (int i = 0; i < enemyAmmo.Count; i++)
            {
                int speed = (int)Math.Abs(680 - x) / 40;
                if (enemyAmmo[i].jenis == -2)
                {
                    enemyAmmo[i].jenis = -1;
                }
                else if (enemyAmmo[i].jenis < -2)
                {
                    enemyAmmo[i].jenis--;
                    if (enemyAmmo[i].jenis == -10)
                    {
                        enemyAmmo.RemoveAt(i);
                        break;
                    }
                }
                if (enemyAmmo[i].x < enemyAmmo[i].arah)
                {
                    enemyAmmo[i].x += speed;
                }
                else if (enemyAmmo[i].x > enemyAmmo[i].arah)
                {
                    enemyAmmo[i].x -= speed;
                }
                enemyAmmo[i].y += 10;
            }
            boss5_ammoColl();
            Invalidate();
        }

        private void stage1_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(e.X+" "+e.Y);
        }

        private void timerShotAtas_Tick(object sender, EventArgs e)
        {
            nembak(1);
        }

        int ctrJuggerAtt = 0;
        private void timerJuggerAtt_Tick(object sender, EventArgs e)
        {
            if (boss_hadap == -1)
            {
                
                currBossAnim = imageJugger[ctrJuggerAtt + 3];
            }
            else
            {
                Bitmap bmp = new Bitmap(imageJugger[ctrJuggerAtt + 3]);
                bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
                currBossAnim = bmp;
            }

            if (ctrJuggerAtt == 5)
            {
                if (boss_hadap == 1)
                {
                    if (x < x_boss)
                    {
                        ctrImmortal = 0;
                        p.immortal = true;
                        timerImmortal.Start();
                        timerPlayerMati.Start();
                        p.life--;
                    }
                }
                if (x < x_boss + 50 && x > x_boss - 50)
                {
                    ctrImmortal = 0;
                    p.immortal = true;
                    timerImmortal.Start();
                    timerPlayerMati.Start();
                    p.life--;
                }
            }

            ctrJuggerAtt++;

            if (ctrJuggerAtt == 6)
            {
                ctrJuggerAtt = 0;
                timerBoss.Start();
                timerJuggerAtt.Stop();
                timer1Detik.Start();
            }
        }

        int detik = 1;
        private void timer1Detik_Tick(object sender, EventArgs e)
        {
            if (level == 1 && stage == 5)
            {
                if (detik % 5 == 0)
                {
                    timerJuggerAtt.Start();
                    timerBoss.Stop();
                    detik++;
                    timer1Detik.Stop();
                }
            }
           
            detik++;
        }

        private void timerBlood_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < xblood.Count; i++)
            {
                ctrblood[i]++;
                if(ctrblood[i] == 2)
                {
                    ctrblood.RemoveAt(i);
                    xblood.RemoveAt(i);
                    yblood.RemoveAt(i);
                }
            }
            Invalidate();
        }
    }
}
