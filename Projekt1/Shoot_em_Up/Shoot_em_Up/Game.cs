using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp2
{
    public class Game
    {
        private const double MS_PER_UPDATE = 8;
        private double _lag;

        private Font _font;

        private PersistenceManager _persistenceManager;
        private AnimationManager _animationManager = new AnimationManager();
        private SoundManager _soundManager = new SoundManager();

        private MapManager _mapManager;
        private DataManager _dataManager;
        private ColliderUpdate _colliderUpdate;

        private Text _scoreText;
        private uint _characterSizeScore = 50;
        private int _displayedScore = 0;

        private Stopwatch _stopwatch = new Stopwatch();
        
        private String _loadedLevel;
        private int _levelID;

        private View camera;
        


        public Game(RenderWindow window, Font font)
        {
            _dataManager = new DataManager();
            _dataManager.Window = window;
            _font = font;
            MessageBus.RegisterSM(_soundManager);
            View camera = new View(new Vector2D(window.Size.X/2, window.Size.Y/2),new Vector2D(window.Size.X,window.Size.Y));
            window.SetView(camera);
        }

        public void Start()
        {
            Initialise();
        }

        public void Run(int playerCount)
        {
            if (playerCount != _dataManager.PlayerCount)
            {
                InitialisePlayer(playerCount);
            }
            
            
            double elapsedTime = (double)_stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * (1000 - Program.slowMotion);
            
            _lag += elapsedTime;
            
            _stopwatch.Restart();
                
            Input(elapsedTime);
            while (_lag >= MS_PER_UPDATE)
            {
                Update(MS_PER_UPDATE);
                _lag -= MS_PER_UPDATE;
            }
            Redraw();
                                
            _stopwatch.Stop();
            
        }


        private void InitialisePlayer(int playerCount)
        {
            _dataManager.PlayerCount = playerCount;

            _mapManager.AddPlayer();
            
        }


        private void Initialise()
        {
            _lag = 0.0;

            _mapManager = new MapManager(_dataManager);
            _mapManager.CreateMap(0);
            

            _scoreText = new Text(_dataManager.Score.ToString(), _font, _characterSizeScore);
            _scoreText.OutlineColor = Color.Black;
            _scoreText.OutlineThickness = 1;

            _colliderUpdate = new ColliderUpdate(_dataManager, _mapManager.GetMap(0), _animationManager);
        }

        private void Input(double elapsedTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                if (!Program.isEscapePressed)
                {
                    Program.isEscapePressed = true;
                    Program.windowState = Program.WindowState.MainMenu;
                }
            }
            else
            {
                Program.isEscapePressed = false;
            }
        }
        
        private void Update(double elapsedTime)
        {
            Joystick.Update();

            if (camera == null)
            {
                camera = new View(Vector2D.Zero(),Program.windowSize);
            }

            _dataManager.Window.SetView(camera);

            if (_dataManager.PlayerCount == 1)
            {
                camera.Center = _dataManager.Players[0].transform.Position;
            }
            if (_dataManager.PlayerCount == 2)
            {
                camera.Center = _dataManager.Players[1].transform.Position + (_dataManager.Players[0].transform.Position - _dataManager.Players[1].transform.Position)/2;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Z)) _dataManager.Players[1].GetScripts<AttackScript>()[0].AttackInterval = 300;
            }


            if(Program.windowState == Program.WindowState.MainMenu || Program.windowState == Program.WindowState.GameOver)
            {
                camera.Center = Program.windowSize / 2;
            }

            if (_dataManager.AddToEnvironment.Count > 0)
            {
                foreach (GameObject newEnv in _dataManager.AddToEnvironment)
                {
                    _dataManager.Environment.SetChild(newEnv);
                }
                _dataManager.AddToEnvironment.Clear();
            }


            foreach (GameObject gameobject in _dataManager.GameObjects)
            {
                gameobject.Update(elapsedTime);
            }

            
            if (_mapManager.ActiveMap == 0)
            {
                _dataManager.DeleteMap();
                _mapManager.CreateMap(1);
            }
            if (_mapManager.ActiveMap == 1)
            {
                foreach (GameObject player in _dataManager.Players)
                {
                    if (player.transform.Position.Y - Program.windowSize.Y / 2 < -50 * 49)
                    {
                        _mapManager.TeleportPlayer(new Vector2D(0, 0));
                     
                        _dataManager.DeleteMap();
                        _mapManager.CreateMap(2);
                        Program.reachedLevel = 2;
                    }
                }
            }
            if (_mapManager.ActiveMap == 2)
            {
                if (_dataManager.Enemies.Count == 0)
                {
                    Program.windowState = Program.WindowState.GameOver;
                }
            }

            if (Program.reachedLevel == 2 && _mapManager.ActiveMap < 2)
            {
                _mapManager.TeleportPlayer(new Vector2D(0, 0));

                _dataManager.DeleteMap();
                _mapManager.CreateMap(2);
            }

            

            List<ICollider> colliderList = _dataManager.Environment.GetComponentsInChilds<ICollider>();
            _colliderUpdate.CheckCollision(colliderList);
            _colliderUpdate.Collide();

            _dataManager.StateManager.Update(elapsedTime);


            int countSpeed = 20;
            
            if (_dataManager.Score >= _displayedScore)
            {
                int add = (_dataManager.Score - _displayedScore) / countSpeed;
                if (add == 0)
                    add = _dataManager.Score - _displayedScore;
                _displayedScore += add;
            }

            _scoreText.DisplayedString = _displayedScore.ToString();

            _scoreText.Position = new Vector2D(camera.Center.X + Program.windowSize.X/2 - _scoreText.GetGlobalBounds().Width - 50, camera.Center.Y + Program.windowSize.Y/2 - _scoreText.GetGlobalBounds().Height - 50);



            _animationManager.Update(elapsedTime);
        }

        public void DrawMimic()
        {
            foreach (GameObject gameObject in _dataManager.Chests)
            {
                foreach (RenderComponent renderComponent in gameObject.GetComponentsInChilds<RenderComponent>())
                {
                    if (renderComponent != null)
                    {
                        Shape shape = renderComponent.DrawShape();
                        Sprite sprite = renderComponent.DrawSprite();
                        if (shape != null)
                            _dataManager.Window.Draw(shape);
                        if (sprite != null)
                            _dataManager.Window.Draw(sprite);
                    }
                }
            }
        }

        private void Redraw()
        {
            _dataManager.Window.Clear();
            _dataManager.Window.DispatchEvents();

            
            /*foreach(RenderComponent render in _players[0].GetComponentsInChilds<RenderComponent>())
            {
                if (render != null)
                {
                    Shape shape = render.DrawShape();
                    Sprite sprite = render.DrawSprite();
                    if (shape != null)
                        _window.Draw(shape);
                    if (sprite != null)
                        _window.Draw(sprite);
                }
            }*/
            
            foreach(GameObject gameObject in _dataManager.GameObjects)
            {
                foreach (RenderComponent renderComponent in gameObject.GetComponentsInChilds<RenderComponent>())
                {
                    if (renderComponent != null)
                    {
                        Shape shape = renderComponent.DrawShape();
                        Sprite sprite = renderComponent.DrawSprite();
                        if (shape != null)
                            _dataManager.Window.Draw(shape);
                        if (sprite != null)
                            _dataManager.Window.Draw(sprite);
                    }
                }
            }

            _dataManager.Window.Draw(_scoreText);

            _dataManager.Window.Display();
        }

        public void SetPM(PersistenceManager persistenceManager)
        {
            _persistenceManager = persistenceManager;
        }

        public Text GetScore()
        {
            return _scoreText;
        }

        public bool IsWon()
        {
            if (_mapManager.ActiveMap == 2)
            {
                if (_dataManager.Enemies.Count == 0)
                {
                    return true;
                }
            }

            return false;
        }


        
    }
}
 