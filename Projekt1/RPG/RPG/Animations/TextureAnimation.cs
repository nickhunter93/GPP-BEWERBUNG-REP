using SFML.Graphics;

namespace ConsoleApp2
{
    public class TextureAnimation : Animation
    {
        private RenderComponent _textureComponent;
        private Texture[] _textures;
        private double _changeSpeed;
        private double _timer;
        private int _selectedTextureNumber = 0;
        private bool _infinite = false;
        private Vector2D _scale;
        private Vector2D _originalScale;
        private bool _firstUpdate = true;
        

        public TextureAnimation(RenderComponent textureComponent, Texture[] textures, double duration, double timeOfStart, double changeSpeed, Vector2D scale, Vector2D originalScale)
        {
            _textureComponent = textureComponent;
            _textures = textures;
            _durationLeft = duration;
            _timeOfStart = timeOfStart;
            _changeSpeed = changeSpeed;
            _scale = scale;
            _originalScale = originalScale;

            if (duration < 0)
            {
                _infinite = true;
                _durationLeft = 1;
            }

        }

        public RenderComponent TextureComponent { get => _textureComponent; }

        public override void Update(double elapsedTime)
        {
            if (_timeOfStart > 0)
            {
                _timeOfStart -= elapsedTime;
                return;
            }

            if (_firstUpdate)
            {
                if (_originalScale.X > 0)
                    _scale = _scale - _originalScale;
                else
                    _scale = _scale - _textureComponent.Sprite.Scale;


                //System.Console.WriteLine(_scale + " " + _textureComponent.Sprite.Scale);
                _scale = _scale / _durationLeft;
            }

            _textureComponent.Sprite.Scale += _scale;


            _timer -= elapsedTime;


            if (_textures != null)
            {
                if (_textures.Length > 0)
                {

                    if (_timer < 0)
                    {
                        _timer = _changeSpeed;

                        _textureComponent.Sprite.Texture = _textures[_selectedTextureNumber];

                        _selectedTextureNumber++;
                        if (_selectedTextureNumber >= _textures.Length)
                            _selectedTextureNumber = 0;

                    }
                }
            }

            if (!_infinite)
                _durationLeft -= elapsedTime;


            _firstUpdate = false;
        }
    }
}