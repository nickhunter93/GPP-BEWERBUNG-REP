using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Game
    {
        private RenderWindow _window;

        private CircleObject _circleObject;
        private float _circleSize = 10;
        private Vector2f _circlePosition;

        private Ai _aiLeft;
        private Ai _aiRight;

        private RectangleObject _rectangleObject1;
        private RectangleObject _rectangleObject2;
        private float _rectangleSizeX = 10;
        private float _rectangleSizeY = 100;
        private float _rectanglePositionX = 20;

        private Vector2f _windowSize = new Vector2f(1280, 800);

        private Font _font;
        private Text _scoreLeftText;
        private Text _scoreRightText;
        private Text _middleLine;
        private uint _characterSizeScore = 100;

        private int _scoreLeft = 0;
        private int _scoreRight = 0;

        private int _probabilityOfPowerUp = 3;


        private SoundManager _soundManager = new SoundManager();

        private List<PowerUp> _powerUps = new List<PowerUp>();
        private List<PowerUp> _prefabPowerUps = new List<PowerUp>();
        private List<int> _nextPowerUps = new List<int>();


        public Game(Vector2f windowSize, Font font)
        {
            _windowSize = windowSize;
            _font = font;
        }

        public void Start(Ai.Difficulty difficultyLeft, Ai.Difficulty difficultyRight)
        {
            Initialise(difficultyLeft, difficultyRight);

            double previous = DateTime.Now.TimeOfDay.TotalMilliseconds;
            while (_window.IsOpen)
            {
                double current = DateTime.Now.TimeOfDay.TotalMilliseconds;
                double elapsedTime = current - previous;
                previous = current;

                Input(elapsedTime);
                Update(elapsedTime);
                Redraw();

            }
        }


        private void Initialise(Ai.Difficulty difficultyLeft, Ai.Difficulty difficultyRight)
        {
            _window = new RenderWindow(new VideoMode((uint)_windowSize.X, (uint)_windowSize.Y), "PONG");
            _window.SetActive();

            _circlePosition = new Vector2f(_windowSize.X / 2, _windowSize.Y / 2);


            _rectangleObject1 = new RectangleObject(new Vector2f(_rectangleSizeX, _rectangleSizeY));
            _rectangleObject1.FillColor = Color.White;
            _rectangleObject1.Attach(new SoundObserver(_soundManager));
            _rectangleObject2 = new RectangleObject(new Vector2f(_rectangleSizeX, _rectangleSizeY));
            _rectangleObject2.FillColor = Color.White;
            _rectangleObject2.Attach(new SoundObserver(_soundManager));

            _circleObject = new CircleObject(_circleSize);
            _circleObject.Attach(new SoundObserver(_soundManager));
            _circleObject.FillColor = Color.White;


            _prefabPowerUps.Add(new SpeedDown(_circleObject));
            _prefabPowerUps.Add(new BiggerRectangleObject(_circleObject));
            _prefabPowerUps.Add(new BiggerRectangleObject(_circleObject));
            _prefabPowerUps.Add(new Disco(_circleObject));
            _prefabPowerUps.Add(new Disco(_circleObject));



            _circleObject.Position = _circlePosition;
            _circleObject.Update(0);


            _rectangleObject1.Position = new Vector2f(_rectanglePositionX, _windowSize.Y / 2);
            _rectangleObject2.Position = new Vector2f(_windowSize.X - _rectanglePositionX, _windowSize.Y / 2);

            _scoreLeftText = new Text(_scoreLeft.ToString(), _font, _characterSizeScore);
            _scoreLeftText.Position = new Vector2f(_windowSize.X / 2 - _scoreLeftText.GetGlobalBounds().Width - 100, 100);

            _scoreRightText = new Text(_scoreRight.ToString(), _font, _characterSizeScore);
            _scoreRightText.Position = new Vector2f(_windowSize.X / 2 + 100, 100);

            _middleLine = new Text("I", _font);
            _middleLine.Position = new Vector2f(_windowSize.X / 2 - _middleLine.GetGlobalBounds().Width / 2, 0);
            while (_middleLine.GetGlobalBounds().Height < _windowSize.Y)
            {
                _middleLine.DisplayedString += "\n\nI";
            }

            _aiLeft = new Ai(_rectangleObject1, _circleObject, _windowSize, difficultyLeft);
            _aiRight = new Ai(_rectangleObject2, _circleObject, _windowSize, difficultyRight);

            _nextPowerUps.Add(Random(0, _prefabPowerUps.Count));
        }

        private void Input(double elapsedTime)
        {
            RectangleMovement(_rectangleObject1, Keyboard.Key.W, Keyboard.Key.S, elapsedTime);
            RectangleMovement(_rectangleObject2, Keyboard.Key.Up, Keyboard.Key.Down, elapsedTime);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                _window.Close();
            }
        }

        private void RectangleMovement(RectangleObject rs, Keyboard.Key up, Keyboard.Key down, double elapsedTime)
        {
            if (Keyboard.IsKeyPressed(up))
            {
                rs.RectangleMovementUp(elapsedTime);
            }
            else if (Keyboard.IsKeyPressed(down))
            {
                rs.RectangleMovementDown(elapsedTime, _windowSize.Y);
            }
        }

        private void Update(double elapsedTime)
        {

            if (_nextPowerUps.Count < 100)
            {
                int i = Random(0, _prefabPowerUps.Count);
                if (i != _nextPowerUps[_nextPowerUps.Count-1])
                    _nextPowerUps.Add(i);
            }

            bool rectangleObject1Collided = _rectangleObject1.Collision(_circleObject, elapsedTime);
            bool rectangleObject2Collided = _rectangleObject2.Collision(_circleObject, elapsedTime);


            _circleObject.Update(elapsedTime);

            bool resetCircleObject = false;

            if (_circleObject.CheckOutOfField((int)_windowSize.X, (int)_windowSize.Y))
            {
                if (_circleObject.Position.X > _windowSize.X / 2)
                {
                    _scoreLeft++;
                }
                else
                {
                    _scoreRight++;
                }

                resetCircleObject = true;
                _circleObject.ResetPosition(_circlePosition);
                _circleObject.Direction = new Vector2f(-_circleObject.Direction.X, _circleObject.Direction.Y);
                _circleObject.Update(0);
                _powerUps.Clear();
            }

            if ((rectangleObject1Collided || rectangleObject2Collided) && Random(0, _probabilityOfPowerUp) == 0)
            {
                RectangleObject collidedRectangle;
                if (rectangleObject1Collided)
                    collidedRectangle = _rectangleObject2;
                else
                    collidedRectangle = _rectangleObject1;
                
                PowerUp newPowerUp = _prefabPowerUps[_nextPowerUps[0]].Clone();

                _nextPowerUps.RemoveAt(0);

                float speed = 0.3f;
                if (_windowSize.X / 2 < _circleObject.Position.X)
                {
                    newPowerUp.Spawn(new Vector2f(_rectangleObject2.Position.X - _rectangleObject2.Size.X - newPowerUp.Size.X, _circleObject.Position.Y), new Vector2f(-1, 0), speed, collidedRectangle);
                }
                else
                {
                    newPowerUp.Spawn(new Vector2f(_rectangleObject1.Position.X + _rectangleObject1.Size.X + newPowerUp.Size.X, _circleObject.Position.Y), new Vector2f(1, 0), speed, collidedRectangle);
                }

                _powerUps.Add(newPowerUp);
            }

            List<PowerUp> usedPowerUps = new List<PowerUp>();

            foreach (PowerUp powerUp in _powerUps)
            {
                powerUp.Update(elapsedTime);

                if (_rectangleObject1.PowerUpCollision(powerUp) || _rectangleObject2.PowerUpCollision(powerUp))
                {
                    powerUp.Execute();
                    usedPowerUps.Add(powerUp);
                }
                else if (powerUp.Position.X < 0 || powerUp.Position.X > _windowSize.X)
                {
                    usedPowerUps.Add(powerUp);
                }
            }

            foreach (PowerUp powerUp in usedPowerUps)
            {
                _powerUps.Remove(powerUp);
            }

            usedPowerUps.Clear();

            
            _aiLeft.Update(elapsedTime, rectangleObject2Collided, rectangleObject1Collided, resetCircleObject);
            _aiRight.Update(elapsedTime, rectangleObject1Collided, rectangleObject2Collided, resetCircleObject);


            if (_scoreLeft < 10)
                _scoreLeftText.DisplayedString = "0" + _scoreLeft.ToString();
            else
                _scoreLeftText.DisplayedString = _scoreLeft.ToString();

            if (_scoreRight < 10)
                _scoreRightText.DisplayedString = "0" + _scoreRight.ToString();
            else
                _scoreRightText.DisplayedString = _scoreRight.ToString();
        }

        private void Redraw()
        {
            _scoreLeftText.Position = new Vector2f(_windowSize.X / 2 - _scoreLeftText.GetGlobalBounds().Width - 100, 100);

            _window.Clear();
            _window.DispatchEvents();
            _window.Draw(_circleObject);
            _window.Draw(_rectangleObject1);
            _window.Draw(_rectangleObject2);
            _window.Draw(_scoreLeftText);
            _window.Draw(_scoreRightText);
            _window.Draw(_middleLine);

            foreach (PowerUp powerUp in _powerUps)
            {
                _window.Draw(powerUp);
            }

            _window.Display();
        }

        private int Random(int min, int max)
        {
            System.Random rnd = new System.Random();
            return rnd.Next(min, max);
        }

    }
}