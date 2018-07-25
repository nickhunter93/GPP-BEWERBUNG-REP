using SFML.Graphics;

namespace ConsoleApp2
{
    public class SimpleText : GuiElement
    {
        private Text _text;
        private bool _leftBound;

        public SimpleText(Vector2D position, Vector2D size, Font font, string text, uint fontSize, bool leftBound) : base(position, size, font)
        {
            _text = new Text(text, font, fontSize);
            _text.Position = position;
            _drawables.Add(_text);
            _leftBound = leftBound;
            RepositionText();
        }

        public Text Text { get => _text; set => _text = value; }

        public void ChangeText(string text)
        {
            _text.DisplayedString = text;
            RepositionText();
        }

        private void RepositionText()
        {
            if (_leftBound)
                return;
            _text.Origin = new Vector2D(_text.GetGlobalBounds().Width, 0);
            _text.Position = Position;
        }

        public override bool Touched(Vector2D position)
        {
            CircleShape mouseCircle = new CircleShape(2);
            mouseCircle.Origin = new Vector2D(1, 1);
            mouseCircle.Position = position;

            if (_text.GetGlobalBounds().Intersects(mouseCircle.GetGlobalBounds()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Update(double elapsedTime)
        {
        }
    }
}