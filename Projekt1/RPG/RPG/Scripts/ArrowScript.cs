using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class ArrowScript : Script, IRegisterEvent
    {
        private List<GameObject> _friendly;
        private Vector2D _direction;
        private double _lifeTime = 10000;
        private double _speed = 1;
        private Factory factory;

        public ArrowScript(Vector2D direction, double speed, List<GameObject> friendly)
        {
            _direction = direction.Normalize();
            Speed = speed;
            Friendly = friendly;
            factory = new Factory();

            MessageBus.RegisterEvent(this);
        }

        public event EventHandler Play;

        public void OnPlay(String sound)
        {
            if (sound == "hitobject")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Hitobject));
            else if (sound == "hitenemy")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Hitenemy));
        }

        public double LifeTime { get => _lifeTime; }
        public double Speed { get => _speed; set => _speed = value; }
        public List<GameObject> Friendly { get => _friendly; set => _friendly = value; }

        public override void Update(double elapsedTime)
        {
            _parent.transform.Position += _direction * Speed * elapsedTime;
            _lifeTime -= elapsedTime;
        }

        public override void OnCollide(List<GameObject> collider)
        {
            /*if (collider.GetComponent<SphereCollider>() != null)
            {
                SphereCollider sCollider = collider.GetComponent<SphereCollider>();
                if (Friendly.Contains(sCollider.Parent))
                {
                    if (gameObject.GetComponent<RectangleCollider>() != null)
                    {
                        gameObject.GetComponent<RectangleCollider>().SetActiveTimer(200);
                    }
                    return;
                }
            }*/
            DataManager data = DataManager.GetInstance();
            foreach (var item in collider)
            {
                if (item.ObjectName != Friendly[0].ObjectName)
                {
                    if (item.ObjectName == "Enemy")
                    {
                        item.GetScript<CharacterScript>().Life -= 10;
                        item.GetScript<CharacterScript>().LastHit = DataManager.GetInstance().Players[1];
                        factory.CreateBloodSplatter(item.transform.Position,gameObject.transform.Rotation,Factory.SplatterType.Drops);
                        factory.CreateBloodSplatter(item.transform.Position + _direction * DataManager.GetInstance().TileManager.TileSize, gameObject.transform.Rotation, Factory.SplatterType.Arrow);
                        OnPlay("hitenemy");
                    }
                    else if (item.ObjectName == "MyPlayer")
                    {
                        item.GetScript<CharacterScript>().Life -= 10;
                        if (!item.GetScript<InvincibleScript>().IsInvincible)
                        {
                            factory.CreateBloodSplatter(item.transform.Position, gameObject.transform.Rotation, Factory.SplatterType.Drops);
                            factory.CreateBloodSplatter(item.transform.Position + _direction * DataManager.GetInstance().TileManager.TileSize, gameObject.transform.Rotation, Factory.SplatterType.Arrow);
                        }
                        
                        OnPlay("hitenemy");
                    }
                    else
                    {
                        OnPlay("hitobject");
                    }

                    data.Arrows.Remove(gameObject);
                    data.Environment.RemoveChildLate(gameObject);
                    data.DynamicObjects.Remove(gameObject);
                }
            }
        }
    }
}