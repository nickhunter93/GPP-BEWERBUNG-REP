using SFML.Graphics;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Factory
    {


        public GameObject CreateEnvironmentObject(Vector2D position, double rotation, Shape shape)
        {
            GameObject child = new GameObject(position);
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

            return child;
        }


        public GameObject CreatePlayer(Vector2D position, MovementScript movementScript, LookScript lookScript, WeaponScript weaponScript, AttackScript attackScript, List<GameObject> players, int playerCount)
        {
            GameObject player = new GameObject(position);
            CircleShape shape = new CircleShape(20);
            shape.Origin = new Vector2D(20, 20);
            shape.FillColor = Color.White;
            player.AddComponent(new RenderComponent(shape));
            player.AddComponent(new SphereCollider(20,false));
            player.AddScript(attackScript);


            GameObject swordRotationLayer = new GameObject(new Vector2D(0, 0));
            player.SetChild(swordRotationLayer);

            GameObject weapon = new GameObject(new Vector2D(25, 0));
            //weapon.AddComponent(weaponScript.GetTextureComponent());
            swordRotationLayer.SetChild(weapon);

            new CharacterScript(movementScript, lookScript, weaponScript, null, weapon, new InvincibleScript(playerCount), player);

            
            movementScript.Id = players.Count;

            return player;
        }


        public MovementScript CreateEnvironmentMovementScript(MovementScript movementScript, List<GameObject> players)
        {
            MovementScript movementScriptEnvironment = movementScript.Clone();
            movementScriptEnvironment.Speed = -movementScript.Speed;

            movementScriptEnvironment.Id = players.Count;

            return movementScriptEnvironment;
        }
        

        public GameObject CreateEnemy(Vector2D position, double rotation, MovementScript movementScript, LookScript lookScript, WeaponScript weaponScript, AiScript aiScript)
        {
            GameObject enemy = new GameObject(position);
            enemy.transform.Rotation = rotation;
            CircleShape shape = new CircleShape(20);
            shape.OutlineColor = Color.White;
            shape.OutlineThickness = 2;
            shape.FillColor = Color.Black;
            shape.Origin = new Vector2D(20, 20);
            RenderComponent sc = new RenderComponent(shape);
            enemy.AddComponent(sc);
            SphereCollider sphereCollider = new SphereCollider(shape.Radius,false);
            enemy.AddComponent(sphereCollider);

            GameObject swordRotationLayer = new GameObject(new Vector2D(0, 0));
            enemy.SetChild(swordRotationLayer);

            GameObject weapon = new GameObject(new Vector2D(25, 0));

            new CharacterScript(movementScript, lookScript, weaponScript, aiScript, weapon, null, enemy);

            weapon.AddComponent(weaponScript.GetTextureComponent());

            swordRotationLayer.SetChild(weapon);
            

            return enemy;

        }


        public GameObject CreateChest(Vector2D position, double rotation, bool trap, List<GameObject> players)
        {
            GameObject chest = new GameObject(position);
            chest.transform.Rotation = rotation;

            chest.AddComponent(new RenderComponent(new Texture("Pictures/chest_idle.png")));
            //chest.AddScript(new EnemyLookScript(200));
            if (trap)
                chest.AddScript(new MimicAi(players));
            return chest;
        }


        public GameObject CreatePowerUp(Vector2D position, int prefabNumber, List<PowerUpScript> prefabPowerUps)
        {
            GameObject powerUp = new GameObject(position);
            powerUp.AddScript(prefabPowerUps[prefabNumber].Clone(powerUp));
            
            return powerUp;
            /*if (_random.Next(0, _probabilityOfPowerUp) == 0)
            {
                PowerUp newPowerUp = _prefabPowerUps[_random.Next(0, _prefabPowerUps.Count)].Clone();
                */
        }


        public GameObject CreateBonfire(Vector2D position)
        {
            GameObject bonfire = new GameObject(position);
            bonfire.AddComponent(new RenderComponent(new Texture("Pictures/bonfire1_2.png")));

            return bonfire;
        }

    }
}