using System;
using SFML.Window;
namespace ConsoleApp2
{
    public class ControllerMovementScript : MovementScript
    {
        private double _deadzone = 15;
        private uint _index;
        
        public ControllerMovementScript(int index,double speed)
        {
            Speed = speed;
            _index = (uint)index;
        }

        public override void Update(double elapsedTime)
        {
            Joystick.Update();
            if (Joystick.IsConnected(_index))
            {
                //Console.Out.WriteLine("Connected");
            }

            if (Joystick.GetAxisPosition(_index, Joystick.Axis.X) > _deadzone || Joystick.GetAxisPosition(_index, Joystick.Axis.X) < -_deadzone)
            {
                gameObject.transform.Position = gameObject.transform.Position + Vector2D.Right() * elapsedTime * Speed * (Joystick.GetAxisPosition(_index, Joystick.Axis.X) / 100);
            }

            if (Joystick.GetAxisPosition(_index, Joystick.Axis.Y) > _deadzone || Joystick.GetAxisPosition(_index, Joystick.Axis.Y) < -_deadzone)
            {
                gameObject.transform.Position = gameObject.transform.Position + Vector2D.Down() * elapsedTime * Speed * (Joystick.GetAxisPosition(_index, Joystick.Axis.Y) / 100);
            }

            if (Joystick.IsButtonPressed(_index, 1))
            {
                if (_parent.GetScripts<InvincibleScript>().Count > 0)
                    _parent.GetScripts<InvincibleScript>()[0].IsInvincible = true;
            }

            /*if (Joystick.IsButtonPressed(_index,0))
            {
                Console.Out.WriteLine("0 = A");
            }
            if (Joystick.IsButtonPressed(_index, 1))
            {
                Console.Out.WriteLine("1 = B");
            }
            if (Joystick.IsButtonPressed(_index, 2))
            {
                Console.Out.WriteLine("2 = X");
            }
            if (Joystick.IsButtonPressed(_index, 3))
            {
                Console.Out.WriteLine("3 = Y");
            }
            if (Joystick.IsButtonPressed(_index, 4))
            {
                Console.Out.WriteLine("4 = L1");
            }
            if (Joystick.IsButtonPressed(_index, 5))
            {
                Console.Out.WriteLine("5 = R1");
            }
            if (Joystick.IsButtonPressed(_index, 6))
            {
                Console.Out.WriteLine("6 = Select");
            }
            if (Joystick.IsButtonPressed(_index, 7))
            {
                Console.Out.WriteLine("7 = Start");
            }
            if (Joystick.IsButtonPressed(_index, 8))
            {
                Console.Out.WriteLine("8 = LeftStick");
            }
            if (Joystick.IsButtonPressed(_index, 9))
            {
                Console.Out.WriteLine("9 = RightStick");
            }*/

        }


        public override MovementScript Clone()
        {
            return new ControllerMovementScript((int)_index,Speed);
        }
    }
}