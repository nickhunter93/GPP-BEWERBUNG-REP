using SFML.Graphics;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class DataManager
    {
        private GameObject _environment;
        private StateManager _stateManager;
        private int _playerCount = 0;
        private int _score = 0;
        private RenderWindow _window;
        private List<GameObject> _gameObjects = new List<GameObject>();
        private List<GameObject> _players = new List<GameObject>();
        private List<GameObject> _enemies = new List<GameObject>();
        private List<GameObject> _bonfires = new List<GameObject>();
        private List<GameObject> _arrows = new List<GameObject>();
        private List<GameObject> _chests = new List<GameObject>();
        private List<GameObject> _powerUps = new List<GameObject>();
        private List<PowerUpScript> _prefabPowerUps = new List<PowerUpScript>();
        private List<GameObject> _addToEnvironment = new List<GameObject>();

        public GameObject Environment { get => _environment; set => _environment = value; }
        public StateManager StateManager { get => _stateManager; set => _stateManager = value; }
        public int PlayerCount { get => _playerCount; set => _playerCount = value; }
        public int Score { get => _score; set => _score = value; }
        public RenderWindow Window { get => _window; set => _window = value; }
        public List<GameObject> GameObjects { get => _gameObjects; }
        public List<GameObject> Players { get => _players; }
        public List<GameObject> Enemies { get => _enemies; }
        public List<GameObject> Bonfires { get => _bonfires; }
        public List<GameObject> Arrows { get => _arrows; }
        public List<GameObject> Chests { get => _chests; }
        public List<GameObject> PowerUps { get => _powerUps; }
        public List<PowerUpScript> PrefabPowerUps { get => _prefabPowerUps; }
        public List<GameObject> AddToEnvironment { get => _addToEnvironment; }

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
                /*_enemies.Remove(r);
                _bonfires.Remove(r);
                _arrows.Remove(r);
                _chests.Remove(r);
                _powerUps.Remove(r);
                _addToEnvironment.Remove(r);*/
            }
            remove.Clear();

            //_environment = null;
            //_gameObjects = new List<GameObject>();
            //_players = new List<GameObject>();
            /*_enemies = new List<GameObject>();
            _bonfires = new List<GameObject>();
            _arrows = new List<GameObject>();
            _chests = new List<GameObject>();
            _powerUps = new List<GameObject>();
            _addToEnvironment = new List<GameObject>();*/

            _enemies.Clear();
            _bonfires.Clear();
            _arrows.Clear();
            _chests.Clear();
            _powerUps.Clear();
            _addToEnvironment.Clear();

        }
    }
}