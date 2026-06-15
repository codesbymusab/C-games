namespace Flappy_Copter
{
    partial class SplashScreen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            Logo = new PictureBox();
            menuScreen1 = new MenuScreen();
            logoTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            SuspendLayout();
            // 
            // Logo
            // 
            Logo.Anchor = AnchorStyles.None;
            Logo.BackgroundImage = Properties.Resources.logo;
            Logo.BackgroundImageLayout = ImageLayout.Zoom;
            Logo.Location = new Point(154, 80);
            Logo.Name = "Logo";
            Logo.Size = new Size(733, 410);
            Logo.TabIndex = 20;
            Logo.TabStop = false;
            // 
            // menuScreen1
            // 
            menuScreen1.BackgroundImage = (Image)resources.GetObject("menuScreen1.BackgroundImage");
            menuScreen1.BackgroundImageLayout = ImageLayout.Stretch;
            menuScreen1.Dock = DockStyle.Fill;
            menuScreen1.Location = new Point(0, 0);
            menuScreen1.Name = "menuScreen1";
            menuScreen1.Size = new Size(1040, 571);
            menuScreen1.TabIndex = 21;
            menuScreen1.Visible = false;
            // 
            // logoTimer
            // 
            logoTimer.Interval = 75;
            logoTimer.Tick += logoTimer_Tick;
            // 
            // SplashScreen
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Cyan;
            ClientSize = new Size(1040, 571);
            Controls.Add(menuScreen1);
            Controls.Add(Logo);
            DoubleBuffered = true;
            KeyPreview = true;
            Name = "SplashScreen";
            Text = "Flappy Copter";
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox Logo;
        private MenuScreen menuScreen1;
        private System.Windows.Forms.Timer logoTimer;
    }
}
