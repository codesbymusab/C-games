using Flappy_Copter.assets.bitmaps;
using Flappy_Copter.Audio;
using Flappy_Copter.models;
using Flappy_Copter.Models;
using Flappy_Copter.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Flappy_Copter.MenuScreen;

namespace Flappy_Copter
{
    public partial class PlayingScreen : UserControl
    {
        private SoundManager _soundManager;
        private const string ScoresFilePath = "scores.txt";

        public DifficulityMode difficulity = DifficulityMode.EASY;

        private Player _player;
        bool _playing = false;
        bool _activateShield = false;
        bool _musicOn = true;
        bool _soundOn = true;
        
        private Random _rand = new Random();
        private Point healthBarInit;
        private int _lives = 0;
        private int _obstaclesLimit = 0;

        // single obstacle instance
        private List<Obstacle> _obstacles=new List<Obstacle>();
        private int _highScore = 100;
        private int _highEasy = 0;
        private int _highMedium = 0;
        private int _highHard = 0;
        private bool _highScoreShown = false;
        private int _score = 0;

        private int SPAWN_OFFSET = 100;
        private int OBSTACLE_SPEED = 2400; // pixels/sec

        // shared obstacle sprite + pixel buffer
        private Bitmap _sharedObstacleBitmap;
      
        // obstacle spawn vertical range
        private readonly int SPAWN_MIN_Y = 40;
        private int SPAWN_MAX_Y => Math.Max(SPAWN_MIN_Y, this.Height - 40 - ObstacleBitMap.OBSTACLE_H);

        // timing
        private readonly Stopwatch _watch = Stopwatch.StartNew();
        private double _lastTime;

        public PlayingScreen()
        {


            DoubleBuffered = true;
            InitializeComponent();

            _player = new Player();
            this.Controls.Add(_player);
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
            this.Score.Text = "Score: " + _score.ToString();
            

            gameTimer.Tick -= gameTimer_Tick;
            gameTimer.Tick += gameTimer_Tick;
            this.ShieldTimer.Tick -= sheildTimer_Tick;
            this.ShieldTimer.Tick += sheildTimer_Tick;

            obstacleTimer.Interval = 1400;
            obstacleTimer.Tick -= obstacleTimer_Tick;
            obstacleTimer.Tick += obstacleTimer_Tick;

            this.pauseScreen1.KeyDown += PauseScreen_KeyDown;
            this.pauseScreen1.btn_resume.Click += PauseScreen_ResumeClick;
            this.pauseScreen1.btn_sound.Click += PauseScreen_SoundClick;
            this.pauseScreen1.btn_music.Click += PauseScreen_MusicClick;
            this.gameOver1.btn_tryAgain.Click += GameOverScreen_TryAgainClick;
            this.gameOver1.btn_menu.Click += GameOverScreen_MenuClick;

            this.Load += PlayingScreen_Load;
            this.Resize += PlayingScreen_Resize;

            // create walls in Wall helper
            Wall.CreateAndCacheBottomWall(this.Width);
            Wall.CreateAndCacheTopWall(this.Width);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            healthBarInit = healthBar.Location;


          

            _soundManager = new SoundManager();


        }

       

        private void SaveScores()
        {
            
            var lines = new[]
            {
        $"EASY/{(difficulity == DifficulityMode.EASY ? _score : _highScoreFor(DifficulityMode.EASY))}",
        $"MEDIUM/{(difficulity == DifficulityMode.MEDIUM ? _score : _highScoreFor(DifficulityMode.MEDIUM))}",
        $"HARD/{(difficulity == DifficulityMode.HARD ? _score : _highScoreFor(DifficulityMode.HARD))}"
    };
            File.WriteAllLines(ScoresFilePath, lines);
        }

     

        private int _highScoreFor(DifficulityMode mode)
        {
            return mode switch
            {
                DifficulityMode.EASY => _highEasy,
                DifficulityMode.MEDIUM => _highMedium,
                DifficulityMode.HARD => _highHard,
                _ => _highEasy
            };
        }

        private void SetHighScoreFor(DifficulityMode mode, int value)
        {
            switch (mode)
            {
                case DifficulityMode.EASY: _highEasy = value; break;
                case DifficulityMode.MEDIUM: _highMedium = value; break;
                case DifficulityMode.HARD: _highHard = value; break;
            }
        }

