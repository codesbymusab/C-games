using Flappy_Copter.Properties;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;



namespace Flappy_Copter.Models
{
    [ToolboxItem(true)]
    public class Player : PictureBox
    {   
        public Point _initLocation=new Point(120, 250);
        public float VelocityY { get; set; } = 0f;
        public float Gravity { get; set; } = 4.5f;
        public float FlapStrength { get; set; } = -24f;

        private int _frameIndex = 0;

        private List<Image> smokeSprites = new List<Image>
{
        Properties.Resources.smoke1,
        Properties.Resources.smoke2,
        Properties.Resources.smoke3,
    
   
        };

        public Image currentSmokeSprite;

        public void setSmokeSprite()
        {
            currentSmokeSprite = smokeSprites[_frameIndex];
            _frameIndex = (_frameIndex + 1) % smokeSprites.Count;
     
        }

        public void UpdatePosition()
        {
            VelocityY += Gravity;
            if (VelocityY > 12f) VelocityY = 12f; 
            this.Top += (int)VelocityY;
        }

        public Player()
        {
            this.DoubleBuffered = true;
            this.Anchor = AnchorStyles.None;
            this.BackColor = Color.Transparent;
            this.BackgroundImage = Properties.Resources.helicopter1;
            this.BackgroundImageLayout = ImageLayout.Zoom;
            this.Location = _initLocation;
            this.Name = "player";
            this.Size = new Size(89, 67);
            this.TabIndex = 0;
            this.TabStop = false;
           
        }

        public void ActivateShield(bool activate)
        {
            if (activate) {

                this.Size = new Size(167, 135);
                this.BackgroundImage = Resources.shielded1;
            }
            else
            {
                this.Size = new Size(89, 67);
                this.BackgroundImage = Resources.helicopter1;
            }
        }
       
        
        public void Display(Graphics g)
        {
            g.FillEllipse(new SolidBrush(this.BackColor), new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height));
        }
    }

}
