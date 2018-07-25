using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class ControllerLookScript : LookScript
    {
        private uint _id;
        private double angle;
        private double _extraRotation;

        public ControllerLookScript(uint id, double speed)
        {
            _id = id;
            angle = 0;
            ExtraRotation = 0;
            Speed = speed;
        }

        public double ExtraRotation { get => _extraRotation; set => _extraRotation = value; }

        public override void Update(double elapsedTime)
        {
            Vector2D stickPosition = new Vector2D(Joystick.GetAxisPosition(_id,Joystick.Axis.U), Joystick.GetAxisPosition(_id, Joystick.Axis.R));
            if(stickPosition.GetLength() > 30)
            {
                angle = stickPosition.GetAngleBetween(Vector2D.Up());
            }
            //angle = 90;
            //Console.Out.WriteLine(angle);
            
            //angle = -angle;

            while (angle - _parent.transform.Rotation > 180)
            {
                angle = angle - 360;
            }
            while (angle - _parent.transform.Rotation < -180)
            {
                angle = angle + 360;
            }


            if (Math.Abs(_parent.transform.Rotation - angle) < Speed * elapsedTime)
                return;


            if (_parent.transform.Rotation < angle)
                _parent.transform.Rotation += Speed * elapsedTime + ExtraRotation;
            else
                _parent.transform.Rotation -= Speed * elapsedTime + ExtraRotation;

        }
    }
}
