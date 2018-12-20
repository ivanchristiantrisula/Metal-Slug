using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class item
    {
        public int jenis,x;
        public item(int jenis, int x)
        {
            this.jenis = jenis;
            this.x = x;
        }
    }

    public class Profile
    {
        public String nama;
        public int[] highscore = new int[6];
        public int stage;
        public int boss;

        public Profile(String nama)
        {
            this.nama = nama;
            for (int i = 0; i < 6; i++)
            {
                highscore[i] = 0;
            }
            stage = 6;
            boss = 0;
        }

        public Profile(String nama, int stage,int boss,int s1, int s2, int s3,int s4,int s5,int s6)
        {
            this.nama = nama;
            this.stage = stage;
            this.boss = boss;
            highscore[0] = s1;
            highscore[1] = s2;
            highscore[2] = s3;
            highscore[3] = s4;
            highscore[4] = s5;
            highscore[5] = s6;
        }
    }

    public class player
    {
        public int life, weapon, point, ammunition, score;
        public bool immortal;
        public player()
        {
            this.ammunition = -1;
            this.life = 3;
            this.score = 0;
            this.weapon = 0;
            this.point = 0;
            this.immortal = false;
        }
    }

    public class zombie
    {
        public int life, jenis, x, y, ani, arah;
        public zombie(int jenis)
        {
            this.jenis = jenis;
            this.arah = -1;
            this.y = 400;
            this.x = 800;
            this.ani = 0;
            //1 zombie
            //2 mutant
            //3 mummy
            //4 clown
            //5 soldier
            //6 frankestein
            if(jenis == 0)
            {
                this.life = 1;
            }else if(jenis == 1)
            {
                this.life = 3;
            }else if(jenis == 2)
            {
                this.life = 6;
            }else if(jenis == 3)
            {
                this.life = 8;
            }else if(jenis == 4)
            {
                this.life = 10;
            }else if(jenis == 5)
            {
                this.life = 13;
            }
        }
    }

    public class ammo
    {
        public int jenis, dmg, x, y, arah;
        public ammo(int i,int x, int y, int arah)
        {
            this.jenis = i;
            this.x = x;
            this.y = y;
            this.arah = arah;
            if(i == 0)
            {
                this.dmg = 1;
            }else if(i == 1)
            {
                this.dmg = 4;
            }else if(i == 2)
            {
                this.dmg = 4;
            }else if(i == 3)
            {
                this.dmg = 1;
            }else if(i == 4)
            {
                this.dmg = 4;
            }else if(i == 5)
            {
                this.dmg = 10;
            }else if(i == 6)
            {
                this.dmg = 4;
            }
        }
    }
}
