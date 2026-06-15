using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Copter
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {

            InitializeComponent();
            DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.Shown += SplashScreen_Shown;
            this.logoTimer.Start();
        }

        // synchronous: immediately enables double buffering

        // async event handler — await the work so exceptions surface and flow is predictable
        private async void SplashScreen_Shown(object sender, EventArgs e)
        {
            try
            {
                await ShowMenuAsync(); // await the task that does loading + delay + swap
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Splash error: " + ex);
            }
        }

        // async Task that performs visible changes and then does the real swap or loading
        private async Task ShowMenuAsync()

        {
            // keep splash visible for a short time
            await Task.Delay(2000);

            var path = Path.Combine(Application.StartupPath, "assets", "backgrounds", "menu_background.png");
            var img = Image.FromFile(path);
            this.BackColor = Color.White;
            this.Activate();


            //this.panelContainer.BackgroundImage = img;
            //this.panelContainer.BackgroundImageLayout = ImageLayout.Stretch;


            this.menuScreen1.Visible = true;
            this.ActiveControl = null;
        }

        // fields in your form/control
        private bool _logoGrowing = false;
        private readonly Size _logoMin = new Size(225, 128);   // adjust
        private readonly Size _logoMax = new Size(275 ,160);  // adjust
        private readonly int _logoStep = 4;                  // pixels per tick

        private void logoTimer_Tick(object sender, EventArgs e)
        {
            PictureBox Logo = this.menuScreen1.hpBox;
            if (Logo == null) return;

            // choose direction
            if (_logoGrowing)
            {
                Logo.Width = Math.Min(_logoMax.Width, Logo.Width + _logoStep);
                Logo.Height = Math.Min(_logoMax.Height, Logo.Height + _logoStep);
            }
            else
            {
                Logo.Width = Math.Max(_logoMin.Width, Logo.Width - _logoStep);
                Logo.Height = Math.Max(_logoMin.Height, Logo.Height - _logoStep);
            }

            // flip direction when we hit min or max
            if (Logo.Width >= _logoMax.Width && Logo.Height >= _logoMax.Height) _logoGrowing = false;
            if (Logo.Width <= _logoMin.Width && Logo.Height <= _logoMin.Height) _logoGrowing = true;

            // keep logo centered in its parent
            var parent = Logo.Parent;
            if (parent != null)
            {
                Logo.Location = new Point(
                    Math.Max(0, (parent.ClientSize.Width - Logo.Width) / 2),
                    Math.Max(0, (parent.ClientSize.Height - Logo.Height) / 2)
                );
            }
        }

    }
}
