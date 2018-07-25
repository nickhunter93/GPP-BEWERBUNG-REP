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

        public Map0()
        {
            _dataManager = DataManager.GetInstance();
            _factory = new Factory();
        }


        public override void AddChests()
        {
            _factory.CreateChest(Vector2D.One() * 500, 0, true, _dataManager.Players);
            _factory.CreateChest(Vector2D.One() * 500 + Vector2D.Down() * 50, 0, false, _dataManager.Players);
        }


        public override void AddPowerUps()
        {

            for (int i = 0; i < 10; i++)
            {
                _factory.CreatePowerUp(new Vector2D(0, 600 + 20 * i), 0, _dataManager.PrefabPowerUps);
            }

            for (int i = 0; i < 10; i++)
            {
                _factory.CreatePowerUp(new Vector2D(50, 600 + 20 * i), 1, _dataManager.PrefabPowerUps);
            }

            for (int i = 0; i < 10; i++)
            {
                _factory.CreatePowerUp(new Vector2D(100, 600 + 20 * i), 2, _dataManager.PrefabPowerUps);
            }
            
        }


        public override void AddWalls()
        {

            RectangleShape rectangleShape = new RectangleShape(Vector2D.One() * 100);
            rectangleShape.Origin = Vector2D.One() * 50;
            _factory.CreateEnvironmentObject(Vector2D.One() * -200, 0, rectangleShape);

            RectangleShape rectangleShape1 = new RectangleShape(Vector2D.One() * 100);
            rectangleShape1.Origin = Vector2D.One() * 50;
            _factory.CreateEnvironmentObject(Vector2D.One(), 0, rectangleShape1);

            RectangleShape rectangleShape2 = new RectangleShape(Vector2D.One() * 100);
            rectangleShape2.Origin = Vector2D.One() * 50;
            _factory.CreateEnvironmentObject(Vector2D.One() * 200, 0, rectangleShape2);
        }


        public override void AddEnemies()
        {
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(600, 900), 0, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet4.png"), 100, new Vector2D(700, 900), 0, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.Enemies), new ShootAi(_dataManager.Players));
        }

        public override void AddBonfires()
        {
            _factory.CreateBonfire(new Vector2D(800, 400), 1000);
            _factory.CreateBonfire(new Vector2D(1000, 400), 1000);
        }

    }
}