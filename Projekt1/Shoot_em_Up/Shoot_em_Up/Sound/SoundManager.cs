using System;
using System.Collections.Generic;
using SFML.Audio;

namespace ConsoleApp2
{
    public class SoundManager
    {
        private List<SoundBuffer> _soundBuffers = new List<SoundBuffer>();

        public enum SoundNumbers
        {
            HitPlayer,
            HitBorder,
            Lose
        }

        public SoundManager()
        {
            _soundBuffers.Add(new SoundBuffer("Soundfiles/hit player.wav"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/hit border.wav"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/lose.wav"));
        }
        
        public void OnPlay(object sender, EventArgs e)
        {
            if (Program.muted)
                return;

            if (e is SoundsEventArgs es)
            {
                Sound sound = new Sound(_soundBuffers[(int)es.SoundNumber]);
                sound.Volume = Program.volume;
                sound.Play();
            }
        }


    }
}