        private void LoadScores()
        {
         
            var lines = File.ReadAllLines(ScoresFilePath);
            foreach (var line in lines)
            {
                var parts = line.Split('/');
                var name = parts[0];
                var val = int.Parse(parts[1]);
                if (name == "EASY") _highEasy = val;
                else if (name == "MEDIUM") _highMedium = val;
                else if (name == "HARD") _highHard = val;
            }
          
            _highScore = _highScoreFor(difficulity);
        }

        public void setDifficulity(DifficulityMode difficulity)
        {
            switch (difficulity) {

                case DifficulityMode.EASY:
                    SPAWN_OFFSET = 100;
                    OBSTACLE_SPEED = 1200;
                    _lives = 4;
                    _obstaclesLimit = 1;
                    
                    break;
                case DifficulityMode.MEDIUM:
                    SPAWN_OFFSET = 200;
                    OBSTACLE_SPEED = 1800;
                    _lives = 3;
                    _obstaclesLimit = 2;
                    
                    break;
                case DifficulityMode.HARD:
                    SPAWN_OFFSET = 250;
                    OBSTACLE_SPEED = 2400;
                    _obstaclesLimit = 3;
                    _lives = 1;
                    break;
            }

        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }

        private void PlayingScreen_Load(object sender, EventArgs e)
        {
            if (FindForm() != null) FindForm().KeyPreview = true;
            this.Focus();
            if (File.Exists(ScoresFilePath))
                LoadScores();
            else
                SaveScores(); 

            _highScore = _highScoreFor(difficulity);
        }

        private void PlayingScreen_Resize(object sender, EventArgs e)
        {
            Wall.CreateAndCacheBottomWall(this.Width);
            Wall.CreateAndCacheTopWall(this.Width);
            Invalidate();
        }


        private void StartGame()
        {
            ResetObstacles();
            _score = 0;
            this.Score.Text = "Score: " + _score;
            this.Score.Visible = true;
            _soundManager.PlayBackground();
            _player.VelocityY = 0;
            _playing = true;
            start_label.Visible = false;
            _lastTime = _watch.Elapsed.TotalSeconds;
            healthBar.Location = healthBarInit;
            this.setDifficulity(difficulity);
            for (int i = 0; i < _lives; i++)
            {
                this.healthBar.Width += 50;
                this.healthBar.Location =new Point(this.healthBar.Location.X - 50, this.healthBar.Location.Y);
                this.healthBar.Controls.Add(new Heart());
            }

            gameTimer.Start();
            obstacleTimer.Start();
            this.Focus();
        }

        private async void DeactivateShieldAfterAsync(int milliseconds)
        {
            this.ShieldTimer.Start();
            await Task.Delay(milliseconds);
            if (this.IsHandleCreated)
            {
                this.Invoke((Action)(() =>
                {
                    _activateShield = false;
                    _player.ActivateShield(_activateShield);

                }));
            }
            this.ShieldTimer.Stop();
            this._player.Visible = true;
            this.Focus();
        }

        private async void hideHighScoreAfterAsync(int milliseconds)
        {
            _soundManager.PlayHighScore();
            this.highScore.Visible = true;
            _highScoreShown = true;

            await Task.Delay(milliseconds);
            
            this.highScore.Visible = false;
            this.Focus();
        }

        private void StopGame()
        {   
            if (_soundOn)
                _soundManager.PlayCollision();

            if (_lives < 0)
            {
                _soundManager.StopBackground(fadeOut:true,fadeMs:1000);
                _soundManager.PlayGameOver();
                _playing = false;
                _player.ActivateShield(false);
             
                _player.Location = _player._initLocation;
                gameTimer.Stop();
                obstacleTimer.Stop();
                this.gameOver1.Score.Text = "Score: "+_score.ToString();
                
                if (_score > _highScore)
                {
                    SetHighScoreFor(difficulity, _score);
                    _highScore = _score;
                }

                SaveScores();
                this.Score.Visible = false;
                this.gameOver1.Visible = true;
                this.gameOver1.Focus();
                
                
          
                return;
            }
            else if (!_activateShield)
            {
                if (_lives > 0)
                {
                    healthBar.Controls.RemoveAt(_lives - 1);
                    _activateShield = true;
                    _soundManager.PlayShield();
                }
                _lives--;

                _player.ActivateShield(_activateShield);
                DeactivateShieldAfterAsync(6000);
                
                this.Focus();
            }
        }

