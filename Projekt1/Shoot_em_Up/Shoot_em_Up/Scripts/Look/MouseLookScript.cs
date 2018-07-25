using SFML.Graphics;
using SFML.Window;
using System;

namespace ConsoleApp2
{
    public class MouseLookScript : LookScript
    {
        private RenderWindow _window;
        private double _extraRotation;

        public double ExtraRotation { get => _extraRotation; set => _extraRotation = value; }

        public MouseLookScript(RenderWindow window, double speed)
        {
            _window = window;
            Speed = speed;
        }


        public override void Update(double elapsedTime)
        {
            Vector2D mousePosition = Mouse.GetPosition(_window);

            //double angle = -(mousePosition - (_parent.transform.Position + _parent.Parent.transform.Position)).GetAngleBetween(Vector2D.Up());
            double angle = -(mousePosition - ((_parent.transform.Position - _window.GetView().Center) + Program.windowSize/2)).GetAngleBetween(Vector2D.Up());
            //angle = 90;
            //Console.Out.WriteLine(angle);

            //_parent.transform.Rotation = -angle;
            angle = -angle;

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