using SFML.Graphics;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class DataManager
    {
        private static DataManager _instance = null;

        private GameObject _environment;
        private StateManager _stateManager;
        private int _playerCount = 0;
        private Font _font;
        private RenderWindow _window;
        private TileManager _tileManager;
        private List<GameObject> _gameObjects = new List<GameObject>();
        private List<GameObject> _players = new List<GameObject>();
        private List<GameObject> _enemies = new List<GameObject>();
        private List<GameObject> _bonfires = new List<GameObject>();
        private List<GameObject> _arrows = new List<GameObject>();
        private List<GameObject> _chests = new List<GameObject>();
        private List<GameObject> _powerUps = new List<GameObject>();
        private List<PowerUpScript> _prefabPowerUps = new List<PowerUpScript>();

        private List<GameObject> _backgroundObjects = new List<GameObject>();
        private List<GameObject> _dynamicObjects = new List<GameObject>();

        public static DataManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataManager();
                _instance.TileManager = new TileManager();
            }
            return _instance;
        }

        public GameObject Environment { get => _environment; set => _environment = value; }
        public StateManager StateManager { get => _stateManager; set => _stateManager = value; }
        public int PlayerCount { get => _playerCount; set => _playerCount = value; }
        public Font Font { get => _font; set => _font = value; }
        public RenderWindow Window { get => _window; set => _window = value; }
        public List<GameObject> GameObjects { get => _gameObjects; }
        public List<GameObject> Players { get => _players; }
        public List<GameObject> Enemies { get => _enemies; }
        public List<GameObject> Bonfires { get => _bonfires; }
        public List<GameObject> Arrows { get => _arrows; }
        public List<GameObject> Chests { get => _chests; }
        public List<GameObject> PowerUps { get => _powerUps; }
        public List<PowerUpScript> PrefabPowerUps { get => _prefabPowerUps; }
        public List<GameObject> BackgroundObjects { get => _backgroundObjects; }
        public List<GameObject> DynamicObjects { get => _dynamicObjects; }
        public TileManager TileManager { get => _tileManager; set => _tileManager = value; }

        public void DeleteMap()
        {
            List<GameObject> remove = new List<GameObject>();
            foreach (GameObject child in _environment.GetChilds())
            {
                if (!_players.Contains(child))
                {
                    remove.Add(child);
                }
            }

            foreach (GameObject r in remove)
            {
                _environment.RemoveChild(r);
            }
            remove.Clear();
            

            _enemies.Clear();
            _bonfires.Clear();
            _arrows.Clear();
            _chests.Clear();
            _powerUps.Clear();

            _backgroundObjects.Clear();
            _dynamicObjects.Clear();

        }

        public void DeleteAll()
        {
            _instance = null;
        }
    }
}