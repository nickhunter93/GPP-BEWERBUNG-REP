using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Sword : WeaponScript, IRegisterEvent
    {
        private double _interval = 400;
        private double _timer = 0;
        private double _speed = 0.55;
        private Factory factory;

        private bool _hasHit = false;

        public bool IsActive { get { return _timer > 0; } }

        public double Timer { get => _timer; }

        public Sword(List<GameObject> friendly)
        {
            factory = new Factory();
            Friendly = friendly;
            MessageBus.RegisterEvent(this);
        }

        public event EventHandler Play;

        public void OnPlay(String sound)
        {
            if (sound == "sword")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Sword));
            else if (sound == "chestopen")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Chestopen));
            else if (sound == "hitobject")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Hitobject));
            else if (sound == "hitenemy")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Hitenemy));
        }

        public override void Update(double elapsedTime)
        {

            _timer -= elapsedTime;

            if (_timer <= 0)
                _parent.Parent.transform.Rotation = 0;

            if(_timer <= 0)
            {
                _hasHit = false;
            }

            if (_timer > _interval - _interval / 5)
                _parent.Parent.transform.Rotation += elapsedTime * _speed;
            else if (_timer > _interval / 2 - _interval / 5)
                _parent.Parent.transform.Rotation -= elapsedTime * _speed;
            else if (_timer > 0)
                _parent.Parent.transform.Rotation += elapsedTime * _speed;
        }

        public override void Attack()
        {
            if (_timer <= 0)
            {
                _timer = _interval;
                OnPlay("sword");
            }
        }

        public override RenderComponent GetTextureComponent()
        {
            return new WeaponFactory().GetSword();
        }

        public override void OnCollide(List<GameObject> collider)
        {
            foreach (var item in collider)
            {
                if (gameObject.Parent.Parent != item)
                {
                    /*MyConsole.Out("Test");
                    MyConsole.Out(item.ObjectName);
                    if (item.ObjectName.Equals("Enemy"))
                    {
                        MyConsole.Out("SUPER");
                        MyConsole.Out(this.gameObject.transform.WorldPosition + "");
                    }
                    if (item.ObjectName.Equals("MyPlayer")) // Enemy hits player
                    {
                        MyConsole.Out("SUPER2");
                        MyConsole.Out(this.gameObject.transform.WorldPosition + "");
                    }*/

                    if (item.ObjectName != gameObject.Parent.Parent.ObjectName)
                    {
                        Vector2D tileSize = DataManager.GetInstance().TileManager.TileSize;
                        if(item.ObjectName == "Enemy")
                        {
                            if(_timer > 0 && !_hasHit)
                            {
                                item.GetScript<CharacterScript>().Life -= 10;
                                item.GetScript<CharacterScript>().LastHit = gameObject.Parent.Parent;
                                factory.CreateBloodSplatter(item.transform.Position,gameObject.Parent.Parent.transform.Rotation,Factory.SplatterType.Sword);
                                Vector2D direction = (item.transform.Position - gameObject.Parent.Parent.transform.Position).Normalize();
                                factory.CreateBloodSplatter(item.transform.Position + direction.Orthogonal() * -tileSize, gameObject.Parent.Parent.transform.Rotation + 90, Factory.SplatterType.Arrow);



                                _hasHit = true;
                                OnPlay("hitenemy");
                            }
                        }
                        else if(item.ObjectName == "MyPlayer")
                        {
                            if (_timer > 0 && !_hasHit)
                            {
                                item.GetScript<CharacterScript>().Life -= 10;
                                if (!item.GetScript<InvincibleScript>().IsInvincible)
                                {
                                    factory.CreateBloodSplatter(item.transform.Position, gameObject.Parent.Parent.transform.Rotation, Factory.SplatterType.Sword);
                                    Vector2D direction = (item.transform.Position - gameObject.Parent.Parent.transform.Position).Normalize();
                                    factory.CreateBloodSplatter(item.transform.Position + direction.Orthogonal() * -tileSize, gameObject.Parent.Parent.transform.Rotation + 90, Factory.SplatterType.Arrow);
                                }
                                _hasHit = true;
                                OnPlay("hitenemy");
                            }
                        }
                        else if (item.ObjectName == "Chest")
                        {
                            if (_timer > 0 && !_hasHit)
                            {
                                DataManager _dataManager = DataManager.GetInstance();
                                new Factory().CreatePowerUp(item.transform.Position, new System.Random().Next(0, 3), _dataManager.PrefabPowerUps);
                                _dataManager.Chests.Remove(item);
                                _dataManager.BackgroundObjects.Remove(item);
                                _dataManager.Environment.RemoveChildLate(item);
                                _hasHit = true;
                                OnPlay("chestopen");
                            }
                        }
                    }

                }
            }
        }
    }
}