        private int GetRandomTopForSingleObstacle()
        {
            int max = SPAWN_MAX_Y;
            if (max <= SPAWN_MIN_Y) return SPAWN_MIN_Y;
            return _rand.Next(SPAWN_MIN_Y, max + 1);
        }

        private void PlayingScreen_Click(object sender, EventArgs e)
        {
            if (!this.Visible && !this._playing) return;


            if (!_playing) StartGame();
            else _player.VelocityY = _player.FlapStrength;


        }
        private void PlayingScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.Visible && !this._playing) return;

            if (e.KeyCode == Keys.Space)
            {
                if (!_playing) StartGame();
                else
                {
                    if (!_player.Bounds.IntersectsWith(this.top_Boundary.Bounds))
                    {
                        _player.VelocityY = _player.FlapStrength;
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (pauseScreen1.Visible == true)
                {
                    _lastTime = _watch.Elapsed.TotalSeconds;
                    this.pauseScreen1.Visible = false;
                    this.gameTimer.Enabled = true;
                    this.obstacleTimer.Enabled = true;
                    this.ActiveControl = null;
                }
                else
                {
                    _lastTime = _watch.Elapsed.TotalSeconds;
                    this.gameTimer.Enabled = false;
                    this.obstacleTimer.Enabled = false;
                    this.pauseScreen1.Visible = true;
                }
            }
        }

        private void PauseScreen_KeyDown(object sender, KeyEventArgs e)
        {
            this.gameTimer.Enabled = true;
            this.obstacleTimer.Enabled = true;
            this.pauseScreen1.Visible = false;
        }

        private void PauseScreen_ResumeClick(object sender, EventArgs e)
        {
            _lastTime = _watch.Elapsed.TotalSeconds;
            this.pauseScreen1.Visible = false;
            this.gameTimer.Enabled = true;
            this.obstacleTimer.Enabled = true;
            this.ActiveControl = null;
        }

        private void PauseScreen_MusicClick(object sender, EventArgs e)
        {
            _soundManager.PlayButton();
            if (_musicOn)
            {
             
                _soundManager.StopBackground();
                _musicOn = false;
                this.pauseScreen1.btn_music.Text = "TURN MUSIC ON";
            }
            else
            {

                _soundManager.PlayBackground();
                _musicOn = true;
                this.pauseScreen1.btn_music.Text = "TURN MUSIC OFF";

            }
        }

        private void PauseScreen_SoundClick(object sender, EventArgs e)

        {
            _soundManager.PlayButton();
            if (_soundOn)
            {   
              
                _soundOn = false;
                this.pauseScreen1.btn_sound.Text = "TURN SOUND ON";
            }
            else
            {

                _soundOn = true;
                this.pauseScreen1.btn_sound.Text = "TURN SOUND OFF";

            }
        }

        private void GameOverScreen_TryAgainClick(object sender, EventArgs e)
        {
            _soundManager.PlayButton();
            this.gameOver1.Visible = false;
            this.start_label.Visible = true;
            StartGame();
        }

        private void GameOverScreen_MenuClick(object sender, EventArgs e)
        {
            _soundManager.PlayButton();
            this._soundManager.StopBackground();
            this.gameOver1.Visible = false;
            this.Visible = false;
            this._soundManager.PlayMenu();
            


        }
       
        private void setBackgorund()
        {

            if (_score >= 200)
            {
                this.BackgroundImage = Resources.playing_background;
                Wall.fillColor = Color.FromArgb(120, 60, 30);
                this.Score.ForeColor = Color.Gold;
                this.highScore.ForeColor = Color.Gold;
                Wall.CreateAndCacheBottomWall(this.Width);
                Wall.CreateAndCacheTopWall(this.Width);
            }
            else if (_score >= 150)
            {
                this.BackgroundImage = Resources.autumnBackground;
                Wall.fillColor = Color.Wheat;
                this.Score.ForeColor = Color.Black;
                this.highScore.ForeColor = Color.Black;
                Wall.CreateAndCacheBottomWall(this.Width);
                Wall.CreateAndCacheTopWall(this.Width);
            }
            else if( _score >= 100)
            {
                this.BackgroundImage = Resources.winterBackground;
                Wall.fillColor = Color.LightSkyBlue;
                this.Score.ForeColor = Color.DarkBlue;
                this.highScore.ForeColor = Color.DarkBlue;
                Wall.CreateAndCacheBottomWall(this.Width);
                Wall.CreateAndCacheTopWall(this.Width);
            }
            else if (_score >= 50)
            {
                this.BackgroundImage = Resources.springBackground;
                 Wall.fillColor = Color.LimeGreen;
                this.Score.ForeColor = Color.White;
                this.highScore.ForeColor = Color.White;
                Wall.CreateAndCacheBottomWall(this.Width);
                Wall.CreateAndCacheTopWall(this.Width);
            }
           
        }   
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (!_playing) return;
            
            setBackgorund();
            
            double now = _watch.Elapsed.TotalSeconds;
            float dt = (float)(now - _lastTime);
            _lastTime = now;
            
            for (int i = _obstacles.Count - 1; i >= 0; i--)
            {
                var obs = _obstacles[i];

                obs.Update(dt);

                if (obs.Right < 0)
                {
                   
                        if (this.Controls.Contains(obs))
                        {
                            this.Controls.Remove(obs);
                        }

                    
                    obs.Dispose();

                    


                   

                    _obstacles.RemoveAt(i);
                  
                }
            }


            // scroll walls 
            Wall._bottomWallOffset -= Wall._bottomWallSpeed * dt;
            Wall._topWallOffset -= Wall._topWallSpeed * dt;

            // regenerate when scrolled full width
            if (Wall.BottomWallBitmap != null && Wall._bottomWallOffset <= -Wall._bottomWallW)
            {
                Wall._bottomWallOffset = 0f;
                Wall.CreateAndCacheBottomWall(this.Width);
            }
            if (Wall.TopWallBitmap != null && Wall._topWallOffset <= -Wall._topWallW)
            {
                Wall._topWallOffset = 0f;
                Wall.CreateAndCacheTopWall(this.Width);
            }

            if (_player.Bounds.IntersectsWith(this.btm_boundary.Bounds))
            {
               _player.VelocityY = 0f;
                _player.Top = this.btm_boundary.Top - _player.Height;
            }
            else
            {
                _player.UpdatePosition();
            }


            if (!_activateShield)
            {
                CheckWallCollison();
                CheckObstacleCollison();
            }

            if (_obstacles.Count>0 && _player != null)
            {

                if (_score > _highScore && !_highScoreShown)
                {
                    hideHighScoreAfterAsync(4000);
                }

                foreach (Obstacle obs in _obstacles) {
                    if (obs.Location.X < _player.Location.X)
                    {
                        if (_soundOn)
                            _soundManager.PlayScoreUp();
                        _score += 10;
                        this.Score.Text = "Score: " + _score.ToString();
                        break;

                    }
                }
            }

            Invalidate();
        }

