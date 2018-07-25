
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class MapManager
    {
        private DataManager _dataManager;
        private List<Map> _maps = new List<Map>();
        private int _activeMap;
        private PersistenceManager _persistenceManager = new PersistenceManager();

        public MapManager()
        {
            _dataManager = DataManager.GetInstance();
            CreateEnvironment();
            InitialiseStates();

            _maps.Add(new Map0());
            _maps.Add(new Map1());
            _maps.Add(new Map2());
        }

        public int ActiveMap { get => _activeMap; set => _activeMap = value; }

        public void CreateMap(int i)
        {
            if (i == 0)
            {
                ActiveMap = 0;
                _persistenceManager.LoadLevel("level0");
                MusicManager.GetInstance().Play(MusicManager.MusicNumbers.Level1);
            }
            if (i == 1)
            {
                ActiveMap = 1;
                _persistenceManager.LoadLevel("level1");
                MusicManager.GetInstance().Play(MusicManager.MusicNumbers.Level1);
            }
            if (i == 2)
            {
                ActiveMap = 2;
                _persistenceManager.LoadLevel("level2");
                MusicManager.GetInstance().Play(MusicManager.MusicNumbers.Level2);
                RecreatePlayer();
            }

            _maps[i].AddBonfires();
            _maps[i].AddChests();
            //_maps[i].AddWalls();
            _maps[i].AddPowerUps();
            _maps[i].AddEnemies();
        }

        private void RecreatePlayer()
        {
            _dataManager.Players[0].transform.Position = Vector2D.Zero();
            if (_dataManager.PlayerCount > 1)
            {
                _dataManager.Players[0].transform.Position = -Vector2D.Right() * 50;
                _dataManager.Players[1].transform.Position = Vector2D.Right() * 50;
            }

                if (!_dataManager.DynamicObjects.Contains(_dataManager.Players[0]))
            {
                _dataManager.DynamicObjects.Add(_dataManager.Players[0]);
                if (_dataManager.PlayerCount > 1)
                    _dataManager.DynamicObjects.Add(_dataManager.Players[1]);
            }
        }

        public Map GetMap(int i)
        {
            return _maps[i];
        }

        public void AddPlayer()
        {

            if (_dataManager.PlayerCount >= 1)
            {
                double maxLife = 100;
                AddFirstPlayer(maxLife);
                CreatePlayerOneGui(maxLife);
            }
            if (_dataManager.PlayerCount >= 2)
            {
                _dataManager.Players[0].transform.Position = - Vector2D.Right() * 50;

                double maxLife = 100;
                AddSecondPlayer(maxLife);
                CreatePlayerTwoGui(maxLife);

                List<MovementScript> playerMovementScripts = _dataManager.Environment.GetScripts<MovementScript>();  //slower env because 2 players

                foreach (MovementScript script in playerMovementScripts)
                {
                    script.Speed = script.Speed / (playerMovementScripts.Count);
                }

            }

            _dataManager.StateManager.Players = _dataManager.Players;
        }
        

        public void AddFirstPlayer(double maxLife)
        {

            Texture texture = new Texture("Pictures/Helmets/helmet1.png");
            Vector2D position = Vector2D.Zero();
            //MovementScript movementScript = new KeyboardMovementScript(Keyboard.Key.Up, Keyboard.Key.Down, Keyboard.Key.Left, Keyboard.Key.Right, Keyboard.Key.RControl, 0.15);
            MovementScript movementScript = new KeyboardMovementScript(Keyboard.Key.W, Keyboard.Key.S, Keyboard.Key.A, Keyboard.Key.D, Keyboard.Key.Space, 0.15);
            LookScript lookScript = new MouseLookScript(_dataManager.Window, 0.5);
            WeaponScript weapon = new Sword(_dataManager.Players);
            AttackScript attackScript = new MouseAttackScript(Mouse.Button.Left, 1000);
            Keyboard.Key[] keys = new Keyboard.Key[] { Keyboard.Key.E, Keyboard.Key.Q };
            double[] intervals = new double[] { 100, 100 };
            InteractionScript interactionScript = new KeyboardInteractionScript(keys, intervals);
            EstusScript estusScript = new EstusScript(3, 30);
            ActionScript actionScript = new ActionScript(60);
            
            new Factory().CreatePlayer(texture, maxLife, position, movementScript, lookScript, weapon, attackScript, interactionScript, estusScript, actionScript, _dataManager.Players, _dataManager.PlayerCount);
            
        }

        public void AddSecondPlayer(double maxLife)
        {
            Texture texture = new Texture("Pictures/Helmets/helmet2.png");
            Vector2D position = Vector2D.Right() * 50;
            MovementScript movementScript = new ControllerMovementScript(0, 0.15);
            LookScript lookScript = new ControllerLookScript(0, 0.5);
            WeaponScript weapon = new Crossbow(_dataManager.Players);
            AttackScript attackScript = new ControllerAttackScript(0, 5, 1000);
            uint[] keys = new uint[] { 0, 2 };
            double[] intervals = new double[] { 100, 100 };
            InteractionScript interactionScript = new ControllerInteractionScript(0, keys, intervals);
            EstusScript estusScript = new EstusScript(3, 30);
            ActionScript actionScript = new ActionScript(60);

            //MovementScript movementScript = new KeyboardMovementScript(Keyboard.Key.I, Keyboard.Key.K, Keyboard.Key.J, Keyboard.Key.L, Keyboard.Key.B, 0.15)
            //LookScript lookScript = new MouseLookScript(_dataManager.Window, 0.5);
            //AttackScript attackScript = new MouseAttackScript(Mouse.Button.Left, 1000);
            //Keyboard.Key[] keys = new Keyboard.Key[] { Keyboard.Key.E, Keyboard.Key.Q };
            //InteractionScript interactionScript = new KeyboardInteractionScript(keys, intervals);


            new Factory().CreatePlayer(texture, maxLife, position, movementScript, lookScript, weapon, attackScript, interactionScript, estusScript, actionScript, _dataManager.Players, _dataManager.PlayerCount);

        }

        
        private void InitialiseStates()
        {
            List<State> states = new List<State>();
            states.Add(new Disco());
            states.Add(new SpeedDown());
            states.Add(new BiggerRectangleObject());

            _dataManager.StateManager = new StateManager(states, _dataManager.Players);

            for (int i = 0; i < states.Count; i++)
            {
                states[i].ID = i;

                PowerUpScript powerUp = new PowerUpScript(states[i]);
                powerUp.StateManager = _dataManager.StateManager;

                _dataManager.PrefabPowerUps.Add(powerUp);
            }
        }


        private void CreateEnvironment()
        {
            _dataManager.Environment = new GameObject();
            _dataManager.GameObjects.Add(_dataManager.Environment);
        }


        public void CreatePlayerOneGui(double maxLife)
        {
            Vector2D border = new Vector2D(20, -20);
            Checkbox background = new Checkbox(new Vector2D(100, Program.windowSize.Y - 40) + border, new Vector2D(250, 180), _dataManager.Font, false, false, true);
            HealthBar healthBar = new HealthBar(new Vector2D(0, Program.windowSize.Y - 10) + border, new Vector2D(200, 10), _dataManager.Font, maxLife);
            SimpleText souls = new SimpleText(new Vector2D(200, Program.windowSize.Y - 62) + border, Vector2D.Zero(), _dataManager.Font, "0", 40, false);
            souls.Text.OutlineColor = Color.Black;
            souls.Text.OutlineThickness = 1;
            TextureGui helmet = new TextureGui(new Vector2D(100-48/2, Program.windowSize.Y - 120) + border, Vector2D.Zero(), _dataManager.Font, new Texture("Pictures/Helmets/helmet1.png"), false);
            TextureGui estus = new TextureGui(new Vector2D(-9, Program.windowSize.Y - 70) + border, Vector2D.Zero(), _dataManager.Font, new Texture("Pictures/Estus/estus3.png"), false);
            SimpleText estusCount = new SimpleText(new Vector2D(40, Program.windowSize.Y - 40) + border, Vector2D.Zero(), _dataManager.Font, "0", 20, false);
            estusCount.Text.OutlineColor = Color.Black;
            estusCount.Text.OutlineThickness = 1;

            PlayerGui playerGui = new PlayerGui(healthBar, souls, helmet, estus, background, estusCount);
            
            _dataManager.Players[0].AddScript(playerGui);
        }


        public void CreatePlayerTwoGui(double maxLife)
        {
            Vector2D border = new Vector2D(Program.windowSize.X - 220, -20);
            Checkbox background = new Checkbox(new Vector2D(100, Program.windowSize.Y - 40) + border, new Vector2D(250, 180), _dataManager.Font, false, false, true);
            HealthBar healthBar = new HealthBar(new Vector2D(0, Program.windowSize.Y - 10) + border, new Vector2D(200, 10), _dataManager.Font, maxLife);
            SimpleText souls = new SimpleText(new Vector2D(200, Program.windowSize.Y - 62) + border, Vector2D.Zero(), _dataManager.Font, "0", 40, false);
            souls.Text.OutlineColor = Color.Black;
            souls.Text.OutlineThickness = 1;
            TextureGui helmet = new TextureGui(new Vector2D(100 - 48 / 2, Program.windowSize.Y - 120) + border, Vector2D.Zero(), _dataManager.Font, new Texture("Pictures/Helmets/helmet2.png"), false);
            TextureGui estus = new TextureGui(new Vector2D(-9, Program.windowSize.Y - 70) + border, Vector2D.Zero(), _dataManager.Font, new Texture("Pictures/Estus/estus3.png"), false);
            SimpleText estusCount = new SimpleText(new Vector2D(40, Program.windowSize.Y - 40) + border, Vector2D.Zero(), _dataManager.Font, "0", 20, false);
            estusCount.Text.OutlineColor = Color.Black;
            estusCount.Text.OutlineThickness = 1;

            PlayerGui playerGui = new PlayerGui(healthBar, souls, helmet, estus, background, estusCount);
            
            _dataManager.Players[1].AddScript(playerGui);
        }
    }
}