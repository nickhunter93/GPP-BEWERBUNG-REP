using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Menu
    {
        private RenderWindow _window;
        private Vector2f _windowSize;
        private Font _font;

        private Text _pong;
        private Text _left;
        private Text _right;
        private Text _human;
        private Text _targetMode;
        private Text _easy;
        private Text _normal;
        private Text _hard;
        private Text _pressToContinue;
        private Text _xLeft;
        private Text _xRight;

        private List<RectangleShape> _checkBoxes = new List<RectangleShape>();
        private float _checkBoxSize = 0;
        private CircleShape _mouseCircle;
        private Ai.Difficulty _difficultyLeft = Ai.Difficulty.Human;
        private Ai.Difficulty _difficultyRight = Ai.Difficulty.Human;

        public Menu(Vector2f windowSize, Font font)
        {
            _windowSize = windowSize;
            _font = font;
        }

        public void Start()
        {
            Initialise();

            while (_window.IsOpen)
            {
                Input();
                Redraw();
            }
            
        }

        private void Initialise()
        {
            _window = new RenderWindow(new VideoMode((uint)_windowSize.X, (uint)_windowSize.Y), "PONG");
            _window.SetActive();


            _pong = new Text("PONG", _font, 60);
            _pong.Position = new Vector2f(_windowSize.X / 2 - _pong.GetGlobalBounds().Width / 2, 100);

            _left = new Text("LEFT", _font, 20);
            _left.Position = new Vector2f(_windowSize.X / 2 - _left.GetGlobalBounds().Width - 150, _pong.Position.Y + _pong.GetGlobalBounds().Height + 100);

            _right = new Text("RIGHT", _font, 20);
            _right.Position = new Vector2f(_windowSize.X / 2 + 150, _left.Position.Y);

            _human = new Text("HUMAN", _font, 20);
            _human.Position = new Vector2f(_windowSize.X / 2 - _human.GetGlobalBounds().Width / 2, _left.Position.Y + _left.GetGlobalBounds().Height + 50);

            _targetMode = new Text("TARGET MODE", _font, 20);
            _targetMode.Position = new Vector2f(_windowSize.X / 2 - _targetMode.GetGlobalBounds().Width / 2, _human.Position.Y + _human.GetGlobalBounds().Height + 50);

            _easy = new Text("EASY", _font, 20);
            _easy.Position = new Vector2f(_windowSize.X / 2 - _easy.GetGlobalBounds().Width / 2, _targetMode.Position.Y + _targetMode.GetGlobalBounds().Height + 50);

            _normal = new Text("NORMAL", _font, 20);
            _normal.Position = new Vector2f(_windowSize.X / 2 - _normal.GetGlobalBounds().Width / 2, _easy.Position.Y + _easy.GetGlobalBounds().Height + 50);

            _hard = new Text("HARD", _font, 20);
            _hard.Position = new Vector2f(_windowSize.X / 2 - _hard.GetGlobalBounds().Width / 2, _normal.Position.Y + _normal.GetGlobalBounds().Height + 50);

            _pressToContinue = new Text("PRESS ENTER TO CONTINUE", _font, 40);
            _pressToContinue.Position = new Vector2f(_windowSize.X / 2 - _pressToContinue.GetGlobalBounds().Width / 2, _windowSize.Y - 100);

            _xLeft = new Text("X", _font, 20);
            _xLeft.Position = new Vector2f(_left.Position.X + _left.GetGlobalBounds().Width / 2 - _xLeft.GetGlobalBounds().Width / 2, _human.Position.Y);

            _xRight = new Text("X", _font, 20);
            _xRight.Position = new Vector2f(_right.Position.X + _right.GetGlobalBounds().Width / 2 - _xRight.GetGlobalBounds().Width / 2, _human.Position.Y);


            
            _checkBoxSize = 24;
            Vector2f sizeInVector = new Vector2f(12, 12);

            for (int i = 0; i < 10; i++)
            {
                RectangleShape newRectangleShape = new RectangleShape(new Vector2f(_checkBoxSize, _checkBoxSize));
                newRectangleShape.FillColor = Color.Transparent;
                newRectangleShape.OutlineColor = Color.White;
                newRectangleShape.OutlineThickness = 1;
                newRectangleShape.Origin = new Vector2f(12, 12);
                _checkBoxes.Add(newRectangleShape);

            }

            float leftX = _left.Position.X + _left.GetGlobalBounds().Width / 2 - _checkBoxSize / 2;
            float rightX = _right.Position.X + _right.GetGlobalBounds().Width / 2 - _checkBoxSize / 2;
            
            _checkBoxes[0].Position = new Vector2f(leftX, _human.Position.Y) + sizeInVector;
            _checkBoxes[1].Position = new Vector2f(rightX, _human.Position.Y) + sizeInVector;
            _checkBoxes[2].Position = new Vector2f(leftX, _targetMode.Position.Y) + sizeInVector;
            _checkBoxes[3].Position = new Vector2f(rightX, _targetMode.Position.Y) + sizeInVector;
            _checkBoxes[4].Position = new Vector2f(leftX, _easy.Position.Y) + sizeInVector;
            _checkBoxes[5].Position = new Vector2f(rightX, _easy.Position.Y) + sizeInVector;
            _checkBoxes[6].Position = new Vector2f(leftX, _normal.Position.Y) + sizeInVector;
            _checkBoxes[7].Position = new Vector2f(rightX, _normal.Position.Y) + sizeInVector;
            _checkBoxes[8].Position = new Vector2f(leftX, _hard.Position.Y) + sizeInVector;
            _checkBoxes[9].Position = new Vector2f(rightX, _hard.Position.Y) + sizeInVector;

            _xLeft.Origin = new Vector2f(_xLeft.GetGlobalBounds().Width / 2, _xLeft.GetGlobalBounds().Height / 2 + 4);
            _xLeft.Position = _checkBoxes[0].Position;

            _xRight.Origin = new Vector2f(_xRight.GetGlobalBounds().Width / 2 + 1, _xRight.GetGlobalBounds().Height / 2 + 4);
            _xRight.Position = _checkBoxes[1].Position;


            _mouseCircle = new CircleObject(2);
            _mouseCircle.Origin = new Vector2f(1, 1);
        }

        private void Input()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) || Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                _window.Close();
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {

                _mouseCircle.Position = (Vector2f)Mouse.GetPosition(_window);

                for (int i = 0; i < _checkBoxes.Count; i++)
                {
                    if (_mouseCircle.GetGlobalBounds().Intersects(_checkBoxes[i].GetGlobalBounds()))
                    {
                        if (i % 2 == 0)
                        {
                            _xLeft.Position = _checkBoxes[i].Position;
                            _difficultyLeft = (Ai.Difficulty)(i / 2);
                            break;
                        }
                        else
                        {
                            _xRight.Position = _checkBoxes[i].Position;
                            _difficultyRight = (Ai.Difficulty)((i - 1) / 2);
                            break;
                        }
                    }
                }
                

                if (_mouseCircle.GetGlobalBounds().Intersects(_pressToContinue.GetGlobalBounds()))
                {
                    _window.Close();
                }

            }
        }

        private void Redraw()
        {
            _window.Clear();
            _window.DispatchEvents();

            foreach (RectangleShape checkBox in _checkBoxes)
            {
                _window.Draw(checkBox);
            }

            _window.Draw(_pong);
            _window.Draw(_left);
            _window.Draw(_right);
            _window.Draw(_human);
            _window.Draw(_targetMode);
            _window.Draw(_easy);
            _window.Draw(_normal);
            _window.Draw(_hard);
            _window.Draw(_pressToContinue);
            _window.Draw(_xLeft);
            _window.Draw(_xRight);


            _window.Display();
        }

        public Ai.Difficulty GetDifficultyLeft()
        {
            return _difficultyLeft;
        }

        public Ai.Difficulty GetDifficultyRight()
        {
            return _difficultyRight;
        }
    }
}
