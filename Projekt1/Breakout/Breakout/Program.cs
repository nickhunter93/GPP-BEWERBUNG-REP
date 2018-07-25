
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Threading;

namespace ConsoleApp2
{
    public class Program
    {
        private Game _game;
        private MainMenu _mainMenu;
        private GameOver _gameOver;
        private PersistenceManager _persistenceManager;

        public static Vector2D minWindowSize = new Vector2D(1280, 800);
        public static Vector2D windowSize = new Vector2D(1280, 800);
        public static Vector2D userWindowSize = new Vector2D(1280, 800);
        private Font _font;
        private RenderWindow _window;
        public static WindowState windowState;
        public static bool isEscapePressed;
        public static Program program;
        public static bool playAnimations;
        public static bool muted;
        public static bool fullscreen;
        public static int maxLifes = 5;
        public static int lifes;
        public static int volume = 50;

        static void Main(string[] args)
        {
            Restart(true);
        }

        public static void Restart(bool playAnimationsNew)
        {
            if (program != null)
            {
                program._window.Close();
            }

            program = new Program();
            
            windowState = WindowState.MainMenu;
            lifes = maxLifes;
            volume = 50;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) || Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                isEscapePressed = true;
            else
                isEscapePressed = false;

            playAnimations = playAnimationsNew;

            program.Start();
            program.Run();
            
        }

        public enum WindowState
        {
            MainMenu,
            Game,
            GameOver
        }

        public void Start()
        {
            if (fullscreen)
                windowSize = new Vector2D(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height);
            else
                windowSize = userWindowSize;
            _window = new RenderWindow(new VideoMode((uint)windowSize.X, (uint)windowSize.Y), "BREAKOUT", fullscreen ? Styles.None : Styles.Titlebar);
            //_window = new RenderWindow(new VideoMode(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height), "BREAKOUT", Styles.None);
            _window.SetActive();

            _font = new Font("sayso chic.ttf");

            _persistenceManager = new PersistenceManager();
            //_persistenceManager.LoadData();
            //_persistenceManager.SaveData();

            _mainMenu = new MainMenu(_window, _font);
            _mainMenu.Start();
            
            _game = new Game(_window, _font);
            _game.SetPM(_persistenceManager);
            _game.Start(_mainMenu.GetDifficultyLeft(), _mainMenu.GetDifficultyRight());

            _gameOver = new GameOver(_window, _font);
            _gameOver.Start();

            playAnimations = true;
        }



        public void Run()
        {

            //_window.SetFramerateLimit(60);

            while (_window.IsOpen)
            {
                //Thread.Sleep(1);
                if (windowState == WindowState.MainMenu)
                {
                    _mainMenu.Run();
                }
                else if (windowState == WindowState.Game)
                {
                    _game.Run(_mainMenu.GetDifficultyLeft(), _mainMenu.GetDifficultyRight());
                }
                else if (windowState == WindowState.GameOver)
                {
                    _gameOver.Run(_game.GetScores());
                }
            }

        }
        


    }
}
