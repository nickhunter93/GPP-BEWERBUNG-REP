using SFML.Graphics;

namespace ConsoleApp2
{
    public class Slider : GuiElement
    {
        private RectangleShape _box;
        private CircleShape _circle;
        private int _value = 50;

        public Slider(Vector2D position, Vector2D size, Font font) : base(position, size, font)
        {
            _box = new RectangleShape(size);
            _box.FillColor = Color.White;
            _box.Origin = size / 2;
            _box.Position = position;

            _drawables.Add(_box);

            double smallerOne;
            if (size.X < size.Y)
                smallerOne = size.X;
            else
                smallerOne = size.Y;

            _circle = new CircleShape((float)(smallerOne * 2));
            _circle.FillColor = Color.Black;
            _circle.OutlineColor = Color.White;
            _circle.OutlineThickness = 1.5f;
            _circle.Origin = new Vector2D(smallerOne * 2, smallerOne / 2 - 1);
            //_circle.Position = position + new Vector2D(-_box.GetGlobalBounds().Width / 2, -_circle.GetGlobalBounds().Height / 2 + _box.GetGlobalBounds().Height / 2);
            //_circle.Position = position + new Vector2D(_box.GetGlobalBounds().Width / 2, -_circle.GetGlobalBounds().Height / 2 + _box.GetGlobalBounds().Height / 2);
            _circle.Position = position + new Vector2D(0, -_circle.GetGlobalBounds().Height / 2 + _box.GetGlobalBounds().Height / 2);

            _drawables.Add(_circle);
        }

        public int Value { get => _value; set => _value = value; }

        public override bool Touched(Vector2D position)
        {
            CircleShape mouseCircle = new CircleShape(2);
            mouseCircle.Origin = new Vector2D(1, 1);
            mouseCircle.Position = position;

            if (_circle.GetGlobalBounds().Intersects(mouseCircle.GetGlobalBounds()) || _box.GetGlobalBounds().Intersects(mouseCircle.GetGlobalBounds()))
            {
                double xPosition = position.X;

                if (xPosition < _box.Position.X + -_box.GetGlobalBounds().Width / 2)
                {
                    xPosition = _box.Position.X + -_box.GetGlobalBounds().Width / 2;
                }
                else if (xPosition > _box.Position.X + _box.GetGlobalBounds().Width / 2)
                {
                    xPosition = _box.Position.X + _box.GetGlobalBounds().Width / 2;
                }

                _circle.Position = new Vector2D(xPosition, _circle.Position.Y);

                double max = (_box.Position.X + _box.GetGlobalBounds().Width / 2) - (_box.Position.X + -_box.GetGlobalBounds().Width / 2);
                double factor = 100 / max;

                Value = (int)((xPosition - (_box.Position.X + -_box.GetGlobalBounds().Width / 2)) * factor);

                return true;
            }

            return false;
        }

        public override void Update(double elapsedTime)
        {
        }
    }
}