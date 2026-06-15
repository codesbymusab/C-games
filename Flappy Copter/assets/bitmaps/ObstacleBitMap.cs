using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Flappy_Copter.assets.bitmaps
{
    internal  class ObstacleBitMap
    {
        public const int OBSTACLE_W = 62;
        public const int OBSTACLE_H = 100;
        public const int OBSTACLE_SPEED = 1200; // pixels/sec
        public  Random _rand = new Random();

        // shared obstacle sprite + pixel buffer
        public  Bitmap _sharedObstacleBitmap;
        public  byte[] _sharedObstaclePixels;
        public  int _sharedObstacleW, _sharedObstacleH, _sharedObstacleStride;


        private  static void CreatePixelBufferFromBitmap(Bitmap bmp, out byte[] buffer, out int width, out int height, out int stride)
        {
            width = bmp.Width;
            height = bmp.Height;
            var rect = new Rectangle(0, 0, width, height);
            var data = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            stride = Math.Abs(data.Stride);
            buffer = new byte[stride * height];
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);
            bmp.UnlockBits(data);
        }
        public  void PrepareSharedObstacle()
        {
           

            _sharedObstacleBitmap?.Dispose();
            _sharedObstacleBitmap = new Bitmap(OBSTACLE_W, OBSTACLE_H, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (var g = Graphics.FromImage(_sharedObstacleBitmap))
            {
                g.Clear(Color.Transparent);

                int r = 40 + _rand.Next(0, 216);
                int gr = 40 + _rand.Next(0, 216);
                int b = 40 + _rand.Next(0, 216);
                Color fillColor = Color.FromArgb(r, gr, b);

                using (var brush = new SolidBrush(fillColor))
                {
                    g.FillRectangle(brush, 0, 0, OBSTACLE_W, OBSTACLE_H);
                }

                Color darker = ControlPaint.Dark(fillColor, 0.25f);
                using (var baseBrush = new SolidBrush(darker))
                {
                    int baseH = Math.Max(6, OBSTACLE_H / 6);
                    g.FillRectangle(baseBrush, 0, OBSTACLE_H - baseH, OBSTACLE_W, baseH);
                }
            }

            CreatePixelBufferFromBitmap(
                _sharedObstacleBitmap,
                out _sharedObstaclePixels,
                out _sharedObstacleW,
                out _sharedObstacleH,
                out _sharedObstacleStride
            );

          
          
        }

    }
}
