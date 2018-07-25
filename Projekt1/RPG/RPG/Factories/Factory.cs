using SFML.Graphics;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Factory
    {
        private DataManager _dataManager;
        private WeaponFactory _weaponFactory;
        private readonly double _HELMET_SIZE = 14;
        private readonly Vector2D _POWER_UP_SIZE = new Vector2D(16,16);

        private readonly int _BLOOD_SPLATTER_TIMER = 8000;
        private readonly int _BLOOD_PUDDLE_TIMER = 25000;

        public Factory()
        {
            _dataManager = DataManager.GetInstance();
            _weaponFactory = new WeaponFactory();
        }

        public void CreateEnvironmentObject(Vector2D position, double rotation, Shape shape)
        {
            GameObject child = new GameObject(position);
            child.ObjectName = "EnviromentObject";
            child.transform.Rotation = rotation;
            if (shape != null)
            {
                child.AddComponent(new RenderComponent(shape));
                if (shape is RectangleShape)
                {
                    RectangleShape rectangle = (RectangleShape)shape;
                    child.AddComponent(new RectangleCollider(rectangle.Size, true));
                }
                if (shape is CircleShape)
                {
                    CircleShape circle = (CircleShape)shape;
                    child.AddComponent(new SphereCollider(circle.Radius, true));
                }
            }
            child.Update(0);
            _dataManager.Environment.SetChild(child);
            _dataManager.BackgroundObjects.Add(child);
        }


        public void CreatePlayer(Texture texture, double maxLife, Vector2D position, MovementScript movementScript, LookScript lookScript, WeaponScript weaponScript, AttackScript attackScript, InteractionScript interactionScript, EstusScript estusScript, ActionScript actionScript, List<GameObject> players, int playerCount)
        {
            GameObject player = new GameObject(position);
            player.ObjectName = "MyPlayer";
            /*CircleShape shape = new CircleShape(20);
            shape.Origin = new Vector2D(20, 20);
            shape.FillColor = Color.White;*/
            player.AddComponent(new RenderComponent(texture));
            player.AddComponent(new SphereCollider(_HELMET_SIZE, false));
            player.AddScript(attackScript);
            player.AddScript(interactionScript);
            player.AddScript(new InvincibleScript(playerCount));
            player.AddScript(estusScript);
            player.AddScript(actionScript);


            GameObject swordRotationLayer = new GameObject(new Vector2D(0, 0));
            swordRotationLayer.ObjectName = "RotationLayer";
            player.SetChild(swordRotationLayer);

            GameObject weapon = new GameObject(new Vector2D(25, -20));
            weapon.ObjectName = "Weapon";
            //weapon.AddComponent(weaponScript.GetTextureComponent());

            new CharacterScript(maxLife, movementScript, lookScript, weaponScript, weapon, player);
            
            swordRotationLayer.SetChild(weapon);

            movementScript.Id = players.Count;

            _dataManager.Players.Add(player);
            _dataManager.Environment.SetChild(player);
            _dataManager.DynamicObjects.Add(player);

            player.Update(0);
        }
                

        public void CreateEnemy(Texture texture, double maxLife, Vector2D position, double rotation, MovementScript movementScript, LookScript lookScript, WeaponScript weaponScript, AiScript aiScript)
        {
            GameObject enemy = new GameObject(position);
            enemy.ObjectName = "Enemy";
            enemy.transform.Rotation = rotation;
            /*CircleShape shape = new CircleShape(20);
            shape.OutlineColor = Color.White;
            shape.OutlineThickness = 2;
            shape.FillColor = Color.Black;
            shape.Origin = new Vector2D(20, 20);*/
            RenderComponent sc = new RenderComponent(texture);
            enemy.AddComponent(sc);
            SphereCollider sphereCollider = new SphereCollider(_HELMET_SIZE, false);
            enemy.AddComponent(sphereCollider);
            enemy.AddScript(aiScript);

            GameObject swordRotationLayer = new GameObject(new Vector2D(0, 0));
            swordRotationLayer.ObjectName = "EnemyRotation";
            enemy.SetChild(swordRotationLayer);

            
            GameObject weapon = new GameObject(new Vector2D(25, -20));
            weapon.ObjectName = "EnemyWeapon";

            new CharacterScript(maxLife, movementScript, lookScript, weaponScript, weapon, enemy);

            weapon.AddComponent(weaponScript.GetTextureComponent());

            swordRotationLayer.SetChild(weapon);

            enemy.Update(0);

            _dataManager.Enemies.Add(enemy);
            _dataManager.Environment.SetChild(enemy);
            _dataManager.DynamicObjects.Add(enemy);


            HealthBar healthBar = new HealthBar(enemy.transform.Position, new Vector2D(50, 5), _dataManager.Font, maxLife);

            EnemyGui enemyGui = new EnemyGui(healthBar);

            enemy.AddScript(enemyGui);
        }


        public void CreateChest(Vector2D position, double rotation, bool trap, List<GameObject> players)
        {
            GameObject chest = new GameObject(position);
            chest.ObjectName = "Chest";
            chest.transform.Rotation = rotation;

            chest.AddComponent(new RenderComponent(new Texture("Pictures/chest_idle.png")));
            chest.AddComponent(new RectangleCollider(new Vector2D(44,22),true));
            if (trap)
                chest.AddScript(new MimicAi(players));

            chest.Update(0);
            _dataManager.Chests.Add(chest);
            _dataManager.Environment.SetChild(chest);
            _dataManager.BackgroundObjects.Add(chest);
        }


        public void CreatePowerUp(Vector2D position, int prefabNumber, List<PowerUpScript> prefabPowerUps)
        {
            GameObject powerUp = new GameObject(position);
            powerUp.AddScript(prefabPowerUps[prefabNumber].Clone(powerUp));
            powerUp.AddComponent(new RectangleCollider(_POWER_UP_SIZE, true, true));
            powerUp.ObjectName = "PowerUp";

            powerUp.Update(0);

            _dataManager.PowerUps.Add(powerUp);
            _dataManager.Environment.SetChild(powerUp);
            _dataManager.BackgroundObjects.Add(powerUp);
            /*if (_random.Next(0, _probabilityOfPowerUp) == 0)
            {
                PowerUp newPowerUp = _prefabPowerUps[_random.Next(0, _prefabPowerUps.Count)].Clone();
                */
        }


        public void CreateBonfire(Vector2D position, int souls)
        {
            GameObject bonfire = new GameObject(position);
            bonfire.AddComponent(new RenderComponent(new Texture("Pictures/bonfire1_2.png")));
            bonfire.AddScript(new BonfireScript(souls));

            bonfire.Update(0);

            _dataManager.Bonfires.Add(bonfire);
            _dataManager.Environment.SetChild(bonfire);
            _dataManager.BackgroundObjects.Add(bonfire);
        }


        public void CreateArrow(Vector2D position, double rotation, Vector2D arrowScale, List<GameObject> friendly)
        {
            GameObject arrow = new GameObject(position);
            arrow.ObjectName = "ARROW";
            arrow.transform.Rotation = rotation;
            RenderComponent textureComponent = _weaponFactory.GetArrow();
            textureComponent.Sprite.Scale = arrowScale;
            arrow.AddComponent(textureComponent);
            Vector2D size = new Vector2D(textureComponent.Sprite.TextureRect.Height, textureComponent.Sprite.TextureRect.Width);
            RectangleCollider rc = new RectangleCollider(size * textureComponent.Sprite.Scale, false, true,300);
            arrow.AddComponent(rc);


            Vector2D direction = Vector2D.Up().Rotate(arrow.transform.Rotation * (System.Math.PI / 180));
            arrow.transform.Position = arrow.transform.Position + direction * 25;

            arrow.AddScript(new ArrowScript(direction, 0.5, friendly));
            arrow.transform.Position += direction.Normalize().Orthogonal() * 22;
            arrow.Update(0);
            _dataManager.Arrows.Add(arrow);
            _dataManager.Environment.SetChild(arrow);
            _dataManager.DynamicObjects.Add(arrow);
            
        }

        public enum SplatterType
        {
            Sword,
            Arrow,
            Drops
        }

        public GameObject CreateBloodSplatter(Vector2D position,double rotation, SplatterType type)
        {
            GameObject splatter = new GameObject(position);
            splatter.transform.Rotation = rotation;
            RenderComponent rc;
            switch (type)
            {
                case SplatterType.Sword:
                    rc = new RenderComponent(new Texture("TileAssets/BloodSplatter.png"));
                    break;
                case SplatterType.Arrow:
                    rc = new RenderComponent(new Texture("TileAssets/BloodArrow.png"));
                    break;
                case SplatterType.Drops:
                    rc = new RenderComponent(new Texture("TileAssets/BloodStainSmall.png"));
                    break;
                default:
                    rc = new RenderComponent(new Texture("TileAssets/BloodSplatter.png"));
                    break;
            }

            splatter.AddComponent(rc);

            rc.Sprite.Scale = Vector2D.One() * .8;

            splatter.AddScript(new BloodSplatterScript(_BLOOD_SPLATTER_TIMER));

            splatter.Update(0);

            _dataManager.Environment.SetChild(splatter);
            _dataManager.BackgroundObjects.Add(splatter);

            return splatter;
        }

        public GameObject CreateBloodPuddle(Vector2D position)
        {
            GameObject puddle = new GameObject(position);
            RenderComponent rc = new RenderComponent(new Texture("TileAssets/BloodStain.png"));
            puddle.AddComponent(rc);
            rc.Sprite.Scale = Vector2D.One() * 1.2;

            puddle.AddScript(new BloodSplatterScript(_BLOOD_PUDDLE_TIMER));

            puddle.Update(0);

            _dataManager.Environment.SetChild(puddle);
            _dataManager.BackgroundObjects.Add(puddle);

            return puddle;
        }
    }
}