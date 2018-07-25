using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp2
{

    public class GameOver : IRegisterEvent
    {
        private RenderWindow _window;
        private Font _font;

        private Text _gameOver;
        private Text _pressToRestart;

        private Stopwatch _stopwatch = new Stopwatch();

        private int _draw = 2;
        
        private bool _moved = false;

        public GameOver(RenderWindow window, Font font)
        {
            _window = window;
            _font = font;

            MessageBus.RegisterEvent(this);
        }

        public event EventHandler Play;

        public void OnPlay(String sound)
        {
            if (sound == "menuselect")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Menuselect));
        }

        public void Start()
        {
            Initialise();
        }

        public void Run(bool isWon)
        {

            if (!_moved)
            {
                Vector2D position = new Vector2D(DataManager.GetInstance().Window.GetView().Center.X, DataManager.GetInstance().Window.GetView().Center.Y);
                position -= Program.windowSize / 2;
                _moved = true;
                _gameOver.Position += position;
                _pressToRestart.Position += position;
            }

            if (isWon)
                _gameOver.DisplayedString = "SURVIVED";

            double elapsedTime = (double)_stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000;

            _stopwatch.Restart();

            Input();
            if (_draw > 0) { Redraw(); _draw--; }
            _stopwatch.Stop();

            MusicManager.GetInstance().Stop();
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
            _pressToRestart.Position = new Vector2D(Program.windowSize.X / 2, Program.windowSize.Y - 100);
            
        }

        private void Input()
        {

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) || Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                if (!Program.isEscapePressed)
                {
                    Program.isEscapePressed = true;
                    OnPlay("menuselect");
                    Restart();
                }
            }
            else
            {
                Program.isEscapePressed = false;
            }
            
        }

        private void Redraw()
        {
            //_window.Clear();
            _window.DispatchEvents();

            _window.Draw(_gameOver);
            _window.Draw(_pressToRestart);
            
            _window.Display();
        }

        private void Restart()
        {
            Program.windowState = Program.WindowState.MainMenu;
            Program.Restart(false);
        }
    }


}