using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp2
{
    public class Game : IRegisterEvent
    {
        private const double MS_PER_UPDATE = 32;
        private double _lag;

        private PersistenceManager _persistenceManager;
        private AnimationManager _animationManager = new AnimationManager();

        private MapManager _mapManager;
        private DataManager _dataManager;
        private ColliderUpdate _colliderUpdate;

        private Stopwatch _stopwatch = new Stopwatch();
        
        private String _loadedLevel;
        private int _levelID;

        private View _camera;
        private bool _firstUpdate = true;

        private int _tileSize;


        public Game(RenderWindow window, Font font)
        {
            _dataManager = DataManager.GetInstance();
            _dataManager.Window = window;
            _dataManager.Font = font;
            _tileSize = (int)_dataManager.TileManager.TileSize.X;
            View camera = new View(new Vector2D(window.Size.X/2, window.Size.Y/2),new Vector2D(window.Size.X,window.Size.Y));
            window.SetView(camera);


            MessageBus.RegisterEvent(this);
        }

        public event EventHandler Play;

        public void OnPlay(String sound)
        {
            if (sound == "menuselect")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Menuselect));
            else if (sound == "startgame")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Startgame));
        }

        public void Start()
        {
            Initialise();
        }

        public void Run(int playerCount)
        {
            if (_firstUpdate)
            {
                _firstUpdate = false;

                _mapManager = new MapManager();
                _mapManager.CreateMap(1);


                _colliderUpdate = new ColliderUpdate(_mapManager.GetMap(0), _animationManager);
            }

            if (playerCount != _dataManager.PlayerCount)
            {
                InitialisePlayer(playerCount);
            }



            double elapsedTime = (double)_stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * (1000 - Program.slowMotion);
            
            _lag += elapsedTime;
            
            _stopwatch.Restart();
                
            if (Program.windowState != Program.WindowState.GameOver)
            {
                Input(elapsedTime);
                while (_lag >= MS_PER_UPDATE)
                {
                    Update(MS_PER_UPDATE);
                    _lag -= MS_PER_UPDATE;
                }
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

        }

        private void Input(double elapsedTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                if (!Program.isEscapePressed)
                {
                    Program.isEscapePressed = true;
                    Program.windowState = Program.WindowState.MainMenu;
                    OnPlay("startgame");
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

            if (_camera == null)
            {
                _camera = new View(Vector2D.Zero(),Program.windowSize);
            }

            _dataManager.Window.SetView(_camera);

            if (_dataManager.PlayerCount == 1)
            {
                _camera.Center = _dataManager.Players[0].transform.Position;
            }
            if (_dataManager.PlayerCount == 2)
            {
                _camera.Center = _dataManager.Players[1].transform.Position + (_dataManager.Players[0].transform.Position - _dataManager.Players[1].transform.Position)/2;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Z)) _dataManager.Players[1].GetScripts<AttackScript>()[0].AttackInterval = 300;
            }


            if(Program.windowState == Program.WindowState.MainMenu || Program.windowState == Program.WindowState.GameOver)
            {
                _camera.Center = Program.windowSize / 2;
            }


            foreach (GameObject gameobject in _dataManager.GameObjects)
            {
                gameobject.Update(elapsedTime);
            }

            //MyConsole.Out((_dataManager.Players[0].transform.Position / 48).ToString());


            if (_mapManager.ActiveMap == 0)
            {
                _dataManager.DeleteMap();
                _mapManager.CreateMap(1);
            }
            if (_mapManager.ActiveMap == 1)
            {
                foreach (GameObject player in _dataManager.Players)
                {
                    if (player.transform.Position.Y < -_tileSize * 49)
                    {                     
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
                _dataManager.DeleteMap();
                _mapManager.CreateMap(2);
            }



            List<ICollider> colliderList = _dataManager.Environment.GetComponentsInChilds<ICollider>();
            _colliderUpdate.CheckCollision(colliderList);
            _colliderUpdate.Collide();

            _dataManager.StateManager.Update(elapsedTime);
            
            _animationManager.Update(elapsedTime);
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


            RenderGameObjectList(_dataManager.BackgroundObjects);
            RenderGameObjectList(_dataManager.DynamicObjects);

            foreach (GameObject enemy in _dataManager.Enemies)
            {
                foreach (GuiElement guiElement in enemy.GetScript<EnemyGui>().GetGuiElements())
                {
                    _dataManager.Window.Draw(guiElement);
                }
            }

            foreach (GameObject player in _dataManager.Players)
            {
                foreach (GuiElement guiElement in player.GetScript<PlayerGui>().GetGuiElements())
                {
                    _dataManager.Window.Draw(guiElement);
                }
            }
            _dataManager.Window.Display();
        }

        private void RenderGameObjectList(List<GameObject> gameObjects)
        {
            foreach (GameObject gameObject in gameObjects)
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

        public void SetPM(PersistenceManager persistenceManager)
        {
            _persistenceManager = persistenceManager;
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
 