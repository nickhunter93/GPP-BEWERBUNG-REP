using SFML.Window;

namespace ConsoleApp2
{
    internal class MouseAttackScript : AttackScript
    {
        private Mouse.Button _left;

        public MouseAttackScript(Mouse.Button left, double attackInterval)
        {
            this._left = left;
            _attackInterval = attackInterval;
        }

        public override void Update(double elapsedTime)
        {
            _attackTimer -= elapsedTime;

            if (Mouse.IsButtonPressed(_left))
            {
                if (_parent.GetScripts<WeaponScript>()[0] != null)
                {
                    if (_attackTimer < 0)
                    {
                        _parent.GetScripts<WeaponScript>()[0].Attack();
                        _attackTimer = _attackInterval;
                    }
                }
            }
        }
    }
}