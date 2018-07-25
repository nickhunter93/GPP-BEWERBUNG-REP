
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
        public static int slowMotion = 0;
        public static int reachedLevel = 1;

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
            
            if (playAnimationsNew)
            {
                reachedLevel = 1;
            }

            program = new Program();
            
            windowState = WindowState.MainMenu;
            lifes = maxLifes;
            volume = 50;
            slowMotion = 0;

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
            _window = new RenderWindow(new VideoMode((uint)windowSize.X, (uint)windowSize.Y), "SHOOT EM UP", fullscreen ? Styles.None : Styles.Titlebar);
            //_window = new RenderWindow(new VideoMode(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height), "BREAKOUT", Styles.None);
            _window.SetActive();

            _font = new Font("Font/sayso chic.ttf");

            _persistenceManager = new PersistenceManager();
            //_persistenceManager.LoadData();
            //_persistenceManager.SaveData();

            _mainMenu = new MainMenu(_window, _font);
            _mainMenu.Start();
            
            _game = new Game(_window, _font);
            _game.SetPM(_persistenceManager);
            _game.Start();

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
                    _mainMenu.PlayerSet();
                    _game.Run(_mainMenu.GetPlayerCount());
                }
                else if (windowState == WindowState.GameOver)
                {
                    _game.DrawMimic();
                    _gameOver.Run(_game.GetScore(), _game.IsWon());
                }
            }

        }
        


    }
}
