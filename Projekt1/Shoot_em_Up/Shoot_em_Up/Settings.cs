namespace ConsoleApp2
{
    public class Settings
    {
        private bool _fullscreen;
        private int _volume;
        private int _maxLifes;
        private Vector2D _windowSize;

        public Settings(bool fullscreen, int volume, int maxLifes, Vector2D windowSize)
        {
            _fullscreen = fullscreen;
            _volume = volume;
            _maxLifes = maxLifes;
            _windowSize = windowSize;
        }

        public bool Fullscreen { get => _fullscreen; }
        public int Volume { get => _volume; }
        public int MaxLifes { get => _maxLifes; }
        public Vector2D WindowSize { get => _windowSize; }
    }
}