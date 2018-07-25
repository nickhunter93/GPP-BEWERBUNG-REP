using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Threading;

namespace ConsoleApp2
{
    public class SoundManager
    {
        private List<MediaPlayer> _mediaPlayers = new List<MediaPlayer>();
        private List<Uri> _uris = new List<Uri>();
        private int _soundLength = 0;

        public enum SoundNumbers
        {
            HitPlayer,
            HitBorder,
            Lose
        }

        public SoundManager()
        {
            _uris.Add(new Uri("hit player.wav", UriKind.Relative));
            _uris.Add(new Uri("hit border.wav", UriKind.Relative));
            _uris.Add(new Uri("lose.wav", UriKind.Relative));
        }

        public void Update()
        {

        }

        public void Play(SoundNumbers soundNumber)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();

            mediaPlayer.Open(_uris[(int)soundNumber]);
            mediaPlayer.Play();

            _mediaPlayers.Add(mediaPlayer);

            if (_mediaPlayers[_mediaPlayers.Count - 1].NaturalDuration.HasTimeSpan)
                _soundLength = _mediaPlayers[_mediaPlayers.Count - 1].NaturalDuration.TimeSpan.Milliseconds;

            Thread newThread = new Thread(StopSound);
            newThread.Start();

        }
        
        private void StopSound()
        {
            Thread.Sleep(_soundLength);
            _mediaPlayers.RemoveAt(0);
        }


    }
}