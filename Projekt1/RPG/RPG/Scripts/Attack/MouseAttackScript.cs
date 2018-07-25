using SFML.Window;

namespace ConsoleApp2
{
    public class MouseAttackScript : AttackScript
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
                if (_parent.GetScriptsInChilds<WeaponScript>().Count != 0)
                {
                    if (_attackTimer < 0)
                    {
                        _parent.GetScriptsInChilds<WeaponScript>()[0].Attack();
                        _attackTimer = _attackInterval;
                    }
                }
            }
        }
    }
}