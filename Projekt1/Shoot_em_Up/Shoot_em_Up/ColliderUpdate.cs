using SFML.Graphics;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class ColliderUpdate
    {
        private DataManager _dataManager;
        private Map _map;
        private AnimationManager _animationManager;

        public ColliderUpdate(DataManager dataManager, Map map, AnimationManager animationManager)
        {
            _dataManager = dataManager;
            _map = map;
            _animationManager = animationManager;
        }
        
        public void CheckCollision(List<ICollider> colliderList)
        {
            foreach (ICollider collider in colliderList)
            {
                foreach (ICollider colliderWith in colliderList)
                {
                    if (collider != null && colliderWith != null)
                    {
                        if (collider != colliderWith)
                        {
                            if (collider is RectangleCollider)
                            {
                                RectangleCollider rectangleCollider = (RectangleCollider)collider;
                                if (colliderWith is RectangleCollider)
                                {
                                    RectangleCollider rectangleColliderWith = (RectangleCollider)colliderWith;
                                    //if (rectangleCollider.Parent.transform.Position.GetDistance(rectangleColliderWith.Parent.transform.Position) < (rectangleCollider.Size * 2).GetLength())
                                    //{
                                        if (rectangleCollider.IsCollided(rectangleColliderWith))
                                        {
                                            rectangleCollider.Parent.CollisionHappened = true;
                                            rectangleCollider.Parent.CollidedWith = rectangleColliderWith;
                                        }
                                    //}
                                }
                                else if (colliderWith is SphereCollider)
                                {
                                    SphereCollider sphereColliderWith = (SphereCollider)colliderWith;
                                    //if (rectangleCollider.Parent.transform.Position.GetDistance(sphereColliderWith.Parent.transform.Position) < (rectangleCollider.Size * 2).GetLength())
                                    //{
                                        if (rectangleCollider.IsCollided(sphereColliderWith))
                                        {
                                            rectangleCollider.Parent.CollisionHappened = true;
                                            rectangleCollider.Parent.CollidedWith = sphereColliderWith;
                                        }
                                    //}
                                }
                            }
                            else if (collider is SphereCollider)
                            {
                                SphereCollider sphereCollider = (SphereCollider)collider;
                                if (colliderWith is RectangleCollider)
                                {
                                    RectangleCollider rectangleColliderWith = (RectangleCollider)colliderWith;
                                    //if (sphereCollider.Parent.transform.Position.GetDistance(rectangleColliderWith.Parent.transform.Position) < (sphereCollider.Radius * 4))
                                    //{
                                        if (sphereCollider.IsCollided(rectangleColliderWith))
                                        {
                                            sphereCollider.Parent.CollisionHappened = true;
                                            sphereCollider.Parent.CollidedWith = rectangleColliderWith;
                                        }
                                    //}
                                }
                                else if (colliderWith is SphereCollider)
                                {
                                    SphereCollider sphereColliderWith = (SphereCollider)colliderWith;
                                    //if (sphereCollider.Parent.transform.Position.GetDistance(sphereColliderWith.Parent.transform.Position) < (sphereCollider.Radius * 4))
                                    //{
                                        if (sphereCollider.IsCollided(sphereColliderWith))
                                        {
                                            sphereCollider.Parent.CollisionHappened = true;
                                            sphereCollider.Parent.CollidedWith = sphereColliderWith;
                                        }
                                    //}
                                }
                            }
                        }
                    }
                }
            }
        }


        public void Collide()
        {
            List<GameObject> remove = new List<GameObject>();
            List<GameObject> playerSwords = new List<GameObject>();
            List<GameObject> enemySwords = new List<GameObject>();


            foreach (GameObject enemy in _dataManager.Enemies)
            {
                if (enemy.GetScripts<Sword>().Count > 0)
                {
                    enemySwords.Add(enemy.GetScripts<CharacterScript>()[0].Weapon);
                }
            }


            foreach (GameObject player in _dataManager.Players)
            {
                foreach (GameObject enemySword in enemySwords)
                {
                    if (!enemySword.Parent.Parent.GetScripts<Sword>()[0].IsActive)
                        continue;

                    if (player.GetComponent<RenderComponent>().Shape.GetGlobalBounds().Intersects(enemySword.GetComponent<RenderComponent>().Sprite.GetGlobalBounds()))
                    {
                        GameOver(player);
                    }
                }


                foreach (GameObject powerUp in _dataManager.PowerUps)
                {
                    if (player.GetComponent<RenderComponent>().Shape.GetGlobalBounds().Intersects(powerUp.GetComponent<RenderComponent>().Sprite.GetGlobalBounds()))
                    {
                        powerUp.GetScripts<PowerUpScript>()[0].ExecutePowerUp(player);
                        remove.Add(powerUp);
                    }

                }


                foreach (GameObject bonfire in _dataManager.Bonfires)
                {
                    if ((bonfire.transform.Position - player.transform.Position).GetLength() < 100)
                    {
                        if (!_animationManager.ExistAnimationToTextureComponent(bonfire.GetComponent<RenderComponent>()))
                        {
                            RenderComponent textureComponent = bonfire.GetComponent<RenderComponent>();
                            _animationManager.AddAnimation(new TextureAnimation(textureComponent, new Texture[] { new Texture("Pictures/bonfire2_2.png"), new Texture("Pictures/bonfire3_2.png") }, -1, 0, 500, Vector2D.One(), Vector2D.One() * -1));
                            _animationManager.AddAnimation(new TextureAnimation(textureComponent, null, 200, 0, 0, Vector2D.One() * 4, Vector2D.One() * -1));
                            _animationManager.AddAnimation(new TextureAnimation(textureComponent, null, 200, 200, 0, Vector2D.One(), Vector2D.One() * 4));
                            _dataManager.Score += 1000;
                        }
                    }

                }

                if (player.GetScripts<Sword>().Count > 0)
                {
                    playerSwords.Add(player.GetScripts<CharacterScript>()[0].Weapon);
                }
            }




            foreach (GameObject sword in playerSwords)
            {
                if (!sword.Parent.Parent.GetScripts<Sword>()[0].IsActive)
                    continue;

                foreach (GameObject enemy in _dataManager.Enemies)
                {
                    if (sword.GetComponent<RenderComponent>().Sprite.GetGlobalBounds().Intersects(enemy.GetComponent<RenderComponent>().Shape.GetGlobalBounds()))
                    {
                        remove.Add(enemy);
                        _dataManager.Score += 30;
                    }

                }


                foreach (GameObject chest in _dataManager.Chests)
                {
                    if (sword.GetComponent<RenderComponent>().Sprite.GetGlobalBounds().Intersects(MakeSmaller(chest.GetComponent<RenderComponent>().Sprite.GetGlobalBounds(), 0.5f)))
                    {
                        remove.Add(chest);
                        _map.AddPowerUpRuntime(chest.transform.Position);
                        continue;
                    }
                }

            }



            foreach (GameObject arrow in _dataManager.Arrows)
            {

                foreach (GameObject enemy in _dataManager.Enemies)
                {
                    if (arrow.GetComponent<RenderComponent>().Sprite.GetGlobalBounds().Intersects(enemy.GetComponent<RenderComponent>().Shape.GetGlobalBounds()) && !arrow.GetScripts<ArrowScript>()[0].Friendly.Contains(enemy))
                    {
                        remove.Add(arrow);
                        remove.Add(enemy);
                        _dataManager.Score += 30;
                        continue;
                    }
                }

                foreach (GameObject player in _dataManager.Players)
                {
                    if (arrow.GetComponent<RenderComponent>().Sprite.GetGlobalBounds().Intersects(player.GetComponent<RenderComponent>().Shape.GetGlobalBounds()) && !arrow.GetScripts<ArrowScript>()[0].Friendly.Contains(player))
                    {
                        remove.Add(arrow);
                        GameOver(player);
                        continue;
                    }
                }

                foreach (GameObject chest in _dataManager.Chests)
                {
                    if (arrow.GetComponent<RenderComponent>().Sprite.GetGlobalBounds().Intersects(MakeSmaller(chest.GetComponent<RenderComponent>().Sprite.GetGlobalBounds(), 0.5f)))
                    {
                        remove.Add(arrow);
                        remove.Add(chest);
                        continue;
                    }
                }

                foreach (GameObject secondArrow in _dataManager.Arrows)
                {
                    if (arrow != secondArrow && arrow.GetComponent<RenderComponent>().Sprite.GetGlobalBounds().Intersects(secondArrow.GetComponent<RenderComponent>().Sprite.GetGlobalBounds()))
                    {
                        remove.Add(arrow);
                        remove.Add(secondArrow);
                        continue;
                    }
                }


                if (arrow.GetScripts<ArrowScript>()[0].LifeTime < 0)
                {
                    remove.Add(arrow);
                    continue;
                }
            }



            foreach (GameObject r in remove)
            {
                _dataManager.Environment.GetChilds().Remove(r);
                _dataManager.Enemies.Remove(r);
                _dataManager.PowerUps.Remove(r);
                _dataManager.Chests.Remove(r);
                _dataManager.Arrows.Remove(r);
                _dataManager.Players.Remove(r);
            }
        }



        private FloatRect MakeSmaller(FloatRect floatRect, float factor)
        {
            FloatRect newFloatRect = new FloatRect(floatRect.Left + (floatRect.Width * factor) / 2, floatRect.Top + (floatRect.Height * factor) / 2, floatRect.Width * factor, floatRect.Height * factor);

            return newFloatRect;
        }

        
        public void GameOver(GameObject player)
        {
            if (!player.GetScripts<InvincibleScript>()[0].IsInvincible)
                Program.windowState = Program.WindowState.GameOver;
        }
    }
}