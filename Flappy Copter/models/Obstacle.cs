using Flappy_Copter.Audio;

public class Obstacle : PictureBox
{
   

    public float X;                      // sub-pixel X position
    public int Speed { get; set; } = 2048; // pixels per second

    public Obstacle(Image sprite, Point location)
    {
        this.Size = new Size(62, 62);
        this.Location = location;
        this.X = location.X;                 // initialize logical X
        this.Image = sprite;
        this.SizeMode = PictureBoxSizeMode.Normal; // avoid runtime scaling
        this.BackColor = Color.Transparent;
        this.TabStop = false;
        
    }

    // time-based update: dt in seconds
    public void Update(float dt)
    {
        X -= 306 * dt;
        this.Left = (int)Math.Round(X);
    }

    public bool IsOffScreen()
    {
        return this.Right < 0;
    }
}
