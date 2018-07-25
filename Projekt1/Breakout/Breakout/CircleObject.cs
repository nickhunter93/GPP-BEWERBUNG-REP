using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class CircleObject : IRegisterEvent
    {
        
        private double _speed = 0.5f;
        private double _additionalSpeed = 0;
        private double _additionalSpeedPerCollision = 0.1f;
        private List<SoundObserver> _observers = new List<SoundObserver>();
        private Vector2D _direction;
        private Vector2D _oldPosition;
        private CircleShape _circleShape = new CircleShape();
        private bool borderHit = true;

        public event EventHandler Play;

        public CircleShape Circle
        {
            get { return _circleShape; }
            set { _circleShape = value; }
        }

        public Vector2D GetOldPosition
        {
            get { return _oldPosition; }
        }

        public Vector2D Direction
        {
            get { return _direction; }
            set { _direction = value.Normalize(); }
        }
        public double Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public double AdditionalSpeed
        {
            get { return _additionalSpeed; }
            set { _additionalSpeed = value; }
        }

        public CircleObject(double size)
        {
            Circle.Radius = (float)size;
            Circle.Origin = new Vector2D(Circle.Position.X + Circle.Radius, Circle.Position.Y + Circle.Radius);
            Direction = new Vector2D(0, 1);
            MessageBus.RegisterEvent(this);
        }

        public bool CheckOutOfField(int windowWidth, int windowHeight)
        {
            if (Circle.Position.Y - Circle.Radius <= 0)
            {
                this.Direction = new Vector2D(this.Direction.X, -this.Direction.Y);
                Circle.Position = new Vector2D(Circle.Position.X, Circle.Radius);
                borderHit = true;
                OnPlay();
            }
            /*else if (this.Position.Y + this.Radius >= windowHeight)
            {
                this.Direction = new Vector2D(this.Direction.X, -this.Direction.Y);
                this.Position = new Vector2D(this.Position.X, windowHeight - this.Radius);
                Notify(SoundManager.SoundNumbers.HitBorder);
            }*/
            else if (Circle.Position.X - Circle.Radius <= 0)
            {
                this.Direction = new Vector2D(-this.Direction.X, this.Direction.Y);
                Circle.Position = new Vector2D(Circle.Radius, Circle.Position.Y);
                borderHit = true;
                OnPlay();
            }
            else if (Circle.Position.X + Circle.Radius >= windowWidth)
            {
                this.Direction = new Vector2D(-this.Direction.X, this.Direction.Y);
                Circle.Position = new Vector2D(windowWidth - Circle.Radius, Circle.Position.Y);
                borderHit = true;
                OnPlay();
            }

            if (Circle.Position.Y - Circle.Radius > windowHeight)
            {
                borderHit = false;
                OnPlay();
                return true;
            }

            return false;
        }

        public void Update(double elapsedTime)
        {
            _oldPosition = Circle.Position;
            Circle.Position += Direction * (_speed + _additionalSpeed) * elapsedTime;
        }

        public ConvexShape GetRectangleBoundary()
        {
            /*
            Vector2D leftSide = Circle.Position + Direction * Circle.Radius;
            Vector2D pointUpLeft = leftSide + new Vector2D(-Direction.Y, Direction.X) * Circle.Radius;
            Vector2D pointDownLeft = leftSide + new Vector2D(Direction.Y, -Direction.X) * Circle.Radius;

            Vector2D rightSide = _oldPosition - Direction * Circle.Radius;
            Vector2D pointUpRight = rightSide - new Vector2D(-Direction.Y, Direction.X) * Circle.Radius;
            Vector2D pointDownRight = rightSide - new Vector2D(Direction.Y, -Direction.X) * Circle.Radius;

            ConvexShape shape = new ConvexShape();
            shape.SetPointCount((uint)4);
            shape.SetPoint(0, pointUpLeft);
            shape.SetPoint(2, pointUpRight);
            shape.SetPoint(1, pointDownRight);
            shape.SetPoint(3, pointDownLeft);

            return shape;*/




            Vector2D pointLeft = Circle.Position + Direction * Circle.Radius;
            Vector2D pointUpLeft = Circle.Position + new Vector2D(-Direction.Y, Direction.X) * Circle.Radius;
            Vector2D pointDownLeft = Circle.Position + new Vector2D(Direction.Y, -Direction.X) * Circle.Radius;
            

            Vector2D pointRight = _oldPosition - Direction * Circle.Radius;
            Vector2D pointUpRight = _oldPosition - new Vector2D(-Direction.Y, Direction.X) * Circle.Radius;
            Vector2D pointDownRight = _oldPosition - new Vector2D(Direction.Y, -Direction.X) * Circle.Radius;

            ConvexShape shape = new ConvexShape();
            shape.SetPointCount((uint)6);
            shape.SetPoint(0, pointUpLeft);
            shape.SetPoint(2, pointUpRight);
            shape.SetPoint(1, pointRight);
            shape.SetPoint(3, pointDownRight);
            shape.SetPoint(4, pointDownLeft);
            shape.SetPoint(5, pointLeft);

            return shape;
        }

        public void ResetPosition(Vector2D newPosition)
        {
            Circle.Position = newPosition;
            _additionalSpeed = 0;
        }

        public void RePosition(Vector2D newPosition)
        {
            Circle.Position = newPosition;
            _oldPosition = Circle.Position;
        }

        public void CollisionHappened()
        {
            _additionalSpeed += _additionalSpeedPerCollision;
        }

        public void OnPlay()
        {
            if (borderHit)
            {
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.HitBorder));
            }
            else
            {
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Lose));
            }
        }
    }
}