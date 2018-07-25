using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Sword : WeaponScript
    {
        private double _interval = 400;
        private double _timer = 0;
        private double _speed = 0.55;

        public bool IsActive { get { return _timer > 0; } }

        public Sword(List<GameObject> friendly)
        {
            Friendly = friendly;
        }

        public override void Update(double elapsedTime)
        {
            if (_timer > 0)
                _timer -= elapsedTime;
            else
                _parent.GetChilds()[0].transform.Rotation = 0;

            if (_timer > _interval - _interval / 5)
                _parent.GetChilds()[0].transform.Rotation += elapsedTime * _speed;
            else if (_timer > _interval / 2 - _interval / 5)
                _parent.GetChilds()[0].transform.Rotation -= elapsedTime * _speed;
            else if (_timer > 0)
                _parent.GetChilds()[0].transform.Rotation += elapsedTime * _speed;
        }

        public override void Attack()
        {
            _timer = _interval;
        }

        public override RenderComponent GetTextureComponent()
        {
            return WeaponCreator.GetSword();
        }

    }
}