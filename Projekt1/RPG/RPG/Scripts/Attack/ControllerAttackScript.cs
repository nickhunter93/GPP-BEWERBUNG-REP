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
        private uint _id;
        private uint _attack;

        public ControllerAttackScript(uint id, uint attack, double attackInterval)
        {
            _id = id;
            _attack = attack;
            _attackInterval = attackInterval;
        }

        public override void Update(double elapsedTime)
        {
            _attackTimer -= elapsedTime;

            if (Joystick.IsButtonPressed(_id, _attack))
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
