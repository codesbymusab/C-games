namespace Flappy_Copter
{
    partial class MenuScreen
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuScreen));
            hpBox = new PictureBox();
            titleBox = new PictureBox();
            btn_leaderboard = new Button();
            btn_profile = new Button();
            btn_play = new Button();
            button1 = new Button();
            difficulityScreen1 = new DifficulityScreen();
            highScoresScreen1 = new HighScoresScreen();
            playingScreen1 = new PlayingScreen();
            ((System.ComponentModel.ISupportInitialize)hpBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)titleBox).BeginInit();
            SuspendLayout();
            // 
            // hpBox
            // 
            hpBox.Anchor = AnchorStyles.None;
            hpBox.BackColor = Color.Transparent;
            hpBox.BackgroundImage = Properties.Resources.helicopter1;
            hpBox.BackgroundImageLayout = ImageLayout.Zoom;
            hpBox.Location = new Point(340, 181);
            hpBox.Name = "hpBox";
            hpBox.Size = new Size(325, 198);
            hpBox.SizeMode = PictureBoxSizeMode.AutoSize;
            hpBox.TabIndex = 19;
            hpBox.TabStop = false;
            // 
            // titleBox
            // 
            titleBox.Anchor = AnchorStyles.None;
            titleBox.BackColor = Color.Transparent;
            titleBox.BackgroundImage = Properties.Resources.bold_title;
            titleBox.BackgroundImageLayout = ImageLayout.Zoom;
            titleBox.Location = new Point(188, -60);
            titleBox.Name = "titleBox";
            titleBox.Size = new Size(649, 340);
            titleBox.SizeMode = PictureBoxSizeMode.AutoSize;
            titleBox.TabIndex = 18;
            titleBox.TabStop = false;
            // 
            // btn_leaderboard
            // 
            btn_leaderboard.Anchor = AnchorStyles.None;
            btn_leaderboard.BackColor = Color.FromArgb(255, 128, 255);
            btn_leaderboard.Font = new Font("Showcard Gothic", 12.096F);
            btn_leaderboard.Location = new Point(430, 502);
            btn_leaderboard.Name = "btn_leaderboard";
            btn_leaderboard.Size = new Size(172, 45);
            btn_leaderboard.TabIndex = 17;
            btn_leaderboard.Text = "LEADERBOARDS";
            btn_leaderboard.UseVisualStyleBackColor = false;
            // 
            // btn_profile
            // 
            btn_profile.Anchor = AnchorStyles.None;
            btn_profile.BackColor = Color.Yellow;
            btn_profile.Font = new Font("Showcard Gothic", 12.096F);
            btn_profile.Location = new Point(430, 444);
            btn_profile.Name = "btn_profile";
            btn_profile.Size = new Size(172, 41);
            btn_profile.TabIndex = 16;
            btn_profile.Text = "DIFFICULITY";
            btn_profile.UseVisualStyleBackColor = false;
            // 
            // btn_play
            // 
            btn_play.Anchor = AnchorStyles.None;
            btn_play.BackColor = Color.FromArgb(128, 255, 128);
            btn_play.Font = new Font("Showcard Gothic", 12.096F);
            btn_play.Location = new Point(430, 385);
            btn_play.Name = "btn_play";
            btn_play.Size = new Size(172, 41);
            btn_play.TabIndex = 15;
            btn_play.Text = "PLAY";
            btn_play.UseVisualStyleBackColor = false;
            btn_play.Click += btn_play_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(486, 369);
            button1.Name = "button1";
            button1.Size = new Size(43, 10);
            button1.TabIndex = 21;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // difficulityScreen1
            // 
            difficulityScreen1.BackColor = Color.SaddleBrown;
            difficulityScreen1.BorderStyle = BorderStyle.Fixed3D;
            difficulityScreen1.Location = new Point(262, 65);
            difficulityScreen1.Name = "difficulityScreen1";
            difficulityScreen1.Size = new Size(477, 482);
            difficulityScreen1.TabIndex = 25;
            difficulityScreen1.Visible = false;
            // 
            // highScoresScreen1
            // 
            highScoresScreen1.BackColor = Color.SaddleBrown;
            highScoresScreen1.Location = new Point(262, 0);
            highScoresScreen1.Name = "highScoresScreen1";
            highScoresScreen1.Size = new Size(538, 567);
            highScoresScreen1.TabIndex = 26;
            highScoresScreen1.Visible = false;
            // 
            // playingScreen1
            // 
            playingScreen1.AutoSize = true;
            playingScreen1.BackColor = Color.Transparent;
            playingScreen1.BackgroundImage = (Image)resources.GetObject("playingScreen1.BackgroundImage");
            playingScreen1.BackgroundImageLayout = ImageLayout.Stretch;
            playingScreen1.Dock = DockStyle.Fill;
            playingScreen1.Location = new Point(0, 0);
            playingScreen1.Margin = new Padding(0);
            playingScreen1.Name = "playingScreen1";
            playingScreen1.Size = new Size(1040, 567);
            playingScreen1.TabIndex = 27;
            playingScreen1.Visible = false;
            // 
            // MenuScreen
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.menu_background;
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(playingScreen1);
            Controls.Add(highScoresScreen1);
            Controls.Add(difficulityScreen1);
            Controls.Add(button1);
            Controls.Add(hpBox);
            Controls.Add(titleBox);
            Controls.Add(btn_leaderboard);
            Controls.Add(btn_profile);
            Controls.Add(btn_play);
            DoubleBuffered = true;
            Name = "MenuScreen";
            Size = new Size(1040, 567);
            ((System.ComponentModel.ISupportInitialize)hpBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)titleBox).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        public PictureBox hpBox;
        public PictureBox titleBox;
        private Button btn_leaderboard;
        private Button btn_profile;
        private Button btn_play;
        private Button button1;
        private DifficulityScreen difficulityScreen1;
        private HighScoresScreen highScoresScreen1;
        private PlayingScreen playingScreen1;
    }
}
