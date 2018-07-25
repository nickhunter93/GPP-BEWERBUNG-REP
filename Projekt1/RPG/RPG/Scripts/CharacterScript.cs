using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class CharacterScript : Script, IRegisterEvent
    {
        private GameObject _weapon;
        private double _life;
        private readonly double _MAX_LIFE = 100;
        private int _souls = 0;
        private GameObject _lastHit = null;
        private readonly int _SOULS_AT_DEATH = 30;

        public CharacterScript(double maxLife, MovementScript movementScript, LookScript lookScript, WeaponScript weaponScript, GameObject weapon, GameObject parent)
        {
            _weapon = weapon;

            parent.AddScript(this);
            _parent.AddScript(movementScript);
            _parent.AddScript(lookScript);
            _weapon.AddScript(weaponScript);

            weapon.AddComponent(weaponScript.GetTextureComponent());

            if (!(weaponScript is Crossbow))
                weapon.AddComponent(new RectangleCollider(new Vector2D(10 * 1.2f,40 * 1.2f),false,true));

            _MAX_LIFE = maxLife;
            _life = maxLife;
            MessageBus.RegisterEvent(this);
        }

        public event EventHandler Play;

        public void OnPlay(String sound)
        {
            if (sound == "gameover")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Gameover));
            else if (sound == "die")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Die));
            else if (sound == "powerup")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Powerup));
        }

        public GameObject Weapon { get => _weapon; }
        public double Life {
            get => _life;
            set
            {
                if (_parent.GetScript<InvincibleScript>() != null)
                {
                    if (!_parent.GetScript<InvincibleScript>().IsInvincible)
                    {
                        _life = value;
                    }
                }
                else
                {
                    _life = value;
                }
                if (_life > _MAX_LIFE)
                    _life = _MAX_LIFE;
            }
        }
        public int Souls { get => _souls; set => _souls = value; }
        public GameObject LastHit { get => _lastHit; set => _lastHit = value; }

        public override void Update(double elapsedTime)
        {
            if(_life <= 0)
            {
                new Factory().CreateBloodPuddle(gameObject.transform.Position);
                if (gameObject.ObjectName == "MyPlayer")
                {
                    OnPlay("gameover");
                    Program.windowState = Program.WindowState.GameOver;
                }
                if(gameObject.ObjectName == "Enemy")
                {
                    DataManager data = DataManager.GetInstance();
                    data.Enemies.Remove(gameObject);
                    data.DynamicObjects.Remove(gameObject);
                    data.Environment.RemoveChildLate(gameObject);
                    OnPlay("die");
                    LastHit.GetScript<CharacterScript>().Souls += _SOULS_AT_DEATH;
                }
            }
        }
    }
}