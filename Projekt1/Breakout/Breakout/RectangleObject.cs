using System;
using SFML.Graphics;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class RectangleObject : IRegisterEvent
    {

        private List<SoundObserver> _observers = new List<SoundObserver>();
        private RectangleShape _rectangleShape = new RectangleShape();
        public static List<RectangleShape> rectangleShapePrefabs;
        private bool _hitPlayer = true;
        private int _id = 0;
        private int _life = 1;

        public RectangleShape Rectangle
        {
            get { return _rectangleShape; }
            set { _rectangleShape = value; }
        }

        public int ID { get => _id; set => _id = value; }
        public int Life { get => _life; set => _life = value; }

        public RectangleObject(Vector2D size)
        {
            Initialise(size);
        }

        public RectangleObject(Vector2D size, RectangleShape prefab)
        {
            Initialise(size);

            ChangeToPrefab(prefab);
        }

        public RectangleObject(Vector2D size, int prefabNumber)
        {
            Initialise(size);

            ChangeToPrefab(rectangleShapePrefabs[prefabNumber]);
        }

        private void Initialise(Vector2D size)
        {

            if (rectangleShapePrefabs == null)
            {
                SetUpRectangleShapePrefabs();
            }

            Rectangle.Size = size;
            Rectangle.Origin = new Vector2D(Rectangle.Size.X / 2, Rectangle.Size.Y / 2);
            MessageBus.RegisterEvent(this);
        }

        private void SetUpRectangleShapePrefabs()
        {
            rectangleShapePrefabs = new List<RectangleShape>();

            RectangleShape rectangleShapePrefab1 = new RectangleShape(new Vector2D(1, 1));
            rectangleShapePrefab1.FillColor = Color.Black;
            rectangleShapePrefab1.OutlineColor = Color.White;
            rectangleShapePrefab1.OutlineThickness = 1;
            rectangleShapePrefabs.Add(rectangleShapePrefab1);

            RectangleShape rectangleShapePrefab2 = new RectangleShape(new Vector2D(1, 1));
            rectangleShapePrefab2.FillColor = Color.Black;
            rectangleShapePrefab2.OutlineColor = Color.White;
            rectangleShapePrefab2.OutlineThickness = 4;
            rectangleShapePrefabs.Add(rectangleShapePrefab2);

            RectangleShape rectangleShapePrefab3 = new RectangleShape(new Vector2D(1, 1));
            rectangleShapePrefab3.FillColor = Color.Black;
            rectangleShapePrefab3.OutlineColor = Color.White;
            rectangleShapePrefab3.OutlineThickness = 8;
            rectangleShapePrefabs.Add(rectangleShapePrefab3);

            RectangleShape rectangleShapePrefab4 = new RectangleShape(new Vector2D(1, 1));
            rectangleShapePrefab4.FillColor = Color.White;
            rectangleShapePrefab4.OutlineColor = Color.White;
            rectangleShapePrefab4.OutlineThickness = 1;
            rectangleShapePrefabs.Add(rectangleShapePrefab4);
        }

        public void ChangeToPrefab(RectangleShape prefab)
        {
            this.Rectangle.FillColor = prefab.FillColor;
            this.Rectangle.OutlineColor = prefab.OutlineColor;
            this.Rectangle.OutlineThickness = prefab.OutlineThickness;
        }

        public event EventHandler Play;

        public void OnPlay()
        {
            if (_hitPlayer)
            {
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.HitPlayer));
            }
            else
            {
                //Change here for another Sound when a Brick is Hit.
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.HitPlayer));
            }
        }

        public bool PowerUpCollision(PowerUp powerUp)
        {
            return Rectangle.GetGlobalBounds().Intersects(powerUp.GetGlobalBounds());
        }



        public bool NewReflectionCollision(CircleObject circleObject, AnimationManager animationManager)
        {
            if (Rectangle.GetGlobalBounds().Intersects(circleObject.GetRectangleBoundary().GetGlobalBounds()))
            {



                Vector2D cornerPosition = Vector2D.Zero();
                Vector2D circleHitPosition = Vector2D.Zero();

                if (circleObject.Direction.X >= 0 && circleObject.Direction.Y <= 0)   // bottom left
                {

                    /// get all important verctors
                    cornerPosition = new Vector2D(Rectangle.Position.X - Rectangle.GetGlobalBounds().Width / 2, Rectangle.Position.Y + Rectangle.GetGlobalBounds().Height / 2);

                    Vector2D cornerUpPosition = cornerPosition + Vector2D.Up();
                    Vector2D cornerRightPosition = cornerPosition + Vector2D.Right();

                    Vector2D circleOldPosition = circleObject.GetOldPosition;
                    Vector2D circleNewPosition = circleObject.Circle.Position;
                    Vector2D circleTotalMovement = circleObject.Circle.Position - circleOldPosition;
                    double circleRadius = circleObject.Circle.Radius;
                    Vector2D circleDirection = circleObject.Direction;
                    ///

                    
                    /// calculate nearest point to corner
                    Vector2D intersectionPoint = NearestCornerPoint(cornerPosition, circleOldPosition, circleDirection);
                    ///
                    

                    /// calculate side of collision

                    double t = 0;

                    if (intersectionPoint.GetDistance(cornerUpPosition) < intersectionPoint.GetDistance(cornerRightPosition))       //hit left
                    {

                        t = (cornerPosition.X - circleRadius - circleNewPosition.X) / circleTotalMovement.X;
                        circleHitPosition = new Vector2D(cornerPosition.X - circleRadius, circleNewPosition.Y + circleTotalMovement.Y * t);

                        circleObject.Circle.Position = circleHitPosition;

                        circleObject.Direction = new Vector2D(-circleDirection.X, circleDirection.Y);
                    }
                    else                                                                                                            //hit down
                    {
                        t = (cornerPosition.Y + circleRadius - circleNewPosition.Y) / circleTotalMovement.Y;
                        circleHitPosition = new Vector2D(circleNewPosition.X - circleTotalMovement.X * t, cornerPosition.Y + circleRadius);

                        circleObject.Circle.Position = circleHitPosition;

                        circleObject.Direction = new Vector2D(circleDirection.X, -circleDirection.Y);
                    }
                    ///

                    /// collision corner
                    if (circleHitPosition.X < cornerPosition.X && circleHitPosition.Y > cornerPosition.Y && circleHitPosition.GetDistance(cornerPosition) <= circleObject.Circle.Radius * 2)
                    {
                        circleObject.Direction = CollisionReaction(circleObject, cornerPosition, circleHitPosition, circleDirection);
                    }
                    ///


                }
                else if (circleObject.Direction.X <= 0 && circleObject.Direction.Y <= 0)  // bottom right
                {

                    /// get all important verctors
                    cornerPosition = new Vector2D(Rectangle.Position.X + Rectangle.GetGlobalBounds().Width / 2, Rectangle.Position.Y + Rectangle.GetGlobalBounds().Height / 2);
                    Vector2D cornerYPosition = cornerPosition + Vector2D.Up();
                    Vector2D cornerXPosition = cornerPosition + Vector2D.Left();

                    Vector2D circleOldPosition = circleObject.GetOldPosition;
                    Vector2D circleNewPosition = circleObject.Circle.Position;
                    Vector2D circleTotalMovement = circleObject.Circle.Position - circleOldPosition;
                    double circleRadius = circleObject.Circle.Radius;
                    Vector2D circleDirection = circleObject.Direction;
                    ///

                    
                    /// calculate nearest point to corner
                    Vector2D intersectionPoint = NearestCornerPoint(cornerPosition, circleOldPosition, circleDirection);
                    ///
                    

                    /// calculate side of collision

                    double t = 0;

                    if (intersectionPoint.GetDistance(cornerYPosition) < intersectionPoint.GetDistance(cornerXPosition))       //hit right
                    {

                        t = (cornerPosition.X + circleRadius - circleNewPosition.X) / circleTotalMovement.X;
                        circleHitPosition = new Vector2D(cornerPosition.X + circleRadius, circleNewPosition.Y + circleTotalMovement.Y * t);

                        circleObject.Circle.Position = circleHitPosition;

                        circleObject.Direction = new Vector2D(-circleDirection.X, circleDirection.Y);
                    }
                    else                                                                                                            //hit down
                    {
                        t = (cornerPosition.Y + circleRadius - circleNewPosition.Y) / circleTotalMovement.Y;
                        circleHitPosition = new Vector2D(circleNewPosition.X - circleTotalMovement.X * t, cornerPosition.Y + circleRadius);

                        circleObject.Circle.Position = circleHitPosition;

                        circleObject.Direction = new Vector2D(circleDirection.X, -circleDirection.Y);
                    }
                    ///

                    /// collision corner
                    if (circleHitPosition.X > cornerPosition.X && circleHitPosition.Y > cornerPosition.Y && circleHitPosition.GetDistance(cornerPosition) <= circleObject.Circle.Radius * 2)
                    {
                        circleObject.Direction = CollisionReaction(circleObject, cornerPosition, circleHitPosition, circleDirection);
                    }
                    ///
                }
                else if (circleObject.Direction.X >= 0 && circleObject.Direction.Y >= 0)  // top left
                {

                    /// get all important verctors
                    cornerPosition = new Vector2D(Rectangle.Position.X - Rectangle.GetGlobalBounds().Width / 2, Rectangle.Position.Y - Rectangle.GetGlobalBounds().Height / 2);
                    Vector2D cornerYPosition = cornerPosition + Vector2D.Down();
                    Vector2D cornerXPosition = cornerPosition + Vector2D.Right();

                    Vector2D circleOldPosition = circleObject.GetOldPosition;
                    Vector2D circleNewPosition = circleObject.Circle.Position;
                    Vector2D circleTotalMovement = circleObject.Circle.Position - circleOldPosition;
                    double circleRadius = circleObject.Circle.Radius;
                    Vector2D circleDirection = circleObject.Direction;
                    ///
                    

                    /// calculate nearest point to corner
                    Vector2D intersectionPoint = NearestCornerPoint(cornerPosition, circleOldPosition, circleDirection);
                    ///


                    /// calculate side of collision

                    double t = 0;

                    if (intersectionPoint.GetDistance(cornerYPosition) < intersectionPoint.GetDistance(cornerXPosition))       //hit left
                    {

                        t = (cornerPosition.X - circleRadius - circleNewPosition.X) / circleTotalMovement.X;
                        circleHitPosition = new Vector2D(cornerPosition.X - circleRadius, circleNewPosition.Y + circleTotalMovement.Y * t);

                        circleObject.Circle.Position = circleHitPosition;

                        circleObject.Direction = new Vector2D(-circleDirection.X, circleDirection.Y);
                    }
                    else                                                                                                            //hit down
                    {
                        t = (cornerPosition.Y - circleRadius - circleNewPosition.Y) / circleTotalMovement.Y;
                        circleHitPosition = new Vector2D(circleNewPosition.X - circleTotalMovement.X * t, cornerPosition.Y - circleRadius);

                        circleObject.Circle.Position = circleHitPosition;

                        circleObject.Direction = new Vector2D(circleDirection.X, -circleDirection.Y);
                    }
                    ///

                    /// collision corner
                    if (circleHitPosition.X < cornerPosition.X && circleHitPosition.Y < cornerPosition.Y && circleHitPosition.GetDistance(cornerPosition) <= circleObject.Circle.Radius * 2)
                    {
                        circleObject.Direction = CollisionReaction(circleObject, cornerPosition, circleHitPosition, circleDirection);
                    }
                    ///


                }
                else if (circleObject.Direction.X <= 0 && circleObject.Direction.Y >= 0)  // top right
                {

                    /// get all important verctors
                    cornerPosition = new Vector2D(Rectangle.Position.X + Rectangle.GetGlobalBounds().Width / 2, Rectangle.Position.Y - Rectangle.GetGlobalBounds().Height / 2);
                    Vector2D cornerYPosition = cornerPosition + Vector2D.Down();
                    Vector2D cornerXPosition = cornerPosition + Vector2D.Left();

                    Vector2D circleOldPosition = circleObject.GetOldPosition;
                    Vector2D circleNewPosition = circleObject.Circle.Position;
                    Vector2D circleTotalMovement = circleObject.Circle.Position - circleOldPosition;
                    double circleRadius = circleObject.Circle.Radius;
                    Vector2D circleDirection = circleObject.Direction;
                    ///
                    

                    /// calculate nearest point to corner
                    Vector2D intersectionPoint = NearestCornerPoint(cornerPosition, circleOldPosition, circleDirection);
                    ///
                    

                    /// calculate side of collision

                    double t = 0;

                    if (intersectionPoint.GetDistance(cornerYPosition) < intersectionPoint.GetDistance(cornerXPosition))       //hit right
                    {

                        t = (cornerPosition.X + circleRadius - circleNewPosition.X) / circleTotalMovement.X;
                        circleHitPosition = new Vector2D(cornerPosition.X + circleRadius, circleNewPosition.Y + circleTotalMovement.Y * t);

                        circleObject.Circle.Position = circleHitPosition;

                        circleObject.Direction = new Vector2D(-circleDirection.X, circleDirection.Y);
                    }
                    else                                                                                                            //hit down
                    {
                        t = (cornerPosition.Y - circleRadius - circleNewPosition.Y) / circleTotalMovement.Y;
                        circleHitPosition = new Vector2D(circleNewPosition.X - circleTotalMovement.X * t, cornerPosition.Y - circleRadius);

                        circleObject.Circle.Position = circleHitPosition;

                        circleObject.Direction = new Vector2D(circleDirection.X, -circleDirection.Y);
                    }
                    ///

                    /// collision corner
                    if (circleHitPosition.X > cornerPosition.X && circleHitPosition.Y < cornerPosition.Y && circleHitPosition.GetDistance(cornerPosition) <= circleObject.Circle.Radius * 2)
                    {
                        circleObject.Direction = CollisionReaction(circleObject, cornerPosition, circleHitPosition, circleDirection);
                    }
                    ///
                }


                OnPlay();

                return true;
            }

            return false;
        }

        private Vector2D NearestCornerPoint(Vector2D cornerPosition, Vector2D circleOldPosition, Vector2D circleDirection)
        {
            Vector2D circleOldCorner = cornerPosition - circleOldPosition;
            double circleOldCornerLength = circleOldCorner.GetLength();
            double angle = circleOldCorner.GetAngleBetween(circleDirection);
            double circleIntercectionPointLength = Math.Sin(angle) * circleOldCornerLength;
            return circleDirection.Normalize() * circleIntercectionPointLength + circleOldPosition;
        }

        private Vector2D CollisionReaction(CircleObject circleObject, Vector2D cornerPosition, Vector2D circleHitPosition, Vector2D circleDirection)
        {
            Vector2D cornerCircle = cornerPosition - circleHitPosition;
            double angle = cornerCircle.GetAngleBetween(circleDirection);
            double cornerCircleLength = Math.Cos(angle) * circleDirection.GetLength();

            double balanceLength = Math.Sin(angle) * circleDirection.GetLength();

            cornerCircle = cornerCircle.Normalize() * cornerCircleLength;

            Vector2D balance = cornerCircle.Orthogonal();
            balance = balance.Normalize() * balanceLength;

            if (balance.GetAngleBetween(circleDirection) > Math.PI / 4 + 1)
            {
                balance = Vector2D.Zero() - balance;
            }


            cornerCircle = Vector2D.Zero() - cornerCircle;

            return cornerCircle + balance;
            
        }

        public bool ReflectionCollision(CircleObject circleObject)
        {
            if (Rectangle.GetGlobalBounds().Intersects(circleObject.GetRectangleBoundary().GetGlobalBounds()))
            {
                /*if (circleObject.Position.X + circleObject.Radius < Position.X - Size.X / 2)
                {
                    circleObject.Direction = new Vector2D(-circleObject.Direction.X, circleObject.Direction.Y);
                    circleObject.Position = new Vector2D(Position.X - Size.X / 2 - circleObject.Radius * 1.5f, circleObject.Position.Y);
                    circleObject.Update(0);
                }
                if (circleObject.Position.X - circleObject.Radius > Position.X + Size.X / 2)
                {
                    circleObject.Direction = new Vector2D(-circleObject.Direction.X, circleObject.Direction.Y);
                    circleObject.Position = new Vector2D(Position.X + Size.X / 2 + circleObject.Radius * 1.5f, circleObject.Position.Y);
                    circleObject.Update(0);
                }*/
                /*if (circleObject.Circle.Position.Y + circleObject.Circle.Radius > Rectangle.Position.Y - Rectangle.Size.Y && circleObject.Circle.Position.Y + circleObject.Circle.Radius < Rectangle.Position.Y)
                {
                    circleObject.Direction = new Vector2D(circleObject.Direction.X, -circleObject.Direction.Y);
                    circleObject.Circle.Position = new Vector2D(circleObject.Circle.Position.X, Rectangle.Position.Y - Rectangle.Size.Y / 2 - circleObject.Circle.Radius * 1.5f);
                    circleObject.Update(0);
                }
                if (circleObject.Circle.Position.Y - circleObject.Circle.Radius < Rectangle.Position.Y + Rectangle.Size.Y && circleObject.Circle.Position.Y - circleObject.Circle.Radius > Rectangle.Position.Y)
                {
                    circleObject.Direction = new Vector2D(circleObject.Direction.X, -circleObject.Direction.Y);
                    circleObject.Circle.Position = new Vector2D(circleObject.Circle.Position.X, Rectangle.Position.Y + Rectangle.Size.Y / 2 + circleObject.Circle.Radius * 1.5f);
                    circleObject.Update(0);
                }*/
                
                if(circleObject.GetOldPosition.X > Rectangle.Position.X + Rectangle.Size.X/2 || circleObject.GetOldPosition.X < Rectangle.Position.X - Rectangle.Size.X/2)
                {
                    circleObject.Direction = new Vector2D(-circleObject.Direction.X, circleObject.Direction.Y);
                    if(circleObject.GetOldPosition.X > Rectangle.Position.X + Rectangle.Size.X/2)
                        circleObject.RePosition(new Vector2D(Rectangle.Position.X + Rectangle.Size.X / 2 + circleObject.Circle.Radius, circleObject.Circle.Position.Y));
                    else
                        circleObject.RePosition(new Vector2D(Rectangle.Position.X - Rectangle.Size.X / 2 - circleObject.Circle.Radius, circleObject.Circle.Position.Y));
                }
                if (circleObject.GetOldPosition.Y > Rectangle.Position.Y + Rectangle.Size.Y/2 || circleObject.GetOldPosition.Y < Rectangle.Position.Y - Rectangle.Size.Y/2)
                {
                    circleObject.Direction = new Vector2D(circleObject.Direction.X, -circleObject.Direction.Y);
                    if (circleObject.GetOldPosition.Y > Rectangle.Position.Y + Rectangle.Size.Y/2)
                        circleObject.RePosition(new Vector2D(circleObject.Circle.Position.X, Rectangle.Position.Y + Rectangle.Size.Y / 2 + circleObject.Circle.Radius));
                    else
                        circleObject.RePosition(new Vector2D(circleObject.Circle.Position.X, Rectangle.Position.Y - Rectangle.Size.Y / 2 - circleObject.Circle.Radius));
                }
                _hitPlayer = false;
                OnPlay();

                return true;
            }
            else
            {
                return false;
            }
        }

        public Vector2D PointLineDist(Vector2D point, Vector2D linestart, Vector2D lineend)
        {
            Vector2D a = lineend - linestart;
            Vector2D b = point - linestart;
            double t = (a * b) / (a.GetLength() * a.GetLength());
            if (t < 0) t = 0;
            if (t > 1) t = 1;
            return linestart + (a * t);
        }

        public bool PongCollision(CircleObject circleObject)
        {

            if (Rectangle.GetGlobalBounds().Intersects(circleObject.GetRectangleBoundary().GetGlobalBounds()))
            {
                /*float heightDifference = this.Position.Y - circleObject.Position.Y;

                if (heightDifference > this.Size.Y / 2)
                    heightDifference = this.Size.Y / 2;
                else if (heightDifference < -this.Size.Y / 2)
                    heightDifference = -this.Size.Y / 2;

                Vector2D newDirection;
                
                float percent = -((heightDifference * 50) / (this.Size.Y/2)) / 100;
                if(Program.windowSize.X/2 > circleObject.Position.X)
                {
                    newDirection = new Vector2D((float)Math.Cos(percent * (float)Math.PI / 2), percent * (float)Math.PI);
                    circleObject.Position = new Vector2D(this.Position.X + this.Size.X / 2 + circleObject.Radius * 1.5f, circleObject.Position.Y);
                    circleObject.Update(0);
                }
                else
                {
                    newDirection = new Vector2D(-(float)Math.Cos(percent * (float)Math.PI / 2), percent * (float)Math.PI);
                    circleObject.Position = new Vector2D(this.Position.X - this.Size.X / 2 - circleObject.Radius * 1.5f, circleObject.Position.Y);
                    circleObject.Update(0);
                }*/


                double heightDifference = Rectangle.Position.X - circleObject.Circle.Position.X;

                if (heightDifference > Rectangle.Size.X / 2)
                    heightDifference = Rectangle.Size.X / 2;
                else if (heightDifference < -Rectangle.Size.X / 2)
                    heightDifference = -Rectangle.Size.X / 2;

                Vector2D newDirection;

                double percent = -((heightDifference * 50) / (Rectangle.Size.X / 2)) / 100;

                newDirection = new Vector2D(percent * Math.PI, Math.Cos(percent * Math.PI / 2));
                circleObject.Circle.Position = new Vector2D(circleObject.Circle.Position.X, Rectangle.Position.Y - Rectangle.Size.Y / 2 - circleObject.Circle.Radius * 1.5f);
                circleObject.Update(0);


                double scalar = Math.Sqrt(newDirection.X * newDirection.X + newDirection.Y * newDirection.Y);
                newDirection = new Vector2D(newDirection.X / scalar, -newDirection.Y / scalar);
                
                circleObject.Direction = newDirection;
                circleObject.CollisionHappened();
                _hitPlayer = true;
                OnPlay();

                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void RectangleMovementUp(double elapsedTime)
        {
            if (Rectangle.Position.Y > 0)
                Rectangle.Position = new Vector2D(Rectangle.Position.X, Rectangle.Position.Y - (1 * elapsedTime));
        }

        public void RectangleMovementDown(double elapsedTime, double windowHeight)
        {
            if (Rectangle.Position.Y < windowHeight)
                Rectangle.Position = new Vector2D(Rectangle.Position.X, Rectangle.Position.Y + (1 * elapsedTime));
        }

        public void RectangleMovementLeft(double elapsedTime)
        {
            if (Rectangle.Position.X > 0)
                Rectangle.Position = new Vector2D(Rectangle.Position.X - (1 * elapsedTime), Rectangle.Position.Y);
        }

        public void RectangleMovementRight(double elapsedTime, double windowWidth)
        {
            if (Rectangle.Position.X < windowWidth)
                Rectangle.Position = new Vector2D(Rectangle.Position.X + (1 * elapsedTime), Rectangle.Position.Y);
        }
        
    }
}