        private void obstacleTimer_Tick(object sender, EventArgs e)
        {
            if (_obstacles.Count == _obstaclesLimit) return;
            ObstacleBitMap obsBitMap=new ObstacleBitMap();
            obsBitMap.PrepareSharedObstacle();
            _sharedObstacleBitmap = obsBitMap._sharedObstacleBitmap;

            int spawnX = this.Width + SPAWN_OFFSET;
            int top = GetRandomTopForSingleObstacle();

            Obstacle obstacle = new Obstacle(_sharedObstacleBitmap, new Point(spawnX, top))
            {
                Size = new Size(ObstacleBitMap.OBSTACLE_W, ObstacleBitMap.OBSTACLE_H),
                SizeMode = PictureBoxSizeMode.Normal,
                TabStop = false
            };

            obstacle.X = spawnX;
            obstacle.Speed = ObstacleBitMap.OBSTACLE_SPEED;
            obstacle.Tag = (obsBitMap._sharedObstaclePixels, obsBitMap._sharedObstacleW, obsBitMap._sharedObstacleH, obsBitMap._sharedObstacleStride);

            this._obstacles.Add(obstacle);
            this.Controls.Add(obstacle);
            if (FindForm() != null) FindForm().KeyPreview = true;
            this.Focus();
        }

