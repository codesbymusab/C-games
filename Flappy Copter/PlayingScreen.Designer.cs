namespace Flappy_Copter
{
    partial class PlayingScreen
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
            components = new System.ComponentModel.Container();
            gameTimer = new System.Windows.Forms.Timer(components);
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            top_wall = new Panel();
            btm_wall = new Panel();
            start_label = new Label();
            obstacleTimer = new System.Windows.Forms.Timer(components);
            pauseScreen1 = new PauseScreen();
            Score = new Label();
            healthBar = new FlowLayoutPanel();
            gameOver1 = new GameOver();
            top_Boundary = new Panel();
            btm_boundary = new Panel();
            ShieldTimer = new System.Windows.Forms.Timer(components);
            highScore = new Label();
            SuspendLayout();
            // 
            // gameTimer
            // 
            gameTimer.Interval = 16;
            gameTimer.Tick += gameTimer_Tick;
            // 
            // top_wall
            // 
            top_wall.Anchor = AnchorStyles.Top;
            top_wall.Location = new Point(34, 0);
            top_wall.Margin = new Padding(0);
            top_wall.Name = "top_wall";
            top_wall.Size = new Size(1032, 1);
            top_wall.TabIndex = 0;
            // 
            // btm_wall
            // 
            btm_wall.Anchor = AnchorStyles.Bottom;
            btm_wall.BackColor = Color.Transparent;
            btm_wall.Location = new Point(34, 726);
            btm_wall.Margin = new Padding(0);
            btm_wall.Name = "btm_wall";
            btm_wall.Size = new Size(1032, 1);
            btm_wall.TabIndex = 1;
            // 
            // start_label
            // 
            start_label.Anchor = AnchorStyles.None;
            start_label.AutoSize = true;
            start_label.Font = new Font("Showcard Gothic", 17.855999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            start_label.ForeColor = Color.DarkMagenta;
            start_label.Location = new Point(404, 336);
            start_label.Name = "start_label";
            start_label.Padding = new Padding(10);
            start_label.Size = new Size(349, 58);
            start_label.TabIndex = 2;
            start_label.Text = "Tap To Start Game";
            start_label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // obstacleTimer
            // 
            obstacleTimer.Interval = 1000;
            obstacleTimer.Tick += obstacleTimer_Tick;
            // 
            // pauseScreen1
            // 
            pauseScreen1.BackColor = Color.Gold;
            pauseScreen1.BorderStyle = BorderStyle.Fixed3D;
            pauseScreen1.Location = new Point(295, 112);
            pauseScreen1.Name = "pauseScreen1";
            pauseScreen1.Size = new Size(475, 383);
            pauseScreen1.TabIndex = 3;
            pauseScreen1.Visible = false;
            pauseScreen1.Load += pauseScreen1_Load;
            // 
            // Score
            // 
            Score.AutoSize = true;
            Score.Font = new Font("Showcard Gothic", 13.8239994F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Score.ForeColor = Color.Gold;
            Score.Location = new Point(12, 13);
            Score.Name = "Score";
            Score.Size = new Size(99, 30);
            Score.TabIndex = 4;
            Score.Text = "Score:";
            // 
            // healthBar
            // 
            healthBar.Location = new Point(1025, 3);
            healthBar.Name = "healthBar";
            healthBar.Padding = new Padding(5);
            healthBar.Size = new Size(30, 40);
            healthBar.TabIndex = 5;
            // 
            // gameOver1
            // 
            gameOver1.BackColor = Color.Gold;
            gameOver1.Location = new Point(295, 112);
            gameOver1.Name = "gameOver1";
            gameOver1.Size = new Size(475, 383);
            gameOver1.TabIndex = 6;
            gameOver1.Visible = false;
            // 
            // top_Boundary
            // 
            top_Boundary.Location = new Point(3, 0);
            top_Boundary.Name = "top_Boundary";
            top_Boundary.Size = new Size(1090, 1);
            top_Boundary.TabIndex = 7;
            // 
            // btm_boundary
            // 
            btm_boundary.Location = new Point(0, 763);
            btm_boundary.Name = "btm_boundary";
            btm_boundary.Size = new Size(1090, 1);
            btm_boundary.TabIndex = 8;
            // 
            // ShieldTimer
            // 
            ShieldTimer.Interval = 5;
            // 
            // highScore
            // 
            highScore.AutoSize = true;
            highScore.Font = new Font("Showcard Gothic", 47.808F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            highScore.ForeColor = Color.Gold;
            highScore.Location = new Point(106, 473);
            highScore.Name = "highScore";
            highScore.Size = new Size(857, 102);
            highScore.TabIndex = 9;
            highScore.Text = "New High Score!!!";
            highScore.Visible = false;
            // 
            // PlayingScreen
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.Transparent;
            BackgroundImage = Properties.Resources.playing_background;
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(highScore);
            Controls.Add(btm_boundary);
            Controls.Add(top_Boundary);
            Controls.Add(gameOver1);
            Controls.Add(healthBar);
            Controls.Add(Score);
            Controls.Add(pauseScreen1);
            Controls.Add(start_label);
            Controls.Add(btm_wall);
            Controls.Add(top_wall);
            DoubleBuffered = true;
            Margin = new Padding(0);
            Name = "PlayingScreen";
            Size = new Size(1108, 767);
            VisibleChanged += PlayingScreen_VisibleChanged;
            Click += PlayingScreen_Click;
            KeyDown += PlayingScreen_KeyDown;
            ResumeLayout(false);
            PerformLayout();


        }

        #endregion
        private System.Windows.Forms.Timer gameTimer;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel top_wall;
        private Panel btm_wall;
        private Label start_label;
        private System.Windows.Forms.Timer obstacleTimer;
        private PauseScreen pauseScreen1;
        private Label Score;
        private FlowLayoutPanel healthBar;
        private GameOver gameOver1;
        private Panel top_Boundary;
        private Panel btm_boundary;
        private System.Windows.Forms.Timer ShieldTimer;
        private Label highScore;
    }
}
