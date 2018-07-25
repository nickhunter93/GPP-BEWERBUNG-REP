using System;
using System.Collections.Generic;
using SFML.Audio;

namespace ConsoleApp2
{
    public class MusicManager
    {
        private List<String> _musicPaths = new List<String>();
        private Music _music;
        private static MusicManager _instance = null;

        public enum MusicNumbers
        {
            Main,
            Level1,
            Level2
        }

        public static MusicManager GetInstance()
        {
            if (_instance == null)
                _instance = new MusicManager();
            return _instance;
        }

        private MusicManager()
        {
            _musicPaths.Add("Musicfiles/main.ogg");
            _musicPaths.Add("Musicfiles/level1.ogg");
            _musicPaths.Add("Musicfiles/level2.ogg");
            _music = new Music(_musicPaths[0]);
        }

        public void Play(MusicNumbers musicNumber)
        {
            if (_music != null)
                _music.Stop();
            _music = new Music(_musicPaths[(int)musicNumber]);
            _music.Volume = Program.musicVolume;
            _music.Loop = true;
            _music.Play();
        }

        public void Stop()
        {
            _music.Stop();
        }

        public void ChangeVolume(int volume)
        {
            _music.Volume = volume;
        }
    }
}