using SFML.Window;
using System;

namespace ConsoleApp2
{
    public class KeyboardMovementScript : MovementScript
    {

        private Keyboard.Key _up;
        private Keyboard.Key _down;
        private Keyboard.Key _left;
        private Keyboard.Key _right;
        public Keyboard.Key _roll;


        public KeyboardMovementScript(Keyboard.Key up, Keyboard.Key down, Keyboard.Key left, Keyboard.Key right, Keyboard.Key roll)
        {
            _up = up;
            _down = down;
            _left = left;
            _right = right;
            _roll = roll;
        }

        public KeyboardMovementScript(Keyboard.Key up, Keyboard.Key down, Keyboard.Key left, Keyboard.Key right, Keyboard.Key roll, double speed) : this(up, down, left, right, roll)
        {
            Speed = speed;
        }
        

        public override void Update(double elapsedTime)
        {

            if (Keyboard.IsKeyPressed(_up))
            {
                gameObject.transform.Position = gameObject.transform.Position + Vector2D.Up() * elapsedTime * Speed;
            }

            if (Keyboard.IsKeyPressed(_down))
            {
                gameObject.transform.Position = gameObject.transform.Position + Vector2D.Down() * elapsedTime * Speed;
            }

            if (Keyboard.IsKeyPressed(_left))
            {
                gameObject.transform.Position = gameObject.transform.Position + Vector2D.Left() * elapsedTime * Speed;
            }

            if (Keyboard.IsKeyPressed(_right))
            {
                gameObject.transform.Position = gameObject.transform.Position + Vector2D.Right() * elapsedTime * Speed;
            }
            
            if (Keyboard.IsKeyPressed(_roll))
            {
                if (_parent.GetScripts<InvincibleScript>().Count > 0)
                    _parent.GetScripts<InvincibleScript>()[0].IsInvincible = true;
            }
        }

        public override void OnCollide(ICollider collider)
        {

            Console.Out.WriteLine("Collided");
            if (collider is RectangleCollider)
                Console.Out.WriteLine("Rectangle");
            if (collider is SphereCollider)
                Console.Out.WriteLine("Sphere");
        }

        public override MovementScript Clone()
        {
            return new KeyboardMovementScript(_up, _down, _left, _right, _roll, Speed);
        }
    }
}