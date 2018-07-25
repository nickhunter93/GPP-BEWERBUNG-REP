using SFML.Graphics;
using SFML.System;

namespace ConsoleApp2
{
    public class Checkbox : GuiElement
    {
        private RectangleShape _box;
        private bool _isChecked;
        private bool _isUnCheckable;
        private Text _x;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;

                if (_x != null)
                {
                    if (_isChecked)
                    {
                        _x.DisplayedString = "X";
                    }
                    else
                    {
                        _x.DisplayedString = "";
                    }
                }
            }
        }

        public bool IsUnCheckable { get => _isUnCheckable; set => _isUnCheckable = value; }

        public Checkbox(Vector2D position, Vector2D size, Font font, bool isChecked, bool invertFont, bool isUnCheckable) : base(position, size, font)
        {
            _box = new RectangleShape(size);
            _box.FillColor = Color.Black;
            _box.OutlineColor = Color.White;
            _box.OutlineThickness = 1;
            _box.Origin = size / 2;
            _box.Position = position;
            
            _x = new Text("X", font, (uint)_size.X);
            _x.Origin = new Vector2D(_x.GetGlobalBounds().Width / 2, _x.GetGlobalBounds().Height / 2);
            _x.Position = _box.Position + new Vector2D(-1.5, -5);

            if (invertFont)
            {
                _x.OutlineThickness = 1;
                _x.OutlineColor = Color.White;
                _x.FillColor = Color.Black;
            }

            IsChecked = isChecked;
            IsUnCheckable = isUnCheckable;

            _drawables.Add(_box);
            _drawables.Add(_x);
        }

        public override void Touched(Vector2D position)
        {
            CircleShape mouseCircle = new CircleShape(2);
            mouseCircle.Origin = new Vector2D(1, 1);
            mouseCircle.Position = position;

            if (_box.GetGlobalBounds().Intersects(mouseCircle.GetGlobalBounds()))
            {
                if (IsUnCheckable)
                    IsChecked = !IsChecked;
                else
                    IsChecked = true;
            }
            
        }
        
    }
}