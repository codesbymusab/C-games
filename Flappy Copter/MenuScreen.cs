using Flappy_Copter.Audio;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Copter
{
    public partial class MenuScreen : UserControl
    {
        public enum DifficulityMode { EASY, MEDIUM, HARD }
        private SoundManager _soundManager;
        public MenuScreen()
        {
            InitializeComponent();

            _soundManager = new SoundManager();
            _soundManager.PlayMenu();

            DoubleBuffered = true;
            this.btn_play.TabStop = false;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.btn_profile.Click += btn_Difficulity_Click;
            this.btn_leaderboard.Click += this.btn_HighScores_Click;
           
            this.difficulityScreen1.btn_Easy.Click += btn_DifficulityMode_Click;
            this.difficulityScreen1.btn_Medium.Click += btn_DifficulityMode_Click;
            this.difficulityScreen1.btn_Hard.Click += btn_DifficulityMode_Click;
        }

        private void btn_play_Click(object sender, EventArgs e)
        {

            _soundManager.StopMenu();
            _soundManager.PlayButton();
            this.playingScreen1.Visible = true;

            this.playingScreen1.Focus();
            this.ActiveControl = playingScreen1;


        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void btn_Difficulity_Click(object sender, EventArgs e)
        {   
            if (!this.playingScreen1.Visible)
            {
                _soundManager.PlayButton();
                this.difficulityScreen1.Visible = true;
            }
        }
        private void btn_DifficulityMode_Click(object sender, EventArgs e)
        {
            if (sender == null) return;

            if (!this.playingScreen1.Visible)
            {
                this.difficulityScreen1.Focus();
                _soundManager.PlayButton();
                this.difficulityScreen1.Visible = true;

                Button difficulityBtn = sender as Button;
                switch (difficulityBtn.Text)
                {
                    case "EASY":
                        this.playingScreen1.difficulity = DifficulityMode.EASY;
                        break;
                    case "MEDIUM":
                        this.playingScreen1.difficulity = DifficulityMode.MEDIUM;
                        break;
                    case "HARD":
                        this.playingScreen1.difficulity = DifficulityMode.HARD;
                        break;
                }
                this.ActiveControl = button1;
                this.difficulityScreen1.Visible = false;
            }
        }

        private void btn_HighScores_Click(object sender, EventArgs e)
        {
            if (!this.playingScreen1.Visible)
            {
                _soundManager.PlayButton();
                this.highScoresScreen1.Visible = true;
            }
        }
        private void playingScreen1_Load(object sender, EventArgs e)
        {

        }

        private void playingScreen1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
