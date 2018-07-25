using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp2
{

    public class GameOver
    {
        private RenderWindow _window;
        private Font _font;
        private bool _isMousePressed = false;

        private Text _gameOver;
        private Text _pressToRestart;

        private List<Text> _scores;
        private uint _characterSizeScore = 100;

        private Stopwatch _stopwatch = new Stopwatch();

        private AnimationManager _animationManager = new AnimationManager();

        public GameOver(RenderWindow window, Font font)
        {
            _window = window;
            _font = font;
        }

        public void Start()
        {
            Initialise();
        }

        public void Run(List<Text> scores)
        {
            double elapsedTime = (double)_stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000;

            _stopwatch.Restart();

            Input();
            Update(elapsedTime, scores);
            Redraw();

            _stopwatch.Stop();
        }

        private void Initialise()
        {

            _gameOver = new Text("GAME OVER", _font, 60);
            MainMenu.SetTextOriginToMiddle(_gameOver);
            _gameOver.Position = new Vector2D(Program.windowSize.X / 2, 100);

            _pressToRestart = new Text("PRESS ENTER TO RESTART", _font, 40);
            _pressToRestart.Origin = new Vector2D(_pressToRestart.GetGlobalBounds().Width / 2, _pressToRestart.GetGlobalBounds().Height);
            //SetTextOriginToMiddle(_pressToContinue);
            _pressToRestart.Position = new Vector2D(Program.windowSize.X / 2, Program.windowSize.Y - 100);

            _animationManager.AddAnimation(new Animation(new Vector2D(Program.windowSize.X / 2, Program.windowSize.Y + _pressToRestart.GetGlobalBounds().Height), new Transformable[] { _pressToRestart }, 400, 0, true));
            
        }

        private void Input()
        {

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) || Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                if (!Program.isEscapePressed)
                {
                    Program.isEscapePressed = true;
                    Restart();
                }
            }
            else
            {
                Program.isEscapePressed = false;
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (!_isMousePressed)
                {

                    _isMousePressed = true;

                    Vector2D mousePosition = Mouse.GetPosition(_window);
                    

                    if (MainMenu.Intersection(_pressToRestart.GetGlobalBounds(), mousePosition))
                    {
                        Restart();
                    }
                    
                }
            }
            else
            {
                _isMousePressed = false;
            }
        }

        private void Update(double elapsedTime, List<Text> scores)
        {
            _animationManager.Update(elapsedTime);

            if (_scores == null)
            {
                _scores = scores;

                foreach (Text score in _scores)
                {
                    MainMenu.SetTextOriginToMiddle(score);
                    score.Position += new Vector2D(score.GetGlobalBounds().Width / 2, score.GetGlobalBounds().Height / 2);
                    Vector2D scorePosition = new Vector2D(score.Position.X + (Program.windowSize.X / 2 - score.Position.X) / 1.5, _gameOver.Position.Y + _gameOver.GetGlobalBounds().Height * 4);
                    _animationManager.AddAnimation(new Animation(scorePosition, new Transformable[] { score }, 1000, 0, false));
                }
            }
        }

        private void Redraw()
        {
            _window.Clear();
            _window.DispatchEvents();

            _window.Draw(_gameOver);
            _window.Draw(_pressToRestart);

            foreach (Text score in _scores)
            {
                _window.Draw(score);
            }

            _window.Display();
        }

        private void Restart()
        {
            Program.windowState = Program.WindowState.MainMenu;
            Program.Restart(true);
        }
    }


}