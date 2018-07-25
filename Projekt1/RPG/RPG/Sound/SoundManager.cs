using System;
using System.Collections.Generic;
using SFML.Audio;

namespace ConsoleApp2
{
    public class SoundManager
    {
        private List<SoundBuffer> _soundBuffers = new List<SoundBuffer>();
        //private Sound _sound;
        private List<Sound> _sounds = new List<Sound>();

        public enum SoundNumbers
        {
            Bonfire,
            Chestmimic,
            Chestopen,
            Crossbow,
            Die,
            Estusdrink,
            Estusempty,
            Gameover,
            Hitenemy,
            Hitobject,
            Menuselect,
            Powerup,
            Roll,
            Startgame,
            Sword
        }
        

        public SoundManager()
        {
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/bonfire.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/chestmimic.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/chestopen.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/crossbow.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/die.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/estusdrink.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/estusempty.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/gameover.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/hitenemy.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/hitobject.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/menuselect.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/powerup.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/roll.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/startgame.ogg"));
            _soundBuffers.Add(new SoundBuffer("Soundfiles/DarkSouls/sword.ogg"));
        }

        public void Update()
        {
            List<Sound> remove = new List<Sound>();

            foreach (Sound sound in _sounds)
            {
                if (sound.Status == SoundStatus.Paused || sound.Status == SoundStatus.Stopped)
                {
                    remove.Add(sound);
                }
            }

            foreach (Sound r in remove)
            {
                _sounds.Remove(r);
            }
        }
        
        public void OnPlay(object sender, EventArgs e)
        {
            if (Program.muted)
                return;

            if (e is SoundsEventArgs es)
            {
                Sound sound = new Sound(_soundBuffers[(int)es.SoundNumber]);
                sound.Volume = Program.soundVolume;
                sound.Play();
                _sounds.Add(sound);
            }
        }


    }
}