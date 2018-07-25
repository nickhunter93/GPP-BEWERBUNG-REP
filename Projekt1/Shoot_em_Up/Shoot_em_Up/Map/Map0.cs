using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Map0 : Map
    {
        private DataManager _dataManager;
        private Factory _factory;
        private Random _random = new Random();

        public Map0(DataManager dataManager)
        {
            _dataManager = dataManager;
            _factory = new Factory();
        }


        public override void InitialiseStates()
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


        public override void CreateEnvironment()
        {
            _dataManager.Environment = new GameObject();
            _dataManager.GameObjects.Add(_dataManager.Environment);
        }


        public override void AddChests()
        {
            _dataManager.Chests.Add(_factory.CreateChest(Vector2D.One() * 500, 0, true, _dataManager.Players));
            _dataManager.Chests.Add(_factory.CreateChest(Vector2D.One() * 500 + Vector2D.Down() * 50, 0, false, _dataManager.Players));

            foreach (GameObject chest in _dataManager.Chests)
            {
                _dataManager.Environment.SetChild(chest);
            }
        }


        public override void AddPowerUps()
        {

            for (int i = 0; i < 10; i++)
            {
                _dataManager.PowerUps.Add(_factory.CreatePowerUp(new Vector2D(0, 600 + 20 * i), 0, _dataManager.PrefabPowerUps));
            }

            for (int i = 0; i < 10; i++)
            {
                _dataManager.PowerUps.Add(_factory.CreatePowerUp(new Vector2D(50, 600 + 20 * i), 1, _dataManager.PrefabPowerUps));
            }

            for (int i = 0; i < 10; i++)
            {
                _dataManager.PowerUps.Add(_factory.CreatePowerUp(new Vector2D(100, 600 + 20 * i), 2, _dataManager.PrefabPowerUps));
            }

            foreach (GameObject powerUp in _dataManager.PowerUps)
            {
                _dataManager.Environment.SetChild(powerUp);
            }
        }
        
        public override void AddPowerUpRuntime(Vector2D position)
        {
            GameObject powerUp = _factory.CreatePowerUp(position, _random.Next(0, 3), _dataManager.PrefabPowerUps);

            _dataManager.PowerUps.Add(powerUp);
            _dataManager.Environment.SetChild(powerUp);
        }


        public override void AddWalls()
        {

            RectangleShape rectangleShape = new RectangleShape(Vector2D.One() * 100);
            rectangleShape.Origin = Vector2D.One() * 50;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(Vector2D.One() * -200, 0, rectangleShape));

            RectangleShape rectangleShape1 = new RectangleShape(Vector2D.One() * 100);
            rectangleShape1.Origin = Vector2D.One() * 50;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(Vector2D.One(), 0, rectangleShape1));

            RectangleShape rectangleShape2 = new RectangleShape(Vector2D.One() * 100);
            rectangleShape2.Origin = Vector2D.One() * 50;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(Vector2D.One() * 200, 0, rectangleShape2));
        }


        public override void AddEnemies()
        {
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(600, 900), 0, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(700, 900), 0, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Enemies), new ShootAi(_dataManager.Players)));

            foreach (GameObject enemy in _dataManager.Enemies)
            {
                _dataManager.Environment.SetChild(enemy);
            }
        }

        public override void AddFirstPlayer()
        {
            //AddPlayer(Program.windowSize / 2, new KeyboardMovementScript(Keyboard.Key.Up, Keyboard.Key.Down, Keyboard.Key.Left, Keyboard.Key.Right, Keyboard.Key.RControl, 0.15), new MouseLookScript(_dataManager.Window, 0.5), new Sword(_dataManager.Players), new MouseAttackScript(Mouse.Button.Left, 1000));       //for teamviewer

            AddPlayer(Program.windowSize / 2, new KeyboardMovementScript(Keyboard.Key.W, Keyboard.Key.S, Keyboard.Key.A, Keyboard.Key.D, Keyboard.Key.Space, 0.15), new MouseLookScript(_dataManager.Window, 0.5), new Sword(_dataManager.Players), new MouseAttackScript(Mouse.Button.Left, 1000));
        }

        public override void AddSecondPlayer()
        {
            AddPlayer(Program.windowSize / 2 + Vector2D.Right() * 50, new ControllerMovementScript(0,0.15), new ControllerLookScript(0,0.5), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Players), new ControllerAttackScript(0,1000));

            //AddPlayer(Program.windowSize / 2 + Vector2D.Right() * 50, new KeyboardMovementScript(Keyboard.Key.I, Keyboard.Key.K, Keyboard.Key.J, Keyboard.Key.L, Keyboard.Key.B, 0.15), new MouseLookScript(_dataManager.Window, 0.5), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Players), new MouseAttackScript(Mouse.Button.Left, 1000));

        }

        public override void AddPlayer(Vector2D position, MovementScript movementScript, LookScript lookScript, WeaponScript weaponScript, AttackScript attackScript)
        {
            GameObject player = _factory.CreatePlayer(position, movementScript, lookScript, weaponScript, attackScript, _dataManager.Players, _dataManager.PlayerCount);

            //_dataManager.Environment.AddScript(_factory.CreateEnvironmentMovementScript(movementScript, _dataManager.Players));
            _dataManager.Players.Add(player);
            _dataManager.Environment.SetChild(player);
        }


        public override void AddBonfires()
        {
            _dataManager.Bonfires.Add(_factory.CreateBonfire(new Vector2D(800, 400)));
            
            _dataManager.Bonfires.Add(_factory.CreateBonfire(new Vector2D(1000, 400)));

            foreach (GameObject bonfire in _dataManager.Bonfires)
            {
                _dataManager.Environment.SetChild(bonfire);
            }
        }

    }
}