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
using System.Media;
using System.Xml;

namespace games
{
    public partial class Form1 : Form
    {
        //background music
        public void bgplay(int i)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            //0 = none
            //1 = mainmenu
            //2 = stage1
            //3 = boss1
            //4 = stage2
            //5 = boss2
            //6 = stage3
            //7 = boss3
            //8 = stage4
            //9 = boss4
            //10 = stage5
            //11 = boss5
            //12 = stage6
            //13 = boss6
            if(i == 1)
            {
                axWindowsMediaPlayer1.URL = "sfx/mainmenu.wav";
            }else if(i == 2)
            {
                axWindowsMediaPlayer1.URL = "sfx/theme_1.wav";
            }else if(i == 3)
            {
                axWindowsMediaPlayer1.URL = "sfx/boss_1.wav";
            }else if(i == 4)
            {
                axWindowsMediaPlayer1.URL = "sfx/theme_2.wav";
            }
            else if (i == 5)
            {
                axWindowsMediaPlayer1.URL = "sfx/boss_2.wav";
            }
            else if (i == 6)
            {
                axWindowsMediaPlayer1.URL = "sfx/theme_3.wav";
            }
            else if (i == 7)
            {
                axWindowsMediaPlayer1.URL = "sfx/boss_3.wav";
            }
            else if (i == 8)
            {
                axWindowsMediaPlayer1.URL = "sfx/theme_4.wav";
            }
            else if (i == 9)
            {
                axWindowsMediaPlayer1.URL = "sfx/boss_4.wav";
            }
            else if (i == 10)
            {
                axWindowsMediaPlayer1.URL = "sfx/theme_5.wav";
            }
            else if (i == 11)
            {
                axWindowsMediaPlayer1.URL = "sfx/boss_5.wav";
            }
            else if (i == 12)
            {
                axWindowsMediaPlayer1.URL = "sfx/theme_6.wav";
            }
            else if (i == 13)
            {
                axWindowsMediaPlayer1.URL = "sfx/boss_6.wav";
            }

            axWindowsMediaPlayer1.settings.setMode("loop",true);
            axWindowsMediaPlayer1.Ctlcontrols.play();
            if(i == 0)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
        }
        //sfx
        SoundPlayer menu_enter = new SoundPlayer("sfx/menu_enter.wav");
        SoundPlayer menu_switch = new SoundPlayer("sfx/menu_switch.wav");

        //profile
        public Profile player;
        public int playlvl = 0;

        //SAVE LOAD
        public void saveXML()
        {
            String name = player.nama+"-data.xml";
            XmlTextWriter wr = new XmlTextWriter(name, Encoding.UTF8);
            wr.WriteStartElement("Data");//<Data>
            wr.WriteElementString("nama", player.nama);
            wr.WriteElementString("stage", player.stage+"");
            wr.WriteElementString("boss", player.boss + "");
            wr.WriteElementString("s1", player.highscore[0]+"");
            wr.WriteElementString("s2", player.highscore[1] + "");
            wr.WriteElementString("s3", player.highscore[2] + "");
            wr.WriteElementString("s4", player.highscore[3] + "");
            wr.WriteElementString("s5", player.highscore[4] + "");
            wr.WriteElementString("s6", player.highscore[5] + "");
            wr.WriteEndElement();//</Data>
            wr.Close();
        }
        public void loadXML(String nama)
        {
            XmlTextReader read = new XmlTextReader(nama + "-data.xml");
            read.ReadStartElement("Data");
            String loadnama = read.ReadElementString("nama");
            int loadstage = Convert.ToInt32(read.ReadElementString("stage"));
            int loadboss = Convert.ToInt32(read.ReadElementString("boss"));
            int loads1 = Convert.ToInt32(read.ReadElementString("s1"));
            int loads2 = Convert.ToInt32(read.ReadElementString("s2"));
            int loads3= Convert.ToInt32(read.ReadElementString("s3"));
            int loads4= Convert.ToInt32(read.ReadElementString("s4"));
            int loads5= Convert.ToInt32(read.ReadElementString("s5"));
            int loads6= Convert.ToInt32(read.ReadElementString("s6"));
            player = new Profile(loadnama, loadstage, loadboss, loads1, loads2, loads3, loads4, loads5, loads6);
            read.ReadEndElement();
            read.Close();

            goForm(3);
        }
        //CTR
        int formNow = 1;

        //1 Start Menu
        //2 New Game
        //3 Main Menu
        //4 gallery
        //5 stage
        //6 highscore

