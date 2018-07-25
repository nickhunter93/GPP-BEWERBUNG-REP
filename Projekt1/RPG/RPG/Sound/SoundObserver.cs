namespace ConsoleApp2
{
    public class SoundObserver
    {
        private SoundManager _soundManager;

        public SoundObserver(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        public void Play(SoundManager.SoundNumbers soundNumber)
        {
            //_soundManager.OnPlay(soundNumber);
        }
    }
}