        private void sheildTimer_Tick(object sender, EventArgs e)
        {
            if (this._player.Visible)
            {
                this._player.Visible = false;
            }
            else
            {
                this._player.Visible = true;
            }
        
        }
        private void ResetObstacles()
        {
                
                 foreach(var control in this.Controls)
            {
              
                if(control is Obstacle)
                {
                    Obstacle obs = control as Obstacle;
                    obs.Visible = false;   
                }
            }
                _obstacles.Clear();
            
           
        }

       
        private void CheckWallCollison()
        {
            // bottom wall world bounds 

            var bottomWorld = new Rectangle(0, this.ClientSize.Height - Wall._bottomWallH, this.ClientSize.Width, Wall._bottomWallH);
            if (_player.Bounds.IntersectsWith(bottomWorld))
            {
                if (PlayerIntersectsTiles(_player, bottomWorld, Wall._bottomWallPixels, Wall._bottomWallW, Wall._bottomWallH, Wall._bottomWallStride, Wall._bottomWallOffset))
                {

                    StopGame();
                    return;
                }
            }

            // top wall world bounds

            var topWorld = new Rectangle(0, 0, this.ClientSize.Width, Wall._topWallH);
            if (_player.Bounds.IntersectsWith(topWorld))
            {
                if (PlayerIntersectsTiles(_player, topWorld, Wall._topWallPixels, Wall._topWallW, Wall._topWallH, Wall._topWallStride, Wall._topWallOffset))
                {
                    StopGame();
                    return;
                }
            }
            this.Focus();
        }

        private void CheckObstacleCollison()
        {   
            if (_obstacles.Count == 0) return;
            foreach (var obstacle in _obstacles)
            {
                if (_player.Bounds.IntersectsWith(obstacle.Bounds))
                {
                    StopGame();
                }
            }
            this.Focus();
        }

        
        private bool PlayerIntersectsTiles(Control player, Rectangle tileWorldBounds, byte[] pixels, int maskW, int maskH, int stride, float tileOffset)
        {
            if (pixels == null) return false;

            Rectangle overlap = Rectangle.Intersect(player.Bounds, tileWorldBounds);
            if (overlap.IsEmpty) return false;

            float sx = (float)maskW / (float)tileWorldBounds.Width;
            float sy = (float)maskH / (float)tileWorldBounds.Height;

            int startX_world = overlap.Left - tileWorldBounds.Left;
            int startY_world = overlap.Top - tileWorldBounds.Top;

            int startX = (int)Math.Floor((startX_world - tileOffset) * sx) % maskW;
            if (startX < 0) startX += maskW;
            int startY = (int)Math.Floor(startY_world * sy);

            int width = Math.Min(maskW - startX, (int)Math.Ceiling(overlap.Width * sx));
            int height = Math.Min(maskH - startY, (int)Math.Ceiling(overlap.Height * sy));
            if (width <= 0 || height <= 0) return false;

            for (int y = 0; y < height; y++)
            {
                int rowBase = (startY + y) * stride + startX * 4;
                for (int x = 0; x < width; x++)
                {
                    if (pixels[rowBase + x * 4 + 3] != 0) return true;
                }
            }

            return false;
        }

       

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            _player.setSmokeSprite();
            e.Graphics.DrawImage(_player.currentSmokeSprite, _player.Location.X - 100, _player.Location.Y-20, 100, 100);


            // draw top wall 
            if (Wall.TopWallBitmap != null)
            {

                int y = 0;
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                int x0 = (int)Math.Round(Wall._topWallOffset);
                e.Graphics.DrawImage(Wall.TopWallBitmap, x0, y, Wall._topWallW, Wall._topWallH);
                e.Graphics.DrawImage(Wall.TopWallBitmap, x0 + Wall._topWallW, y, Wall._topWallW, Wall._topWallH);
            }

            // draw bottom wall
            if (Wall.BottomWallBitmap != null)
            {
                int y = this.ClientSize.Height - Wall._bottomWallH;
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                int x0 = (int)Math.Round(Wall._bottomWallOffset);
                e.Graphics.DrawImage(Wall.BottomWallBitmap, x0, y, Wall._bottomWallW, Wall._bottomWallH);
                e.Graphics.DrawImage(Wall.BottomWallBitmap, x0 + Wall._bottomWallW, y, Wall._bottomWallW, Wall._bottomWallH);
            }


        }

        private void PlayingScreen_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                this.ActiveControl = null;
            }
        }

        private void pauseScreen1_Load(object sender, EventArgs e)
        {

        }
    }
}
