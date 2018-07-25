using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class ControllerAttackScript : AttackScript
    {
        uint _id;

        public ControllerAttackScript(uint id, double attackInterval)
        {
            _id = id;
            _attackInterval = attackInterval;
        }

        public override void Update(double elapsedTime)
        {
            _attackTimer -= elapsedTime;

            if (Joystick.IsButtonPressed(_id,5))
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
