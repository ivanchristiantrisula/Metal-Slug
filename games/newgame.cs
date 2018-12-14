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

namespace games
{
    public partial class newgame : Form
    {
        public newgame()
        {
            InitializeComponent();
        }
        int ctr = 0;
        //0 new game
        //1 load game
        public void ctrNow(int i)
        {
            ctr = i;
        }

        private void newgame_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(800, 600);
            this.BackgroundImage = Image.FromFile("image/background/bg_newgame.jpg");
            
        }

        private void newgame_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
