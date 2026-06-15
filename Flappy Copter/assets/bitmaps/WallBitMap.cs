using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Flappy_Copter.assets.bitmaps
{
    internal static class Wall
    {
        private static Random _rand = new Random();

        // bottom wall
        private static int _bottomWallHeight = 100;
        private static Bitmap _bottomWallBitmap;
        public static byte[] _bottomWallPixels;
        public static int _bottomWallStride, _bottomWallW, _bottomWallH;
        public static float _bottomWallOffset = 0f;
        public static float _bottomWallSpeed = 1200f;
        public static Color fillColor=Color.FromArgb(120, 60, 30); 
        // top wall (same logic as bottom but flipped vertically)
        private static int _topWallHeight = 100;
        private static Bitmap _topWallBitmap;
        public static byte[] _topWallPixels;
        public static int _topWallStride, _topWallW, _topWallH;
        public static float _topWallOffset = 0f;
        public static float _topWallSpeed = 600f;

        
        public static Bitmap BottomWallBitmap => _bottomWallBitmap;
        public static Bitmap TopWallBitmap => _topWallBitmap;

        // build BGRA buffer from Bitmap
        private static void CreatePixelBufferFromBitmap(Bitmap bmp, out byte[] buffer, out int width, out int height, out int stride)
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

        public static Bitmap CreateAndCacheBottomWall(int width)
        {
            _bottomWallBitmap?.Dispose();
            _bottomWallBitmap = CreateMountainBottomWall(
                sourceWidth: Math.Max(64, width / 4),
                sourceHeight: _bottomWallHeight,
                targetWidth: width,
                targetHeight: _bottomWallHeight,
                greenFill: Color.Transparent,
     
                soilFill: fillColor,
                minMountainHeight: (int)(_bottomWallHeight * 0.5f),
                maxMountainHeight: _bottomWallHeight - 2,
                octaves: 5,
                roughness: 0.5f,
                seed: Environment.TickCount ^ _rand.Next()
            );

            CreatePixelBufferFromBitmap(_bottomWallBitmap, out _bottomWallPixels, out _bottomWallW, out _bottomWallH, out _bottomWallStride);
            _bottomWallOffset = 0f;
            return _bottomWallBitmap;
        }

        public static Bitmap CreateAndCacheTopWall(int width)
        {
            _topWallBitmap?.Dispose();
            var src = CreateMountainBottomWall(
                sourceWidth: Math.Max(64, width / 4),
                sourceHeight: _topWallHeight,
                targetWidth: width,
                targetHeight: _topWallHeight,
                greenFill: Color.Transparent,
                soilFill: fillColor,
                minMountainHeight: (int)(_topWallHeight * 0.5f),
                maxMountainHeight: _topWallHeight - 2,
                octaves: 5,
                roughness: 0.5f,
                seed: Environment.TickCount ^ _rand.Next()
            );

            src.RotateFlip(RotateFlipType.Rotate180FlipNone);
            _topWallBitmap = new Bitmap(src);
            src.Dispose();

            CreatePixelBufferFromBitmap(_topWallBitmap, out _topWallPixels, out _topWallW, out _topWallH, out _topWallStride);
            _topWallOffset = 0f;
            return _topWallBitmap;
        }

        // mountain generator (transparent above, soil opaque below)
        public static Bitmap CreateMountainBottomWall(int sourceWidth, int sourceHeight, int targetWidth, int targetHeight,
            Color greenFill, Color soilFill, int minMountainHeight = 12, int maxMountainHeight = 48,
            int octaves = 4, float roughness = 0.5f, int seed = 0)
        {
            var rand = new Random(seed == 0 ? Environment.TickCount : seed);
            float[] heights = new float[sourceWidth];
            float amplitude = 1f;
            float frequency = 1f;

            for (int o = 0; o < octaves; o++)
            {
                float phase = (float)(rand.NextDouble() * Math.PI * 2.0);
                float scale = (float)(rand.NextDouble() * 0.6 + 0.7);
                for (int x = 0; x < sourceWidth; x++)
                {
                    float nx = (float)x / sourceWidth * frequency * (1f + 0.2f * o);
                    float v = (float)Math.Sin(nx * Math.PI * 2.0 + phase) * scale;
                    heights[x] += v * amplitude;
                }
                amplitude *= roughness;
                frequency *= 2f;
            }

            float minH = float.MaxValue, maxH = float.MinValue;
            for (int i = 0; i < sourceWidth; i++)
            {
                if (heights[i] < minH) minH = heights[i];
                if (heights[i] > maxH) maxH = heights[i];
            }
            float range = Math.Max(1e-6f, maxH - minH);

            int[] mountainTop = new int[sourceWidth];
            for (int x = 0; x < sourceWidth; x++)
            {
                float norm = (heights[x] - minH) / range;
                float scaled = minMountainHeight + norm * (maxMountainHeight - minMountainHeight);
                mountainTop[x] = sourceHeight - (int)Math.Round(scaled);
            }

            // small smoothing
            for (int pass = 0; pass < 2; pass++)
            {
                int[] tmp = new int[sourceWidth];
                for (int x = 0; x < sourceWidth; x++)
                {
                    int left = Math.Max(0, x - 1);
                    int right = Math.Min(sourceWidth - 1, x + 1);
                    tmp[x] = (mountainTop[left] + mountainTop[x] + mountainTop[right]) / 3;
                }
                Array.Copy(tmp, mountainTop, sourceWidth);
            }

            var srcBmp = new Bitmap(sourceWidth, sourceHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(srcBmp))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                g.Clear(greenFill);
                using (var soilBrush = new SolidBrush(soilFill))
                {
                    for (int x = 0; x < sourceWidth; x++)
                    {
                        int topY = mountainTop[x];
                        g.FillRectangle(soilBrush, x, topY, 1, sourceHeight - topY);
                    }
                }
            }

            if (targetWidth == sourceWidth && targetHeight == sourceHeight) return srcBmp;

            var resized = new Bitmap(targetWidth, targetHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                g.DrawImage(srcBmp, 0, 0, targetWidth, targetHeight);
            }

            srcBmp.Dispose();
            return resized;
        }
    }
}
