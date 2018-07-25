using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class CircleObject : CircleShape , ISounds
    {
        
        private float _speed = 0.5f;
        private float _additionalSpeed = 0;
        private float _additionalSpeedPerCollision = 0.1f;
        private List<SoundObserver> _observers = new List<SoundObserver>();
        private Vector2f _direction;
        private Vector2f _oldPosition;


        public Vector2f Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public float AdditionalSpeed
        {
            get { return _additionalSpeed; }
            set { _additionalSpeed = value; }
        }

        public CircleObject(float size) : base(size)
        {
            this.Origin = new Vector2f(this.Position.X + this.Radius, this.Position.Y + this.Radius);
            _direction = new Vector2f(-1, 0);
        }

        public bool CheckOutOfField(int windowWidth, int windowHeight)
        {
            if (this.Position.Y - this.Radius <= 0)
            {
                this.Direction = new Vector2f(this.Direction.X, -this.Direction.Y);
                this.Position = new Vector2f(this.Position.X, this.Radius);
                Notify(SoundManager.SoundNumbers.HitBorder);
            }
            else if (this.Position.Y + this.Radius >= windowHeight)
            {
                this.Direction = new Vector2f(this.Direction.X, -this.Direction.Y);
                this.Position = new Vector2f(this.Position.X, windowHeight - this.Radius);
                Notify(SoundManager.SoundNumbers.HitBorder);
            }

            if (this.Position.X - this.Radius < 0 || this.Position.X + this.Radius > windowWidth)
            {
                Notify(SoundManager.SoundNumbers.Lose);
                return true;
            }

            return false;
        }

        public void Update(double elapsedTime)
        {
            _oldPosition = this.Position;
            this.Position += _direction * (_speed + _additionalSpeed) * (float)elapsedTime;
        }

        public ConvexShape GetRectangleBoundary()
        {
            Vector2f leftSide = this.Position + _direction * Radius;
            Vector2f pointUpLeft = leftSide + new Vector2f(-_direction.Y, _direction.X) * Radius;
            Vector2f pointDownLeft = leftSide + new Vector2f(_direction.Y, -_direction.X) * Radius;

            Vector2f rightSide = _oldPosition - _direction * Radius;
            Vector2f pointUpRight = rightSide - new Vector2f(-_direction.Y, _direction.X) * Radius;
            Vector2f pointDownRight = rightSide - new Vector2f(_direction.Y, -_direction.X) * Radius;

            ConvexShape shape = new ConvexShape();
            shape.SetPointCount((uint)4);
            shape.SetPoint(0, pointUpLeft);
            shape.SetPoint(2, pointUpRight);
            shape.SetPoint(1, pointDownRight);
            shape.SetPoint(3, pointDownLeft);

            return shape;
        }

        public void ResetPosition(Vector2f newPosition)
        {
            this.Position = newPosition;
            _additionalSpeed = 0;
        }

        public void CollisionHappened()
        {
            _additionalSpeed += _additionalSpeedPerCollision;
        }

        public void Notify(SoundManager.SoundNumbers i)
        {
            foreach (SoundObserver observer in _observers)
            {
                observer.Play(i);
            }
        }
        
        public void Attach(SoundObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(SoundObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}