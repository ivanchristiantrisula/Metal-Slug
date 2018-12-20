namespace games
{
    partial class stage1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerIdle = new System.Windows.Forms.Timer(this.components);
            this.timerJalan = new System.Windows.Forms.Timer(this.components);
            this.timerJump = new System.Windows.Forms.Timer(this.components);
            this.timerZombie = new System.Windows.Forms.Timer(this.components);
            this.timerAmmo = new System.Windows.Forms.Timer(this.components);
            this.timerShot = new System.Windows.Forms.Timer(this.components);
            this.timerMove = new System.Windows.Forms.Timer(this.components);
            this.timerNextStage = new System.Windows.Forms.Timer(this.components);
            this.timerPlayerMati = new System.Windows.Forms.Timer(this.components);
            this.timerLootBox = new System.Windows.Forms.Timer(this.components);
            this.timerCooldown = new System.Windows.Forms.Timer(this.components);
            this.timerBonus = new System.Windows.Forms.Timer(this.components);
            this.timerChild = new System.Windows.Forms.Timer(this.components);
            this.timerImmortal = new System.Windows.Forms.Timer(this.components);
            this.timerBoss = new System.Windows.Forms.Timer(this.components);
            this.timerShotAtas = new System.Windows.Forms.Timer(this.components);
            this.boss4Shot = new System.Windows.Forms.Timer(this.components);
            this.timerJuggerAtt = new System.Windows.Forms.Timer(this.components);
            this.timer1Detik = new System.Windows.Forms.Timer(this.components);
            this.timerBlood = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerIdle
            // 
            this.timerIdle.Tick += new System.EventHandler(this.timerIdle_Tick);
            // 
            // timerJalan
            // 
            this.timerJalan.Interval = 50;
            this.timerJalan.Tick += new System.EventHandler(this.timerJalan_Tick);
            // 
            // timerJump
            // 
            this.timerJump.Tick += new System.EventHandler(this.timerJump_Tick);
            // 
            // timerZombie
            // 
            this.timerZombie.Interval = 1500;
            this.timerZombie.Tick += new System.EventHandler(this.timerZombie_Tick);
            // 
            // timerAmmo
            // 
            this.timerAmmo.Interval = 10;
            this.timerAmmo.Tick += new System.EventHandler(this.timerAmmo_Tick);
            // 
            // timerShot
            // 
            this.timerShot.Tick += new System.EventHandler(this.timerShot_Tick);
            // 
            // timerMove
            // 
            this.timerMove.Interval = 500;
            this.timerMove.Tick += new System.EventHandler(this.timerMove_Tick);
            // 
            // timerNextStage
            // 
            this.timerNextStage.Interval = 5;
            this.timerNextStage.Tick += new System.EventHandler(this.timerNextStage_Tick);
            // 
            // timerPlayerMati
            // 
            this.timerPlayerMati.Interval = 250;
            this.timerPlayerMati.Tick += new System.EventHandler(this.timerPlayerMati_Tick);
            // 
            // timerLootBox
            // 
            this.timerLootBox.Interval = 250;
            this.timerLootBox.Tick += new System.EventHandler(this.timerLootBox_Tick);
            // 
            // timerCooldown
            // 
            this.timerCooldown.Enabled = true;
            this.timerCooldown.Tick += new System.EventHandler(this.timerCooldown_Tick);
            // 
            // timerBonus
            // 
            this.timerBonus.Interval = 10000;
            this.timerBonus.Tick += new System.EventHandler(this.timerBonus_Tick);
            // 
            // timerChild
            // 
            this.timerChild.Enabled = true;
            this.timerChild.Tick += new System.EventHandler(this.timerChild_Tick);
            // 
            // timerImmortal
            // 
            this.timerImmortal.Interval = 1000;
            this.timerImmortal.Tick += new System.EventHandler(this.timerImmortal_Tick);
            // 
            // timerBoss
            // 
            this.timerBoss.Interval = 50;
            this.timerBoss.Tick += new System.EventHandler(this.timerBoss_Tick);
            // 
            // timerShotAtas
            // 
            this.timerShotAtas.Tick += new System.EventHandler(this.timerShotAtas_Tick);
            // 
            // boss4Shot
            // 
            this.boss4Shot.Interval = 1000;
            this.boss4Shot.Tick += new System.EventHandler(this.boss4Shot_Tick);
            // 
            // timerJuggerAtt
            // 
            this.timerJuggerAtt.Interval = 500;
            this.timerJuggerAtt.Tick += new System.EventHandler(this.timerJuggerAtt_Tick);
            // 
            // timer1Detik
            // 
            this.timer1Detik.Interval = 1000;
            this.timer1Detik.Tick += new System.EventHandler(this.timer1Detik_Tick);
            // 
            // timerBlood
            // 
            this.timerBlood.Interval = 500;
            this.timerBlood.Tick += new System.EventHandler(this.timerBlood_Tick);
            // 
            // stage1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 601);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "stage1";
            this.Text = "stage1";
            this.Load += new System.EventHandler(this.stage1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.stage1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.stage1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.stage1_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.stage1_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerIdle;
        private System.Windows.Forms.Timer timerJalan;
        private System.Windows.Forms.Timer timerJump;
        private System.Windows.Forms.Timer timerZombie;
        private System.Windows.Forms.Timer timerAmmo;
        private System.Windows.Forms.Timer timerShot;
        private System.Windows.Forms.Timer timerMove;
        private System.Windows.Forms.Timer timerNextStage;
        private System.Windows.Forms.Timer timerPlayerMati;
        private System.Windows.Forms.Timer timerLootBox;
        private System.Windows.Forms.Timer timerCooldown;
        private System.Windows.Forms.Timer timerBonus;
        private System.Windows.Forms.Timer timerChild;
        private System.Windows.Forms.Timer timerImmortal;
        private System.Windows.Forms.Timer timerBoss;
        private System.Windows.Forms.Timer timerShotAtas;
        private System.Windows.Forms.Timer boss4Shot;
        private System.Windows.Forms.Timer timerJuggerAtt;
        private System.Windows.Forms.Timer timer1Detik;
        private System.Windows.Forms.Timer timerBlood;
    }
}