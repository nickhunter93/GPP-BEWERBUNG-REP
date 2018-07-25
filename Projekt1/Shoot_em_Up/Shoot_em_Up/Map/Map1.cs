using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Map1 : Map
    {
        private DataManager _dataManager;
        private Factory _factory;
        private Random _random = new Random();

        public Map1(DataManager dataManager)
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
        }


        public override void AddPowerUps()
        {
            Vector2D middle = Program.windowSize / 2;
            //_dataManager.PowerUps.Add(_factory.CreatePowerUp(new Vector2D(50 * 0, 50 * 0) + middle, _random.Next(0, 3), _dataManager.PrefabPowerUps));
            _dataManager.PowerUps.Add(_factory.CreatePowerUp(new Vector2D(50 * 11, -50 * 2) + middle, 0, _dataManager.PrefabPowerUps));
            _dataManager.PowerUps.Add(_factory.CreatePowerUp(new Vector2D(50 * 5.5, -50 * 16) + middle, 2, _dataManager.PrefabPowerUps));
            _dataManager.PowerUps.Add(_factory.CreatePowerUp(new Vector2D(50 * 20, -50 * 37) + middle, 1, _dataManager.PrefabPowerUps));

            foreach (GameObject powerUp in _dataManager.PowerUps)
            {
                _dataManager.Environment.SetChild(powerUp);
            }
        }

        public override void AddPowerUpRuntime(Vector2D position)
        {
        }


        public override void AddWalls()
        {
            Vector2D vector;
            Vector2D middle = Program.windowSize / 2;
            Shape shape;
            double d = Math.Sqrt(2);

            vector = new Vector2D(100, 150);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(-50 * 3.5, 0) + middle, 0, shape));

            vector = new Vector2D(150, 100);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(0, 50 * 3.5) + middle, 0, shape));

            vector = new Vector2D(50 * d * 4, 50 * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(-50 * 3, 50 * 3) + middle, 45, shape));

            vector = new Vector2D(50 * d * 2, 50 * d * 9);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(-50 * 2.5, -50 * 3.5) + middle, 45, shape));

            vector = new Vector2D(50 * d * 2, 50 * d * 9);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 3.5, 50 * 2.5) + middle, 45, shape));

            vector = new Vector2D(50 * 11, 150);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 11.5, 25) + middle, 0, shape));

            vector = new Vector2D(50 * d * 9, 50 * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 13.5, -50 * 3.5) + middle, 45, shape));

            //8
            vector = new Vector2D(50 * 3, 50 * 7.5);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 9.5, -50 * 10.75) + middle, 0, shape));

            vector = new Vector2D(50 * 7, 50 * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 14, -50 * 15) + middle, 0, shape));

            vector = new Vector2D(50 * 2, 50 * 1);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 11.5, -50 * 17) + middle, 0, shape));

            vector = new Vector2D(50 * 3, 50 * 5);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 16, -50 * 19) + middle, 0, shape));

            vector = new Vector2D(50 * d * 4, 50 * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 14.5, -50 * 22.5) + middle, 45, shape));

            //13
            shape = new CircleShape(50);
            shape.Origin = Vector2D.One() * 50;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 5.5, -50 * 17.5) + middle, 0, shape));

            vector = new Vector2D(50 * 3, 50 * 6);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 1.5, -50 * 10) + middle, 0, shape));

            vector = new Vector2D(50 * d * 4.5, 50 * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(-50 * 0.25, -50 * 14.25) + middle, 45, shape));

            vector = new Vector2D(50 * 7, 50 * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(-50 * 5, -50 * 17) + middle, 0, shape));

            vector = new Vector2D(50 * d * 4, 50 * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(-50 * 9.5, -50 * 18.5) + middle, 45, shape));

            //18
            vector = new Vector2D(50 * 3, 50 * 19);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(-50 * 11, -50 * 29) + middle, 0, shape));

            vector = new Vector2D(50 * d * 2, 50 * d * 4);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(-50 * 9.5, -50 * 39.5) + middle, 45, shape));

            vector = new Vector2D(50 * 9, 50 * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(-50 * 4, -50 * 41) + middle, 0, shape));

            vector = new Vector2D(50 * d * 2, 50 * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 0.5, -50 * 41.5) + middle, 45, shape));

            vector = new Vector2D(50 * 3, 50 * 10);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 1, -50 * 46.5) + middle, 0, shape));

            //23
            vector = new Vector2D(50 * 4, 50 * 14);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(-50 * 3.5, -50 * 28.5) + middle, 0, shape));

            vector = new Vector2D(50 * 3, 50 * 5);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 0, -50 * 33) + middle, 0, shape));

            vector = new Vector2D(50 * 4, 50 * 4);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 7.5, -50 * 33.5) + middle, 0, shape));

            vector = new Vector2D(50 * 3, 50 * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 7, -50 * 30.5) + middle, 0, shape));

            vector = new Vector2D(50 * 2, 50 * 6);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 7.5, -50 * 26.5) + middle, 0, shape));

            //28
            vector = new Vector2D(50 * d * 1, 50 * d * 1);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 7.5, -50 * 23.5) + middle, 45, shape));
            
            vector = new Vector2D(50 * 1, 50 * 1);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 7, -50 * 23) + middle, 0, shape));

            vector = new Vector2D(50 * 3, 50 * 5);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 13, -50 * 26) + middle, 0, shape));

            vector = new Vector2D(50 * d * 2, 50 * d * 6);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 16.5, -50 * 30.5) + middle, 45, shape));

            shape = new CircleShape(100);     //collision!!!
            shape.Origin = Vector2D.One() * 100;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 16.5, -50 * 33.5) + middle, 0, shape));

            //33
            vector = new Vector2D(50 * 10, 50 * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 21.5, -50 * 34) + middle, 0, shape));

            vector = new Vector2D(50 * d * 7, 50 * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 24, -50 * 37) + middle, 45, shape));

            vector = new Vector2D(50 * 10, 50 * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 16.5, -50 * 40) + middle, 0, shape));

            vector = new Vector2D(50 * d * 3, 50 * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 11, -50 * 41) + middle, 45, shape));

            vector = new Vector2D(50 * 3, 50 * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 9, -50 * 42) + middle, 0, shape));

            //38
            vector = new Vector2D(50 * d * 1, 50 * d * 1);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 7.5, -50 * 41.5) + middle, 45, shape));

            vector = new Vector2D(50 * 3, 50 * 10);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 8, -50 * 46.5) + middle, 0, shape));

            vector = new Vector2D(50 * d * 2, 50 * d * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 11, -50 * 14.5) + middle, 45, shape));

        }


        public override void AddEnemies()
        {
            Vector2D middle = Program.windowSize / 2;
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 7, -50 * 7) + middle, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 5.5, -50 * 15) + middle, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 12, -50 * 22) + middle, 45, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 16, -50 * 36) + middle, 270, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 9.5, -50 * 38.5) + middle, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(-50 * 7, -50 * 37) + middle, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 4.5, -50 * 42) + middle, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Enemies), new ShootAi(_dataManager.Players)));

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
            //AddPlayer(Program.windowSize / 2 + Vector2D.Right() * 50, new ControllerMovementScript(0,0.15), new ControllerLookScript(0,0.5), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Players), new ControllerAttackScript(0,1000));

            AddPlayer(Program.windowSize / 2 + Vector2D.Right() * 50, new KeyboardMovementScript(Keyboard.Key.I, Keyboard.Key.K, Keyboard.Key.J, Keyboard.Key.L, Keyboard.Key.B, 0.15), new MouseLookScript(_dataManager.Window, 0.5), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Players), new MouseAttackScript(Mouse.Button.Left, 1000));

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
            Vector2D middle = Program.windowSize / 2;
            _dataManager.Bonfires.Add(_factory.CreateBonfire(new Vector2D(50 * 4.5, -50 * 47) + middle));
            

            foreach (GameObject bonfire in _dataManager.Bonfires)
            {
                _dataManager.Environment.SetChild(bonfire);
            }
        }

    }
}