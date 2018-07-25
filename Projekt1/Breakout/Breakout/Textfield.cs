using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace ConsoleApp2
{
    public class Textfield : GuiElement
    {
        private RectangleShape _box;
        private Text _text;
        private bool _isChecked;
        private RenderWindow _window;
        private bool _isOverflowActivated;
        private double _blinkTimer = 0;
        private double _blinkInterval = 500;
        private Text _blinker;
        private bool _isBlinkerActivated = false;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;

                if (_isChecked)
                {
                    _window.TextEntered += _window_TextEntered;
                    IsBlinkerActivated = true;
                }
                else
                {
                    _window.TextEntered -= _window_TextEntered;
                    IsBlinkerActivated = false;
                }
            }
        }

        public void SetText(String text)
        {
            RemoveText();

            foreach (Char letter in text)
            {
                AddLetter(letter);
            }
        }

        public void RemoveText()
        {
            _text.DisplayedString = "";
        }

        public bool IsBlinkerActivated
        {
            get { return _isBlinkerActivated; }
            set
            {
                _isBlinkerActivated = value;

                if (_isBlinkerActivated)
                {
                    if (_blinker != null && !_drawables.Contains(_blinker) && IsChecked)
                    {
                        _drawables.Add(_blinker);
                        _blinkTimer = _blinkInterval;
                        ReplaceBlinker();
                    }
                }
                else
                {
                    if (_drawables.Contains(_blinker))
                        _drawables.Remove(_blinker);
                }
            }
        }

        public Textfield(Vector2D position, Vector2D size, Font font, uint fontSize, bool isChecked, string text, RenderWindow window, bool isOverflowActivated) : base(position, size, font)
        {
            _box = new RectangleShape(size);
            _box.FillColor = Color.Black;
            _box.OutlineColor = Color.White;
            _box.OutlineThickness = 1;
            _box.Origin = size / 2;
            _box.Position = position;

            _text = new Text(text, font, fontSize);
            _text.Origin = new Vector2D(_text.GetGlobalBounds().Width / 2, _text.GetGlobalBounds().Height / 2);
            _text.Position = _box.Position + new Vector2D(-1.5, -5);

            _blinker = new Text("I", font, fontSize);
            _blinker.Origin = new Vector2D(_blinker.GetGlobalBounds().Width / 2, _blinker.GetGlobalBounds().Height / 2);
            ReplaceBlinker();
                        
            _drawables.Add(_box);
            _drawables.Add(_text);

            this._isOverflowActivated = isOverflowActivated;
            _window = window;
            IsChecked = isChecked;
        }

        public override void Touched(Vector2D position)
        {
            CircleShape mouseCircle = new CircleShape(2);
            mouseCircle.Origin = new Vector2D(1, 1);
            mouseCircle.Position = position;

            if (_box.GetGlobalBounds().Intersects(mouseCircle.GetGlobalBounds()))
            {
                IsChecked = true;
            }
            else
            {
                IsChecked = false;
            }
        }

        public void Update()
        {
            _blinkTimer -= 1;
            if (_blinkTimer <= 0)
            {
                _blinkTimer = _blinkInterval;

                IsBlinkerActivated = !IsBlinkerActivated;
            }
        }

        private void _window_TextEntered(object sender, TextEventArgs e)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) || Keyboard.IsKeyPressed(Keyboard.Key.Escape))       //unicode?
            {
                IsChecked = false;
            }
            else
            {
                if (e.Unicode == "\b")
                {
                    RemoveLastLetter();
                }
                else
                    AddLetter(e.Unicode[0]);
            }

        }

        private void AddLetter(Char letter)
        {
            _text.DisplayedString += letter;

            RepositionText();
        }

        private void RepositionText()
        {
            if (_isOverflowActivated && _text.GetGlobalBounds().Width + _blinker.GetGlobalBounds().Width * 2 > _box.GetGlobalBounds().Width)
            {
                RemoveLastLetter();
            }

            _text.Origin = new Vector2D(_text.GetGlobalBounds().Width / 2, _text.GetGlobalBounds().Height / 2);
            _text.Position = _box.Position + new Vector2D(-1.5, -5);
            ReplaceBlinker();
        }

        private void ReplaceBlinker()
        {
            _blinker.Position = new Vector2D(_text.Position.X + _text.GetGlobalBounds().Width / 2 + _blinker.GetGlobalBounds().Width, _text.Position.Y);
            _blinkTimer = _blinkInterval;
            IsBlinkerActivated = true;
        }

        private void RemoveLastLetter()
        {
            if (_text.DisplayedString.Length > 0)
                _text.DisplayedString = _text.DisplayedString.Remove(_text.DisplayedString.Length - 1);

            RepositionText();
        }

        public string GetText()
        {
            return _text.DisplayedString;
        }

        public int GetNumbersInText()
        {
            string newString = "";
            foreach (char c in _text.DisplayedString)
            {
                if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                {
                    newString += c;
                }
            }

            if (newString == "")
            {
                return 0;
            }

            return int.Parse(newString);
        }

    }
}