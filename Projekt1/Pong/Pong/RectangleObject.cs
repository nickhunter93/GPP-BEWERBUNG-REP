using System;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class RectangleObject : RectangleShape, ISounds
    {

        private List<SoundObserver> _observers = new List<SoundObserver>();

        public RectangleObject(Vector2f size) : base(size)
        {
            this.Origin = new Vector2f(this.Size.X / 2, this.Size.Y / 2);
        }

        public bool PowerUpCollision(PowerUp powerUp)
        {
            return this.GetGlobalBounds().Intersects(powerUp.GetGlobalBounds());
        }

        public bool Collision(CircleObject circleObject, double elapsedTime)
        {

            if (this.GetGlobalBounds().Intersects(circleObject.GetRectangleBoundary().GetGlobalBounds()))
            {
                float heightDifference = this.Position.Y - circleObject.Position.Y;

                if (heightDifference > this.Size.Y / 2)
                    heightDifference = this.Size.Y / 2;
                else if (heightDifference < -this.Size.Y / 2)
                    heightDifference = -this.Size.Y / 2;

                Vector2f newDirection;
                
                float percent = -((heightDifference * 50) / (this.Size.Y/2)) / 100;
                if(Program.windowSize.X/2 > circleObject.Position.X)
                {
                    newDirection = new Vector2f((float)Math.Cos(percent * (float)Math.PI / 2), percent * (float)Math.PI);
                    circleObject.Position = new Vector2f(this.Position.X + this.Size.X / 2 + circleObject.Radius * 1.5f, circleObject.Position.Y);
                    circleObject.Update(0);
                }
                else
                {
                    newDirection = new Vector2f(-(float)Math.Cos(percent * (float)Math.PI / 2), percent * (float)Math.PI);
                    circleObject.Position = new Vector2f(this.Position.X - this.Size.X / 2 - circleObject.Radius * 1.5f, circleObject.Position.Y);
                    circleObject.Update(0);
                }
                
                float scalar = (float)Math.Sqrt(newDirection.X * newDirection.X + newDirection.Y * newDirection.Y);
                newDirection = new Vector2f(newDirection.X / scalar, newDirection.Y / scalar);
                
                circleObject.Direction = newDirection;
                circleObject.CollisionHappened();


                Notify(SoundManager.SoundNumbers.HitPlayer);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void RectangleMovementUp(double elapsedTime)
        {
            if (this.Position.Y > 0)
                this.Position = new Vector2f(this.Position.X, this.Position.Y - (float)(1 * elapsedTime));
        }


        public void RectangleMovementDown(double elapsedTime, float windowHeight)
        {
            if (this.Position.Y < windowHeight)
                this.Position = new Vector2f(this.Position.X, this.Position.Y + (float)(1 * elapsedTime));
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