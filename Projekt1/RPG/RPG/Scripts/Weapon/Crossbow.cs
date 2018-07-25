using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Crossbow : WeaponScript, IRegisterEvent
    {
        private Factory _factory;

        public Crossbow(List<GameObject> friendly)
        {
            Friendly = friendly;
            _factory = new Factory();
            MessageBus.RegisterEvent(this);
        }

        public event EventHandler Play;

        public void OnPlay(String sound)
        {
            if (sound == "crossbow")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Crossbow));
        }
        
        public override void Update(double elapsedTime)
        {
            
        }

        public override void Attack()
        {
            _factory.CreateArrow(_parent.Parent.Parent.transform.Position, _parent.Parent.Parent.transform.Rotation, _parent.Parent.Parent.GetScript<CharacterScript>().Weapon.GetComponent<RenderComponent>().Sprite.Scale / 2, Friendly);
            OnPlay("crossbow");
        }

        public override RenderComponent GetTextureComponent()
        {
            return new WeaponFactory().GetCrossbow();
        }
    }
}