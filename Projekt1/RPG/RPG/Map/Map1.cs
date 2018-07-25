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
        private int _tileSize;

        public Map1()
        {
            _dataManager = DataManager.GetInstance();
            _factory = new Factory();
            _tileSize = (int)_dataManager.TileManager.TileSize.X;
        }


        public override void AddChests()
        {
        }


        public override void AddPowerUps()
        {
            Vector2D offset = Vector2D.Zero(); //Program.windowSize / 2;
            //_dataManager.PowerUps.Add(_factory.CreatePowerUp(new Vector2D(_tileSize * 0, _tileSize * 0) + middle, _random.Next(0, 3), _dataManager.PrefabPowerUps));
            _factory.CreatePowerUp(new Vector2D(_tileSize * 11, -_tileSize * 2) + offset, 0, _dataManager.PrefabPowerUps);
            _factory.CreatePowerUp(new Vector2D(_tileSize * 5.5, -_tileSize * 16) + offset, 2, _dataManager.PrefabPowerUps);
            _factory.CreatePowerUp(new Vector2D(_tileSize * 20, -_tileSize * 37) + offset, 1, _dataManager.PrefabPowerUps);
            
        }


        public override void AddWalls()
        {
            Vector2D vector;
            Vector2D offset = Vector2D.Zero();// Program.windowSize / 2;
            Shape shape;
            double d = Math.Sqrt(2);

            vector = new Vector2D(100, 150);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(-_tileSize * 3.5, 0) + offset, 0, shape);

            vector = new Vector2D(150, 100);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(0, _tileSize * 3.5) + offset, 0, shape);

            vector = new Vector2D(_tileSize * d * 4, _tileSize * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(-_tileSize * 3, _tileSize * 3) + offset, 45, shape);

            vector = new Vector2D(_tileSize * d * 2, _tileSize * d * 9);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(-_tileSize * 2.5, -_tileSize * 3.5) + offset, 45, shape);

            vector = new Vector2D(_tileSize * d * 2, _tileSize * d * 9);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 3.5, _tileSize * 2.5) + offset, 45, shape);

            vector = new Vector2D(_tileSize * 11, 150);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 11.5, 25) + offset, 0, shape);

            vector = new Vector2D(_tileSize * d * 9, _tileSize * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 13.5, -_tileSize * 3.5) + offset, 45, shape);

            //8
            vector = new Vector2D(_tileSize * 3, _tileSize * 7.5);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 9.5, -_tileSize * 10.75) + offset, 0, shape);

            vector = new Vector2D(_tileSize * 7, _tileSize * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 14, -_tileSize * 15) + offset, 0, shape);

            vector = new Vector2D(_tileSize * 2, _tileSize * 1);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 11.5, -_tileSize * 17) + offset, 0, shape);

            vector = new Vector2D(_tileSize * 3, _tileSize * 5);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 16, -_tileSize * 19) + offset, 0, shape);

            vector = new Vector2D(_tileSize * d * 4, _tileSize * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 14.5, -_tileSize * 22.5) + offset, 45, shape);

            //13
            shape = new CircleShape(_tileSize);
            shape.Origin = Vector2D.One() * _tileSize;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 5.5, -_tileSize * 17.5) + offset, 0, shape);

            vector = new Vector2D(_tileSize * 3, _tileSize * 6);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 1.5, -_tileSize * 10) + offset, 0, shape);

            vector = new Vector2D(_tileSize * d * 4.5, _tileSize * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(-_tileSize * 0.25, -_tileSize * 14.25) + offset, 45, shape);

            vector = new Vector2D(_tileSize * 7, _tileSize * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(-_tileSize * 5, -_tileSize * 17) + offset, 0, shape);

            vector = new Vector2D(_tileSize * d * 4, _tileSize * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(-_tileSize * 9.5, -_tileSize * 18.5) + offset, 45, shape);

            //18
            vector = new Vector2D(_tileSize * 3, _tileSize * 19);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(-_tileSize * 11, -_tileSize * 29) + offset, 0, shape);

            vector = new Vector2D(_tileSize * d * 2, _tileSize * d * 4);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(-_tileSize * 9.5, -_tileSize * 39.5) + offset, 45, shape);

            vector = new Vector2D(_tileSize * 9, _tileSize * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(-_tileSize * 4, -_tileSize * 41) + offset, 0, shape);

            vector = new Vector2D(_tileSize * d * 2, _tileSize * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 0.5, -_tileSize * 41.5) + offset, 45, shape);

            vector = new Vector2D(_tileSize * 3, _tileSize * 10);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 1, -_tileSize * 46.5) + offset, 0, shape);

            //23
            vector = new Vector2D(_tileSize * 4, _tileSize * 14);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(-_tileSize * 3.5, -_tileSize * 28.5) + offset, 0, shape);

            vector = new Vector2D(_tileSize * 3, _tileSize * 5);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 0, -_tileSize * 33) + offset, 0, shape);

            vector = new Vector2D(_tileSize * 4, _tileSize * 4);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 7.5, -_tileSize * 33.5) + offset, 0, shape);

            vector = new Vector2D(_tileSize * 3, _tileSize * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 7, -_tileSize * 30.5) + offset, 0, shape);

            vector = new Vector2D(_tileSize * 2, _tileSize * 6);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 7.5, -_tileSize * 26.5) + offset, 0, shape);

            //28
            vector = new Vector2D(_tileSize * d * 1, _tileSize * d * 1);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 7.5, -_tileSize * 23.5) + offset, 45, shape);
            
            vector = new Vector2D(_tileSize * 1, _tileSize * 1);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 7, -_tileSize * 23) + offset, 0, shape);

            vector = new Vector2D(_tileSize * 3, _tileSize * 5);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 13, -_tileSize * 26) + offset, 0, shape);

            vector = new Vector2D(_tileSize * d * 2, _tileSize * d * 6);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 16.5, -_tileSize * 30.5) + offset, 45, shape);

            shape = new CircleShape(100);     //collision!!!
            shape.Origin = Vector2D.One() * 100;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 16.5, -_tileSize * 33.5) + offset, 0, shape);

            //33
            vector = new Vector2D(_tileSize * 10, _tileSize * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 21.5, -_tileSize * 34) + offset, 0, shape);

            vector = new Vector2D(_tileSize * d * 7, _tileSize * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 24, -_tileSize * 37) + offset, 45, shape);

            vector = new Vector2D(_tileSize * 10, _tileSize * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 16.5, -_tileSize * 40) + offset, 0, shape);

            vector = new Vector2D(_tileSize * d * 3, _tileSize * d * 2);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 11, -_tileSize * 41) + offset, 45, shape);

            vector = new Vector2D(_tileSize * 3, _tileSize * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 9, -_tileSize * 42) + offset, 0, shape);

            //38
            vector = new Vector2D(_tileSize * d * 1, _tileSize * d * 1);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 7.5, -_tileSize * 41.5) + offset, 45, shape);

            vector = new Vector2D(_tileSize * 3, _tileSize * 10);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 8, -_tileSize * 46.5) + offset, 0, shape);

            vector = new Vector2D(_tileSize * d * 2, _tileSize * d * 3);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 11, -_tileSize * 14.5) + offset, 45, shape);

        }


        public override void AddEnemies()
        {
            Vector2D offset = Vector2D.Zero();// Program.windowSize / 2;
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(_tileSize * 6, -_tileSize * 7) + offset, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));     
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(_tileSize * 5.5, -_tileSize * 15) + offset, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(_tileSize * 12, -_tileSize * 22) + offset, 45, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(_tileSize * 16, -_tileSize * 36) + offset, 270, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(_tileSize * 9.5, -_tileSize * 38.5) + offset, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(-_tileSize * 7, -_tileSize * 37) + offset, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet4.png"), 100, new Vector2D(_tileSize * 4.5, -_tileSize * 42) + offset, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.Enemies), new ShootAi(_dataManager.Players));
            
        }
        

        public override void AddBonfires()
        {
            Vector2D offset = Vector2D.Zero();// Program.windowSize / 2;
            _factory.CreateBonfire(new Vector2D(_tileSize * 4.5, -_tileSize * 47) + offset, 1000);
        }

    }
}