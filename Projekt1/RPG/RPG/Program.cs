
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
        private PopUp _popUp;
        private GameOver _gameOver;
        private PersistenceManager _persistenceManager;
        private SoundManager _soundManager;

        public static Vector2D minWindowSize = new Vector2D(1280, 800);
        public static Vector2D windowSize = new Vector2D(1280, 800);
        public static Vector2D userWindowSize = new Vector2D(1280, 800);
        private Font _font;
        private RenderWindow _window;
        public static WindowState lastWindowState;
        public static WindowState windowState;
        public static bool isEscapePressed;
        public static Program program;
        public static bool playAnimations;
        public static bool muted;
        public static bool fullscreen;
        public static int maxLifes = 5;
        public static int lifes;
        public static int soundVolume = 50;
        public static int musicVolume = 50;
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
                DataManager.GetInstance().DeleteAll();
            }
            
            if (playAnimationsNew)
            {
                reachedLevel = 1;
            }

            program = new Program();
            
            windowState = WindowState.MainMenu;
            lifes = maxLifes;
            soundVolume = 50;
            musicVolume = 50;
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
            PopUp,
            Game,
            GameOver
        }

        public void Start()
        {
            _soundManager = new SoundManager();
            MessageBus.RegisterSM(_soundManager);

            MusicManager.GetInstance().Play(MusicManager.MusicNumbers.Main);

            if (fullscreen)
                windowSize = new Vector2D(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height);
            else
                windowSize = userWindowSize;
            _window = new RenderWindow(new VideoMode((uint)windowSize.X, (uint)windowSize.Y), "RPG", fullscreen ? Styles.None : Styles.Titlebar);
            //_window = new RenderWindow(new VideoMode(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height), "BREAKOUT", Styles.None);
            _window.SetActive();

            _font = new Font("Font/sayso chic.ttf");

            _persistenceManager = new PersistenceManager();
            //_persistenceManager.LoadData();
            //_persistenceManager.SaveData();

            _mainMenu = new MainMenu(_window, _font);
            _mainMenu.Start();

            _popUp = new PopUp(_window, _font);
            _popUp.Start();
            
            _game = new Game(_window, _font);
            _game.SetPM(_persistenceManager);
            _game.Start();

            _gameOver = new GameOver(_window, _font);
            _gameOver.Start();

            playAnimations = true;
        }



        public void Run()
        {

            while (_window.IsOpen)
            {

                if (windowState == WindowState.MainMenu)
                {
                    _mainMenu.Run();
                }
                else if (windowState == WindowState.PopUp)
                {
                    _popUp.Run();
                }
                else if (windowState == WindowState.Game)
                {
                    _mainMenu.PlayerSet();
                    _game.Run(_mainMenu.GetPlayerCount());
                }
                else if (windowState == WindowState.GameOver)
                {
                    _gameOver.Run(_game.IsWon());
                }

                //lastWindowState = windowState;
                _soundManager.Update();
            }

        }
        


    }
}
