using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp2
{
    public class MainMenu
    {
        private RenderWindow _window;
        private Font _font;
        private bool _isMousePressed = false;

        private Text _title;
        private Text _pressToContinue;
        private Text _restart;
        private Text _exit;
        private DifficultyGui _difficultyGui;
        private SettingsGui _settingsGui;
        private RectangleShape _rectangleLeft;
        private RectangleShape _rectangleRight;

        private Stopwatch _stopwatch = new Stopwatch();

        private AnimationManager _animationManager = new AnimationManager();


        public MainMenu(RenderWindow window, Font font)
        {
            _window = window;
            _font = font;
        }

        public void Start()
        {
            Initialise();
        }

        public void Run()
        {
            double elapsedTime = (double)_stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000;

            _stopwatch.Restart();

            Input();
            Update(elapsedTime);
            Redraw();

            _stopwatch.Stop();
        }

        private void Initialise()
        {
            Vector2D rSize = new Vector2D(Program.windowSize.X / 2, Program.windowSize.Y / 2);

            _rectangleLeft = new RectangleShape(rSize);
            _rectangleLeft.Origin = rSize / 2;
            _rectangleLeft.Position = new Vector2D(Program.windowSize.X / 4 - 50, Program.windowSize.Y / 2);
            _rectangleLeft.OutlineThickness = 3;
            _rectangleLeft.OutlineColor = Color.White;
            _rectangleLeft.FillColor = Color.Black;

            _rectangleRight = new RectangleShape(_rectangleLeft);
            _rectangleRight.Position = new Vector2D(3 * Program.windowSize.X / 4 + 50, Program.windowSize.Y / 2);



            _title = new Text("BREAKOUT", _font, 60);
            SetTextOriginToMiddle(_title);
            _title.Position = new Vector2D(Program.windowSize.X / 2, 100);
            
            _pressToContinue = new Text("PRESS ENTER TO CONTINUE", _font, 40);
            _pressToContinue.Origin = new Vector2D(_pressToContinue.GetGlobalBounds().Width / 2, _pressToContinue.GetGlobalBounds().Height);
            //SetTextOriginToMiddle(_pressToContinue);
            _pressToContinue.Position = new Vector2D(Program.windowSize.X / 2, Program.windowSize.Y - 100);

            _restart = new Text("RESTART", _font, 20);
            _restart.Origin = new Vector2D(0, _restart.GetGlobalBounds().Height);
            //SetTextOriginToMiddle(_restart);
            _restart.Position = new Vector2D(100, Program.windowSize.Y - 100);

            _exit = new Text("EXIT", _font, 20);
            _exit.Origin = new Vector2D(_exit.GetGlobalBounds().Width, _exit.GetGlobalBounds().Height);
            //SetTextOriginToMiddle(_exit);
            _exit.Position = new Vector2D(Program.windowSize.X - 100, Program.windowSize.Y - 100);


            _settingsGui = new SettingsGui(new Vector2D(Program.windowSize.X / 2 + 300, Program.windowSize.Y / 2 - 150), _font, _window);
            _difficultyGui = new DifficultyGui(new Vector2D(Program.windowSize.X / 2 - 300, Program.windowSize.Y / 2 - 150), _font);
            
            _animationManager.AddAnimation(new Animation(new Vector2D(-_rectangleLeft.Size.X / 2 - _rectangleLeft.OutlineThickness, Program.windowSize.Y / 2), new Transformable[] { _rectangleLeft, _difficultyGui }, 1000, 0, true));
            _animationManager.AddAnimation(new Animation(new Vector2D(_rectangleRight.Size.X / 2 + Program.windowSize.X + _rectangleRight.OutlineThickness, Program.windowSize.Y / 2), new Transformable[] { _rectangleRight, _settingsGui }, 1000, 400, true));
            _animationManager.AddAnimation(new Animation(new Vector2D(Program.windowSize.X / 2, Program.windowSize.Y + _pressToContinue.GetGlobalBounds().Height), new Transformable[] { _pressToContinue }, 400, 1400, true));
        }
        

        private void Input()
        {

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) || Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                if (!Program.isEscapePressed)
                {
                    Program.isEscapePressed = true;
                    Program.windowState = Program.WindowState.Game;
                }
            }
            else
            {
                Program.isEscapePressed = false;
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2D mousePosition = Mouse.GetPosition(_window);

                _settingsGui.TouchedMulitiple(mousePosition);

                if (!_isMousePressed)
                {
                    
                    _isMousePressed = true;


                    _settingsGui.TouchedOnce(mousePosition);
                    _difficultyGui.Touched(mousePosition);

                    if (Intersection(_pressToContinue.GetGlobalBounds(), mousePosition))
                    {
                        Program.windowState = Program.WindowState.Game;
                    }

                    if (Intersection(_restart.GetGlobalBounds(), mousePosition))
                    {
                        Program.Restart(true);
                    }
                    if (Intersection(_exit.GetGlobalBounds(), mousePosition))
                    {
                        _window.Close();
                    }
                }
            }
            else
            {
                _isMousePressed = false;
            }
        }

        private void Update(double elapsedTime)
        {
            _settingsGui.Update();
            _animationManager.Update(elapsedTime);
        }

        private void Redraw()
        {
            _window.Clear();
            _window.DispatchEvents();

            _window.Draw(_rectangleLeft);
            _window.Draw(_rectangleRight);

            _window.Draw(_title);
            _window.Draw(_pressToContinue);
            _window.Draw(_restart);
            _window.Draw(_exit);

            _window.Draw(_difficultyGui);
            _window.Draw(_settingsGui);

            _window.Display();
        }

        public Ai.Difficulty GetDifficultyLeft()
        {
            return _difficultyGui.GetDifficultyLeft();
        }

        public Ai.Difficulty GetDifficultyRight()
        {
            return _difficultyGui.GetDifficultyRight();
        }

        public static void SetTextOriginToMiddle(Text text)
        {
            text.Origin = new Vector2D(text.GetGlobalBounds().Width / 2, text.GetGlobalBounds().Height / 2);
        }

        public static bool Intersection(FloatRect floatRect, Vector2D position)
        {
            CircleShape mouseCircle = new CircleShape(2);
            mouseCircle.Origin = new Vector2D(1, 1);
            mouseCircle.Position = position;

            return (mouseCircle.GetGlobalBounds().Intersects(floatRect));
        }
    }
}
