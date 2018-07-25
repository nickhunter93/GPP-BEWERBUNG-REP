using System;

namespace ConsoleApp2
{
    public class SoundsEventArgs : EventArgs
    {
        private SoundManager.SoundNumbers _soundNumber;

        public SoundManager.SoundNumbers SoundNumber { get => _soundNumber; }

        public SoundsEventArgs(SoundManager.SoundNumbers soundNumber)
        {
            _soundNumber = soundNumber;
        }
        
    }
}