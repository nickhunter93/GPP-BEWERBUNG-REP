using System;

namespace ConsoleApp2
{
    public class EnemyLookScript : LookScript
    {
        private Vector2D _targetPosition = Vector2D.One();

        public Vector2D TargetPosition { get => _targetPosition; set { _targetPosition = value; IsActive = true; } }

        public EnemyLookScript(double speed)
        {
            Speed = speed;
            IsActive = false;
        }

        public override void Update(double elapsedTime)
        {
            double angle = (TargetPosition - (_parent.transform.Position + _parent.Parent.transform.Position)).GetAngleBetween(Vector2D.Up());

            /*if (_parent.transform.Position.Y + _parent.Parent.transform.Position.Y < TargetPosition.Y)
            {
                angle = 180 - angle;
            }*/

            while (angle - _parent.transform.Rotation > 180)
            {
                angle = angle - 360;
            }
            while (angle - _parent.transform.Rotation < -180)
            {
                angle = angle + 360;
            }
            
            if (Math.Abs(_parent.transform.Rotation - angle) < Speed * elapsedTime)
            {
                _turning = false;
                return;
            }

            _turning = true;


            if (_parent.transform.Rotation < angle)
                _parent.transform.Rotation += Speed * elapsedTime;
            else
                _parent.transform.Rotation -= Speed * elapsedTime;
        }

        
    }
}