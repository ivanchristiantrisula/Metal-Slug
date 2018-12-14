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
    public partial class startmenu : Form
    {
        int prog = 1;
        //1 = New Game
        //2 = Load Game
        //3 = Exit
        public startmenu()
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile("image/background/bg_startmenu.jpg");
        }

        private void startmenu_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(800, 600);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void next()
        {
            prog++;
            if(prog > 3)
            {
                prog = 1;
            }

            if(prog == 1)
            {
                label1.Text = "< New Game >";
            }else if(prog == 2)
            {
                label1.Text = "< Load Game >";
            }
            else
            {
                label1.Text = "< Exit >";
            }
        }
        public void enter()
        {
            Form1 parent = (Form1)this.MdiParent;
            if(prog == 3)
            {
                if(MessageBox.Show("Are you sure ?","EXIT",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1)==DialogResult.Yes)
                parent.Close();
            }else if(prog == 1)
            {
                parent.goForm(2);
                parent.setGame(0);
            }else if(prog == 2)
            {
                parent.goForm(2);
                parent.setGame(1);
            }
        }
        public void left()
        {
            prog--;
            if (prog < 1)
            {
                prog = 3;
            }

            if (prog == 1)
            {
                label1.Text = "< New Game >";
            }
            else if (prog == 2)
            {
                label1.Text = "< Load Game >";
            }
            else
            {
                label1.Text = "< Exit >";
            }
        }
    }
}
