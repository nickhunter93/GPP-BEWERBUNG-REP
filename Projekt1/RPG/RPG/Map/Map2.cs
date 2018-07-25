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
        private int _tileSize;

        public Map2()
        {
            _dataManager = DataManager.GetInstance();
            _factory = new Factory();
            _tileSize = (int)_dataManager.TileManager.TileSize.X;
        }


        public override void AddChests()
        {
            Vector2D middle = Vector2D.Zero();
            Vector2D middle2 = new Vector2D(_tileSize * 0, -_tileSize * 20) + middle;
            Vector2D direction = new Vector2D(_tileSize * 13, _tileSize * 13) * 1.18;
            direction = direction.Rotate(9 * (Math.PI / 180));

            for (int i = 0; i < 20; i++)
            {
                bool trap = _random.Next(0, 2) == 0 ? true : false;
                _factory.CreateChest(middle2 + direction, i * 18 - 45 + 9, trap, _dataManager.Players);
                direction = direction.Rotate(18 * (Math.PI / 180));
            }
        }


        public override void AddPowerUps()
        {

        }


        public override void AddWalls()
        {
            Vector2D vector;
            Vector2D offset = Vector2D.Zero();
            Shape shape;
            double d = Math.Sqrt(5);


            vector = new Vector2D(_tileSize * 11, _tileSize * 11);
            shape = new RectangleShape(vector);
            shape.Origin = vector / 2;
            _factory.CreateEnvironmentObject(new Vector2D(_tileSize * 0, -_tileSize * 20) + offset, 0, shape);
            

            Vector2D middle2 = new Vector2D(_tileSize * 0, -_tileSize * 20) + offset;
            Vector2D direction = new Vector2D(_tileSize * 13, _tileSize * 13);

            for (int i = 0; i < 20; i++)
            {
                CircleShape circleShape = new CircleShape(25);
                circleShape.Origin = Vector2D.One() * 25;

                vector = new Vector2D(150, 405);
                RectangleShape rectangleShape = new RectangleShape(vector);
                rectangleShape.Origin = vector / 2;

                _factory.CreateEnvironmentObject(middle2 + direction, 0, circleShape);
                direction = direction.Rotate(9 * (Math.PI / 180));
                _factory.CreateEnvironmentObject(middle2 + direction * 1.3, 18 * i + 45 + 9, rectangleShape);
                direction = direction.Rotate(9 * (Math.PI / 180));
            }
            
        }


        public override void AddEnemies()
        {
            Vector2D offset = Vector2D.Zero();
            
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet4.png"), 100, new Vector2D(-_tileSize * 11, -_tileSize * 10) + offset, 135, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.Enemies), new ShootAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet4.png"), 100, new Vector2D(-_tileSize * 9.5, -_tileSize * 11.5) + offset, 135, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.Enemies), new ShootAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet4.png"), 100, new Vector2D(-_tileSize * 8, -_tileSize * 13) + offset, 135, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.Enemies), new ShootAi(_dataManager.Players));

            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(-_tileSize * 10, -_tileSize * 7) + offset, 135, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(-_tileSize * 8.5, -_tileSize * 8.5) + offset, 135, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(-_tileSize * 7, -_tileSize * 10) + offset, 135, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));

            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet4.png"), 100, new Vector2D(-_tileSize * 4, -_tileSize * 11) + offset, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.Enemies), new ShootAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet4.png"), 100, new Vector2D(-_tileSize * 2, -_tileSize * 11) + offset, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.Enemies), new ShootAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(-_tileSize * 2, -_tileSize * 9) + offset, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));


            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet4.png"), 100, new Vector2D(_tileSize * 11, -_tileSize * 10) + offset, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.Enemies), new ShootAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet4.png"), 100, new Vector2D(_tileSize * 9.5, -_tileSize * 11.5) + offset, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.Enemies), new ShootAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet4.png"), 100, new Vector2D(_tileSize * 8, -_tileSize * 13) + offset, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.Enemies), new ShootAi(_dataManager.Players));

            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(_tileSize * 10, -_tileSize * 7) + offset, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(_tileSize * 8.5, -_tileSize * 8.5) + offset, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(_tileSize * 7, -_tileSize * 10) + offset, 225, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));

            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet4.png"), 100, new Vector2D(_tileSize * 4, -_tileSize * 11) + offset, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.Enemies), new ShootAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet4.png"), 100, new Vector2D(_tileSize * 2, -_tileSize * 11) + offset, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Crossbow(_dataManager.Enemies), new ShootAi(_dataManager.Players));
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(_tileSize * 2, -_tileSize * 9) + offset, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));
            
            _factory.CreateEnemy(new Texture("Pictures/Helmets/helmet3.png"), 100, new Vector2D(_tileSize * 0, -_tileSize * 8) + offset, 180, new EnemyMovementScript(0.1), new EnemyLookScript(0.05), new Sword(_dataManager.Enemies), new MeleeAi(_dataManager.Players));
            
        }


        public override void AddBonfires()
        {
            
        }

    }
}