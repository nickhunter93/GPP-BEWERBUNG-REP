using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Map2 : Map
    {
        private DataManager _dataManager;
        private Factory _factory;
        private Random _random = new Random();

        public Map2(DataManager dataManager)
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
            Vector2D middle = Program.windowSize / 2;
            Vector2D middle2 = new Vector2D(50 * 0, -50 * 20) + middle;
            Vector2D direction = new Vector2D(50 * 13, 50 * 13) * 1.18;
            direction = direction.Rotate(9 * (Math.PI / 180));

            for (int i = 0; i < 20; i++)
            {
                bool trap = _random.Next(0, 2) == 0 ? true : false;
                _dataManager.Chests.Add(_factory.CreateChest(middle2 + direction, i * 18 - 45 + 9, trap, _dataManager.Players));
                direction = direction.Rotate(18 * (Math.PI / 180));
            }

            foreach (GameObject chest in _dataManager.Chests)
            {
                _dataManager.Environment.SetChild(chest);
            }
        }


        public override void AddPowerUps()
        {

        }

        public override void AddPowerUpRuntime(Vector2D position)
        {
        }


        public override void AddWalls()
        {
            Vector2D vector;
            Vector2D middle = Program.windowSize / 2;
            Shape shape;
            double d = Math.Sqrt(5);


            vector = new Vector2D(50 * 11, 50 * 11);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(new Vector2D(50 * 0, -50 * 20) + middle, 0, shape));
            

            Vector2D middle2 = new Vector2D(50 * 0, -50 * 20) + middle;
            Vector2D direction = new Vector2D(50 * 13, 50 * 13);

            for (int i = 0; i < 20; i++)
            {
                CircleShape circleShape = new CircleShape(25);
                circleShape.Origin = Vector2D.One() * 25;

                vector = new Vector2D(150, 405);
                RectangleShape rectangleShape = new RectangleShape(vector);
                rectangleShape.Origin = vector / 2;

                _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(middle2 + direction, 0, circleShape));
                direction = direction.Rotate(9 * (Math.PI / 180));
                _dataManager.Environment.SetChild(_factory.CreateEnvironmentObject(middle2 + direction * 1.3, 18 * i + 45 + 9, rectangleShape));
                direction = direction.Rotate(9 * (Math.PI / 180));
            }
            
        }


        public override void AddEnemies()
        {
            Vector2D middle = Program.windowSize / 2;
            
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(-50 * 11, -50 * 10) + middle, 135, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Enemies), new ShootAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(-50 * 9.5, -50 * 11.5) + middle, 135, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Enemies), new ShootAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(-50 * 8, -50 * 13) + middle, 135, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Enemies), new ShootAi(_dataManager.Players)));

            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(-50 * 10, -50 * 7) + middle, 135, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(-50 * 8.5, -50 * 8.5) + middle, 135, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(-50 * 7, -50 * 10) + middle, 135, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));

            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(-50 * 4, -50 * 11) + middle, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Enemies), new ShootAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(-50 * 2, -50 * 11) + middle, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Enemies), new ShootAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(-50 * 2, -50 * 9) + middle, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));


            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 11, -50 * 10) + middle, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Enemies), new ShootAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 9.5, -50 * 11.5) + middle, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Enemies), new ShootAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 8, -50 * 13) + middle, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Enemies), new ShootAi(_dataManager.Players)));

            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 10, -50 * 7) + middle, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 8.5, -50 * 8.5) + middle, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 7, -50 * 10) + middle, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));

            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 4, -50 * 11) + middle, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Enemies), new ShootAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 2, -50 * 11) + middle, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.AddToEnvironment, _dataManager.Arrows, _dataManager.Enemies), new ShootAi(_dataManager.Players)));
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 2, -50 * 9) + middle, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));
            
            _dataManager.Enemies.Add(_factory.CreateEnemy(new Vector2D(50 * 0, -50 * 8) + middle, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players)));
            
            
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