using SFML.Graphics;

namespace ConsoleApp2
{
    public class TextureGui : GuiElement
    {
        private bool _isPopup = false;

        public TextureGui(Vector2D position, Vector2D size, Font font, Texture texture, bool isPopup) : base(position, size, font)
        {
            _isPopup = isPopup;
            Position = position;
            _drawables.Add(InitialiseSprite(texture));
        }

        public TextureGui(Vector2D position, Vector2D size, Font font, Sprite sprite, bool isPopup) : base(position, size, font)
        {
            _isPopup = isPopup;
            sprite.Position = position;
            Position = position;
            _drawables.Add(sprite);
        }

        public void ChangeTexture(Texture texture)
        {
            _drawables.Clear();
            _drawables.Add(InitialiseSprite(texture));
        }

        private Sprite InitialiseSprite(Texture texture)
        {
            Sprite sprite = new Sprite(texture);
            sprite.Position = Position;
            return sprite;
        }

        public override bool Touched(Vector2D position)
        {
            if (_isPopup)
            {
                MyConsole.Out("HMM");
                return true;
            }
            return false;
        }

        public override void Update(double elapsedTime)
        {

        }
    }
}