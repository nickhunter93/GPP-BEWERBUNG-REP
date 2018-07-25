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

        private Text _score;
        private uint _characterSizeScore = 100;

        private Stopwatch _stopwatch = new Stopwatch();

        private AnimationManager _animationManager = new AnimationManager();

        private bool _draw = false;

        private View camera;

        public GameOver(RenderWindow window, Font font)
        {
            _window = window;
            _font = font;
        }

        public void Start()
        {
            Initialise();
        }

        public void Run(Text score, bool isWon)
        {
            if(camera == null)
            {
                camera = new View(Program.windowSize/2, Program.windowSize);
            }

            _window.SetView(camera);

            if (isWon)
                _gameOver.DisplayedString = "SURVIVED";

            double elapsedTime = (double)_stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000;

            _stopwatch.Restart();

            Input();
            Update(elapsedTime, score);
            if (!_draw) { Redraw(); _draw = true; }

            _stopwatch.Stop();
        }

        private void Initialise()
        {

            _gameOver = new Text("YOU DIED", _font, 60);
            MainMenu.SetTextOriginToMiddle(_gameOver);
            _gameOver.Position = new Vector2D(Program.windowSize.X / 2, 100);
            _gameOver.OutlineColor = Color.Black;
            _gameOver.OutlineThickness = 2;

            _pressToRestart = new Text("PRESS ENTER TO RESTART", _font, 40);
            _pressToRestart.Origin = new Vector2D(_pressToRestart.GetGlobalBounds().Width / 2, _pressToRestart.GetGlobalBounds().Height);
            _pressToRestart.OutlineColor = Color.Black;
            _pressToRestart.OutlineThickness = 2;
            //SetTextOriginToMiddle(_pressToContinue);
            /*_pressToRestart.Position = new Vector2D(Program.windowSize.X / 2, Program.windowSize.Y - 100);

            _animationManager.AddAnimation(new MoveAnimation(new Vector2D(Program.windowSize.X / 2, Program.windowSize.Y + _pressToRestart.GetGlobalBounds().Height), new Transformable[] { _pressToRestart }, 400, 0, true));
            */
            _pressToRestart.Position = new Vector2D(Program.windowSize.X / 2, Program.windowSize.Y - 100);
            
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

        private void Update(double elapsedTime, Text score)
        {
            _animationManager.Update(elapsedTime);

            /*if (_score == null)
            {
                _score = score;
                
                MainMenu.SetTextOriginToMiddle(score);
                score.Position += new Vector2D(score.GetGlobalBounds().Width / 2, score.GetGlobalBounds().Height / 2);
                Vector2D scorePosition = new Vector2D(score.Position.X + (Program.windowSize.X / 2 - score.Position.X) / 1.5, _gameOver.Position.Y + _gameOver.GetGlobalBounds().Height * 4);
                _animationManager.AddAnimation(new MoveAnimation(scorePosition, new Transformable[] { score }, 1000, 0, false));
                
            }*/
        }

        private void Redraw()
        {
            //_window.Clear();
            _window.DispatchEvents();

            _window.Draw(_gameOver);
            _window.Draw(_pressToRestart);
            
            //_window.Draw(_score);
            
            _window.Display();
        }

        private void Restart()
        {
            Program.windowState = Program.WindowState.MainMenu;
            Program.Restart(false);
        }
    }


}