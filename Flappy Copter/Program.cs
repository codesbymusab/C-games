using System.Collections.Concurrent;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;

namespace Flappy_Copter
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new SplashScreen());

        }


    }


static class ResourceLoader
    {
        private static readonly ConcurrentDictionary<string, Image> Cache = new();

        public static async Task<Image> LoadImageAsync(string path)
        {
            if (Cache.TryGetValue(path, out var cached) && cached != null) return cached;

            byte[] bytes;
            try
            {
                // read file bytes off the UI thread
                bytes = await Task.Run(() => File.ReadAllBytes(path)).ConfigureAwait(false);
            }
            catch
            {
                return null; // caller should handle null
            }

            // create Image on the calling (UI) thread to avoid cross-thread GDI handle issues
            try
            {
                using var ms = new MemoryStream(bytes);
                var img = Image.FromStream(ms); // runs on UI thread because caller awaited above
                Cache[path] = img;
                return img;
            }
            catch
            {
                return null;
            }
        }
    }



}