        //ALL FORM
        highscore hs = new highscore();
        startmenu sm = new startmenu();
        newgame ng = new newgame();
        mainmenu mm = new mainmenu();
        gallery g = new gallery();
        stage s = new stage();
        public void setLevel(int lvl)
        {
            playlvl = lvl;
        }
        public void newgame(String nama)
        {
            player = new Profile(nama);
        }
        public void goForm(int i)
        {
            hs.Visible = false;
            sm.Visible = false;
            ng.Visible = false;
            mm.Visible = false;
            g.Visible = false;
            s.Visible = false;

            if(i == 1)
            {
                sm.Visible = true;
                sm.Location = new Point(0, 0);
                formNow = 1;
                bgplay(0);
            }
            else if(i == 2)
            {
                ng.Visible = true;
                ng.Location = new Point(0, 0);
                formNow = 2;
                bgplay(0);
            }
            else if(i == 3)
            {
                mm.Visible = true;
                mm.Location = new Point(0, 0);
                formNow = 3;
                bgplay(1);
                mm.repaint();
            }else if(i == 4)
            {
                g.Visible = true;
                g.Location = new Point(0, 0);
                formNow = 4;
                g.repaint();
            }else if(i == 5)
            {
                s.Visible = true;
                s.Location = new Point(0, 0);
                formNow = 5;
                s.repaint();
            }
            else if (i == 6)
            {
                stage1 ss = new stage1();
                ss.MdiParent = this;
                ss.Visible = true;
                ss.Location = new Point(0, 0);
                ss.relaunch(playlvl);
                ss.Show();
                formNow = 6;
            }else if(i == 7)
            {
                hs.Visible = true;
                hs.Location = new Point(0, 0);
                formNow = 7;
                hs.relaunch();
            }
        }
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Size = new Size(805, 605);

            //INITIALIZE CHILD
            sm.MdiParent = this;
            ng.MdiParent = this;
            mm.MdiParent = this;
            g.MdiParent = this;
            hs.MdiParent = this;
            s.MdiParent = this;            

            goForm(1);
        }
        int ctrGame = 0;
        //0 new game
        //1 loadgame
        public void setGame(int i)
        {
            ctrGame = i;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = new Point(100, 100);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(formNow == 1)
            {
                if(e.KeyCode == Keys.D)
                {
                    menu_switch.Play();
                    sm.next();
                }else if(e.KeyCode == Keys.A)
                {
                    menu_switch.Play();
                    sm.left();
                }else if(e.KeyCode == Keys.Enter)
                {
                    menu_enter.Play();
                    sm.enter();
                }
            }else if(formNow == 2)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if(ctrGame == 0)
                    {
                        menu_enter.Play();
                        if (ng.textBox1.Text == "")
                        {
                            MessageBox.Show("Name cannot be empty!");
                        }
                        else
                        {
                            MessageBox.Show("Registration Success!");
                            newgame(ng.textBox1.Text);
                            ng.textBox1.Text = "";
                            goForm(3);
                        }
                    }
                    else
                    {
                        try
                        {
                            loadXML(ng.textBox1.Text);
                            MessageBox.Show("Load Successfull!");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Save File Not Found!");
                        }
                        ng.textBox1.Text = "";
                    }
                    
                }
            }else if(formNow == 3)
            {
                if(e.KeyCode == Keys.S)
                {
                    mm.next();
                    menu_switch.Play();
                }else if(e.KeyCode == Keys.W)
                {
                    mm.prev();
                    menu_switch.Play();
                }else if(e.KeyCode == Keys.Enter)
                {
                    mm.enter();
                    menu_enter.Play();
                }
            }else if(formNow == 4)
            {
                if(e.KeyCode == Keys.D)
                {
                    menu_switch.Play();
                    g.next();
                }else if(e.KeyCode == Keys.Back)
                {
                    g.back();
                    menu_enter.Play();
                }else if(e.KeyCode == Keys.A)
                {
                    menu_switch.Play();
                    g.prev();
                }else if(e.KeyCode == Keys.Enter)
                {
                    menu_enter.Play();
                    g.enter();
                }
            }else if(formNow == 5)
            {
                if(e.KeyCode == Keys.D)
                {
                    menu_switch.Play();
                    s.next();
                }else if(e.KeyCode == Keys.A)
                {
                    menu_switch.Play();
                    s.prev();
                }else if(e.KeyCode == Keys.Back)
                {
                    goForm(3);
                    menu_enter.Play();
                }
            }else if(formNow == 7)
            {
                if(e.KeyCode == Keys.Back)
                {
                    goForm(3);

                }
            }
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            
        }
    }
}
