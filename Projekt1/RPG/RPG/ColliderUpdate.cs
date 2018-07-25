using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class ColliderUpdate : IRegisterEvent
    {
        private DataManager _dataManager;
        private Map _map;
        private AnimationManager _animationManager;

        public ColliderUpdate(Map map, AnimationManager animationManager)
        {
            _dataManager = DataManager.GetInstance();
            _map = map;
            _animationManager = animationManager;
            MessageBus.RegisterEvent(this);
        }

        public event EventHandler Play;

        public void OnPlay(String sound)
        {
            if (sound == "powerup")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Powerup));
        }

        public void CheckCollision(List<ICollider> colliderList)
        {
            List<ICollider> test = new List<ICollider>();

            foreach (ICollider item in colliderList)
            {
                if (item != null)
                {
                    test.Add(item);
                    //MyConsole.Out(((Component)item).Parent.ObjectName);
                }
            }

            for (int i = 0; i<test.Count;i++)
            {
                ICollider collider = test[i];
                for (int k = 0; k< test.Count;k++)
                {
                    ICollider colliderWith = test[k];
                    if (i != k)
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
                                        //rectangleCollider.Parent.CollisionHappened = true;
                                        //rectangleCollider.Parent.CollidedWith = rectangleColliderWith;
                                    }
                                //}
                            }
                            if (colliderWith is SphereCollider)
                            {
                                SphereCollider sphereColliderWith = (SphereCollider)colliderWith;
                                //if (rectangleCollider.Parent.transform.Position.GetDistance(sphereColliderWith.Parent.transform.Position) < (rectangleCollider.Size * 2).GetLength())
                                //{
                                    if (rectangleCollider.IsCollided(sphereColliderWith))
                                    {
                                        //rectangleCollider.Parent.CollisionHappened = true;
                                        //rectangleCollider.Parent.CollidedWith = sphereColliderWith;
                                    }
                                //}
                            }
                        }
                        if (collider is SphereCollider)
                        {
                            SphereCollider sphereCollider = (SphereCollider)collider;
                            if (colliderWith is RectangleCollider)
                            {
                                RectangleCollider rectangleColliderWith = (RectangleCollider)colliderWith;
                                //if (sphereCollider.Parent.transform.Position.GetDistance(rectangleColliderWith.Parent.transform.Position) < (sphereCollider.Radius * 4))
                                //{
                                    if (sphereCollider.IsCollided(rectangleColliderWith))
                                    {
                                        //sphereCollider.Parent.CollisionHappened = true;
                                        //sphereCollider.Parent.CollidedWith = rectangleColliderWith;
                                    }
                                //}
                            }
                            if (colliderWith is SphereCollider)
                            {
                                SphereCollider sphereColliderWith = (SphereCollider)colliderWith;
                                //if (sphereCollider.Parent.transform.Position.GetDistance(sphereColliderWith.Parent.transform.Position) < (sphereCollider.Radius * 4))
                                //{
                                    if (sphereCollider.IsCollided(sphereColliderWith))
                                    {
                                        //sphereCollider.Parent.CollisionHappened = true;
                                        //sphereCollider.Parent.CollidedWith = sphereColliderWith;
                                    }
                                //}
                            }
                        }
                    }
                }
            }
        }


        public void Collide()
        {
            List<GameObject> remove = new List<GameObject>();


            foreach (GameObject player in _dataManager.Players)
            {


                /*foreach (GameObject powerUp in _dataManager.PowerUps)
                {
                    if (player.GetComponent<RenderComponent>().Texture.GetGlobalBounds().Intersects(powerUp.GetComponent<RenderComponent>().Sprite.GetGlobalBounds()))
                    {
                        powerUp.GetScripts<PowerUpScript>()[0].ExecutePowerUp(player);
                        remove.Add(powerUp);
                        OnPlay("powerup");
                    }

                }*/
                
            }
            

            foreach (GameObject r in remove)
            {
                _dataManager.Environment.GetChilds().Remove(r);
                _dataManager.PowerUps.Remove(r);
                _dataManager.BackgroundObjects.Remove(r);
                _dataManager.DynamicObjects.Remove(r);
            }
        }

        
    }
}