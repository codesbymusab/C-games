using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Flappy_Copter.Audio
{
    public class SoundManager : IDisposable
    {
        // Music playback (streaming + loop)
        private static string GetAssetPath(params string[] parts)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(new[] { baseDir }.Concat(parts).ToArray());
        }

        private IWavePlayer _musicOutput;
        private AudioFileReader _musicReader;
        private LoopStream _musicLoop;

        // Menu playback (separate track)
        private IWavePlayer _menuOutput;
        private AudioFileReader _menuReader;
        private LoopStream _menuLoop;

        // SFX mixer (low-latency mixing of many short sounds)
        private MixingSampleProvider _sfxMixer;
        private IWavePlayer _sfxOutput;

        // Cached short sounds
        private readonly Dictionary<string, CachedSound> _cached = new Dictionary<string, CachedSound>(StringComparer.OrdinalIgnoreCase);

        // volumes (0..1)
        private float _musicVolume = 0.35f;
        private float _sfxVolume = 1.0f;

        public SoundManager()
        {
            string musicPath = GetAssetPath("assets", "audio", "background.mp3");
            string scoreUpPath = GetAssetPath("assets", "audio", "scoreup.mp3");
            string collisionPath = GetAssetPath("assets", "audio", "colission.mp3");
            string menuPath = GetAssetPath("assets", "audio", "menu.mp3");
            string losePath = GetAssetPath("assets", "audio", "gameover.mp3");
            string buttonPath = GetAssetPath("assets", "audio", "button.mp3");
            string highScorePath = GetAssetPath("assets", "audio", "highscore.mp3");

            // Setup SFX mixer/output (44.1k or will adapt on first add)
            _sfxMixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2))
            {
                ReadFully = true
            };
            _sfxOutput = new WaveOutEvent();
            _sfxOutput.Init(_sfxMixer);

            // Preload common SFX if provided
            if (!string.IsNullOrEmpty(scoreUpPath)) PreloadSfx("score", scoreUpPath);
            if (!string.IsNullOrEmpty(collisionPath)) PreloadSfx("collision", collisionPath);
            if (!string.IsNullOrEmpty(highScorePath)) PreloadSfx("highscore", highScorePath);
            if (!string.IsNullOrEmpty(buttonPath)) PreloadSfx("button", buttonPath);
            if (!string.IsNullOrEmpty(losePath)) PreloadSfx("gameover", losePath);

            // Setup music if provided
            if (!string.IsNullOrEmpty(musicPath))
            {
                try
                {
                    _musicReader = new AudioFileReader(musicPath) { Volume = _musicVolume };
                    _musicLoop = new LoopStream(_musicReader);
                    _musicOutput = new WaveOutEvent();
                    _musicOutput.Init(_musicLoop);
                }
                catch
                {
                    DisposeMusic();
                }
            }

            // Setup menu track if provided (separate output so menu/music don't conflict)
            if (!string.IsNullOrEmpty(menuPath))
            {
                try
                {
                    _menuReader = new AudioFileReader(menuPath) { Volume = _musicVolume };
                    _menuLoop = new LoopStream(_menuReader);
                    _menuOutput = new WaveOutEvent();
                    _menuOutput.Init(_menuLoop);
                }
                catch
                {
                    DisposeMenu();
                }
            }

            // Start SFX output so mixer is ready to play immediately
            try { _sfxOutput.Play(); } catch { }
        }

        // ---------------- public API ----------------

        public void PlayBackground(bool loop = true)
        {
            if (_musicOutput == null || _musicReader == null) return;
            _musicLoop.EnableLooping = loop;
            _musicReader.Volume = _musicVolume;
            _musicOutput.Play();
        }

        public void StopBackground(bool fadeOut = false, int fadeMs = 300)
        {
            if (_musicOutput == null || _musicReader == null) return;
            if (fadeOut)
            {
                _ = FadeOutMusicAsync(_musicReader, _musicOutput, fadeMs);
            }
            else
            {
                try { _musicOutput.Stop(); } catch { }
            }
        }

        // Menu controls
        public void PlayMenu(bool loop = true)
        {
            if (_menuOutput == null || _menuReader == null) return;
            _menuLoop.EnableLooping = loop;
            _menuReader.Volume = _musicVolume;
            _menuOutput.Play();
        }

        public void StopMenu(bool fadeOut = false, int fadeMs = 300)
        {
            if (_menuOutput == null || _menuReader == null) return;
            if (fadeOut)
            {
                _ = FadeOutMusicAsync(_menuReader, _menuOutput, fadeMs);
            }
            else
            {
                try { _menuOutput.Stop(); } catch { }
            }
        }

        public void PlayScoreUp() => PlaySfxKey("score");
        public void PlayShield() => PlaySfxKey("shield");
        public void PlayCollision() => PlaySfxKey("collision");
        public void PlayHighScore() => PlaySfxKey("highscore");
        public void PlayButton() => PlaySfxKey("button");
        public void PlayGameOver() => PlaySfxKey("gameover");

        public void PreloadSfx(string key, string path)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(path)) return;
            try
            {
                if (!_cached.ContainsKey(key))
                {
                    var cs = new CachedSound(path);
                    _cached[key] = cs;
                }
            }
            catch
            {
                // ignore preload failures
            }
        }

        public void SetMusicVolume(float v)
        {
            _musicVolume = Math.Max(0f, Math.Min(1f, v));
            if (_musicReader != null) _musicReader.Volume = _musicVolume;
            if (_menuReader != null) _menuReader.Volume = _musicVolume;
        }

        public void SetSfxVolume(float v)
        {
            _sfxVolume = Math.Max(0f, Math.Min(1f, v));
        }

        // ---------------- internal helpers ----------------

        private void PlaySfxKey(string key)
        {
            if (string.IsNullOrEmpty(key)) return;
            if (!_cached.TryGetValue(key, out var cs)) return;
            PlayCachedSound(cs);
        }

        private void PlayCachedSound(CachedSound cs)
        {
            if (cs == null) return;
            try
            {
                var provider = new CachedSoundSampleProvider(cs)
                {
                    Volume = _sfxVolume
                };

                if (!_sfxMixer.WaveFormat.Equals(provider.WaveFormat))
                {
                    var newMixer = new MixingSampleProvider(provider.WaveFormat) { ReadFully = true };
                    _sfxMixer = newMixer;
                    try
                    {
                        _sfxOutput?.Stop();
                        _sfxOutput?.Dispose();
                    }
                    catch { }
                    _sfxOutput = new WaveOutEvent();
                    _sfxOutput.Init(_sfxMixer);
                    _sfxOutput.Play();
                }

                _sfxMixer.AddMixerInput(provider);
            }
            catch
            {
                // swallow play errors
            }
        }

        private async Task FadeOutMusicAsync(AudioFileReader reader, IWavePlayer output, int ms)
        {
            if (reader == null || output == null) { try { output?.Stop(); } catch { } return; }
            int steps = 20;
            int delay = Math.Max(1, ms / steps);
            float start = reader.Volume;
            for (int i = 0; i < steps; i++)
            {
                float t = 1f - (i + 1f) / (float)steps;
                reader.Volume = start * t;
                await Task.Delay(delay).ConfigureAwait(false);
            }
            try { output.Stop(); } catch { }
            reader.Volume = start;
        }

        // ---------------- dispose ----------------

        public void Dispose()
        {
            DisposeMusic();
            DisposeMenu();
            try
            {
                _sfxOutput?.Stop();
                _sfxOutput?.Dispose();
            }
            catch { }
            _sfxOutput = null;
            _sfxMixer = null;
            _cached.Clear();
        }

        private void DisposeMusic()
        {
            try { _musicOutput?.Stop(); } catch { }
            try { _musicOutput?.Dispose(); } catch { }
            try { _musicLoop?.Dispose(); } catch { }
            try { _musicReader?.Dispose(); } catch { }
            _musicOutput = null;
            _musicLoop = null;
            _musicReader = null;
        }

        private void DisposeMenu()
        {
            try { _menuOutput?.Stop(); } catch { }
            try { _menuOutput?.Dispose(); } catch { }
            try { _menuLoop?.Dispose(); } catch { }
            try { _menuReader?.Dispose(); } catch { }
            _menuOutput = null;
            _menuLoop = null;
            _menuReader = null;
        }

        // ---------------- small helper classes (NAudio patterns) ----------------

        private class LoopStream : WaveStream
        {
            private readonly WaveStream _source;
            public bool EnableLooping { get; set; } = true;
            public LoopStream(WaveStream source) { _source = source; }
            public override WaveFormat WaveFormat => _source.WaveFormat;
            public override long Length => long.MaxValue;
            public override long Position { get => _source.Position; set => _source.Position = value; }
            public override int Read(byte[] buffer, int offset, int count)
            {
                int read = _source.Read(buffer, offset, count);
                if (read == 0 && EnableLooping)
                {
                    _source.Position = 0;
                    read = _source.Read(buffer, offset, count);
                }
                return read;
            }
        }

        private class CachedSound
        {
            public float[] AudioData { get; }
            public WaveFormat WaveFormat { get; }

            public CachedSound(string audioFileName)
            {
                using (var afr = new AudioFileReader(audioFileName))
                {
                    WaveFormat = afr.WaveFormat;
                    var whole = new List<float>((int)(afr.Length / 4));
                    var buffer = new float[afr.WaveFormat.SampleRate * afr.WaveFormat.Channels];
                    int read;
                    while ((read = afr.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        for (int n = 0; n < read; n++) whole.Add(buffer[n]);
                    }
                    AudioData = whole.ToArray();
                }
            }
        }

        private class CachedSoundSampleProvider : ISampleProvider
        {
            private readonly CachedSound _cache;
            private long _position;
            public float Volume { get; set; } = 1.0f;
            public CachedSoundSampleProvider(CachedSound cache) { _cache = cache; }
            public WaveFormat WaveFormat => _cache.WaveFormat;

            public int Read(float[] buffer, int offset, int count)
            {
                int available = (int)(_cache.AudioData.Length - _position);
                int toCopy = Math.Min(available, count);
                if (toCopy <= 0) return 0;
                for (int n = 0; n < toCopy; n++)
                {
                    buffer[offset + n] = _cache.AudioData[_position + n] * Volume;
                }
                _position += toCopy;
                return toCopy;
            }
        }
    }
}
