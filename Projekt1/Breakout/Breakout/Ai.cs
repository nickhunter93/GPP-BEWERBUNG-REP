using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Ai
    {
        private RectangleObject _rectangleObject;
        private CircleObject _circleObject;
        private Vector2D _windowSize;
        private double _nextPosition;
        private double _randomNumber;
        private Difficulty _difficulty;
        private double _timer = 0;

        private double _buffer = 0;

        public Difficulty AIDifficulty { get => _difficulty; set => _difficulty = value; }

        public enum Difficulty
        {
            Human,
            TargetMode,
            Easy,
            Normal,
            Hard,
            None
        }

        public Ai(RectangleObject rectangleObject, CircleObject circleObject, Vector2D windowSize, Difficulty difficulty)
        {
            _rectangleObject = rectangleObject;
            _circleObject = circleObject;
            _windowSize = windowSize;
            _nextPosition = windowSize.Y / 2;
            AIDifficulty = difficulty;
        }
        
        public void Update(double elapsedTime, bool opponendCollided, bool selfCollided, bool resetCircleObject)
        {
            if (_rectangleObject == null || _circleObject == null || AIDifficulty == Difficulty.Human || AIDifficulty == Difficulty.None)
                return;

            if (AIDifficulty == Difficulty.TargetMode)
                TargetMode(elapsedTime, opponendCollided, selfCollided, resetCircleObject);
            else if (AIDifficulty == Difficulty.Easy)
                Easy(elapsedTime, opponendCollided, selfCollided, resetCircleObject);
            else if (AIDifficulty == Difficulty.Normal)
                Normal(elapsedTime, opponendCollided, selfCollided, resetCircleObject);
            else if (AIDifficulty == Difficulty.Hard)
                Hard(elapsedTime, opponendCollided, selfCollided, resetCircleObject);
        }

        public int Random(int min, int max)
        {
            System.Random rnd = new System.Random();
            return rnd.Next(min, max);
        }

        public void TargetMode(double elapsedTime, bool opponendCollided, bool selfCollided, bool resetCircleObject)
        {
            if (selfCollided)
            {
                _nextPosition = Random(0, (int)_windowSize.Y);
            }

            GoToNextPosition(elapsedTime);
        }

        public void Easy(double elapsedTime, bool opponendCollided, bool selfCollided, bool resetCircleObject)
        {
            _timer += (float)elapsedTime;

            if (_timer > 1000 || selfCollided)
            {
                _timer = 0;
                _nextPosition = Random(0, (int)_windowSize.Y);
            }

            GoToNextPosition(elapsedTime);
        }

        public void Normal(double elapsedTime, bool opponendCollided, bool selfCollided, bool resetCircleObject)
        {
            /*_nextPosition = _circleObject.Position.Y;

            if (selfCollided || resetCircleObject)
            {
                _randomNumber = Random(-(int)(_rectangleObject.Size.Y / 2.5f), (int)(_rectangleObject.Size.Y / 2.5f));
            }
            
            _nextPosition += _randomNumber;*/


            _nextPosition = _circleObject.Circle.Position.X;

            if (selfCollided || resetCircleObject)
            {
                _randomNumber = Random(-(int)(_rectangleObject.Rectangle.Size.X / 2.5f), (int)(_rectangleObject.Rectangle.Size.X / 2.5f));
            }

            _nextPosition += _randomNumber;


            GoToNextPosition(elapsedTime);
        }

        public bool SideCondition(double nP, double rectangleX)
        {
            if (_rectangleObject.Rectangle.Position.X < _windowSize.X / 2)
            {
                return nP > _rectangleObject.Rectangle.Position.X && nP < _windowSize.X + 0.1f;
            }
            else
            {
                return nP < _rectangleObject.Rectangle.Position.X && nP > -0.1f;
            }
        }

        public void Hard(double elapsedTime, bool opponendCollided, bool selfCollided, bool resetCircleObject)
        {
            
            if (resetCircleObject)
            {

                if (_rectangleObject.Rectangle.Position.X < _windowSize.X / 2)
                {
                    if (_circleObject.Direction.X < 0)
                    {
                        opponendCollided = true;
                    }
                    else
                    {
                        selfCollided = true;
                    }
                }
                else
                {

                    if (_circleObject.Direction.X > 0)
                    {
                        opponendCollided = true;
                    }
                    else
                    {
                        selfCollided = true;
                    }
                }

            }


            if (opponendCollided)
            {
                int sideHitNumber = 0;
                Vector2D calcCirclePosition = _circleObject.Circle.Position;
                Vector2D calcCircleDirection = _circleObject.Direction;
                Vector2D lastCalcCirclePosition = _circleObject.Circle.Position;
                Vector2D lastCalcCircleDirection = _circleObject.Direction;

                if (_circleObject.Direction.Y != 0)
                {
                    
                    double lastNP = 0;
                    double nP = _windowSize.Y / 2;

                    sideHitNumber = 0;

                    while (SideCondition(nP, _rectangleObject.Rectangle.Position.X))
                    {
                        lastNP = nP;
                        
                        if (calcCircleDirection.Y > 0)
                        {
                            double tt = (_windowSize.Y - _circleObject.Circle.Radius - calcCirclePosition.Y) / calcCircleDirection.Y;
                            nP = calcCirclePosition.X + calcCircleDirection.X * tt;
                        }
                        else
                        {
                            double tt = (_circleObject.Circle.Radius + calcCirclePosition.Y) / calcCircleDirection.Y;
                            nP = calcCirclePosition.X - calcCircleDirection.X * tt;
                        }

                        
                        if (calcCircleDirection.Y > 0)
                            calcCirclePosition = new Vector2D(nP, _windowSize.Y - _circleObject.Circle.Radius);
                        else
                            calcCirclePosition = new Vector2D(nP, _circleObject.Circle.Radius);
                        

                        if (SideCondition(nP, _rectangleObject.Rectangle.Position.X))
                        {
                            calcCircleDirection = new Vector2D(calcCircleDirection.X, -calcCircleDirection.Y);
                        }

                        sideHitNumber++;
                    }
                    
                    lastCalcCircleDirection = calcCircleDirection;
                    

                    if (calcCirclePosition.Y > _windowSize.Y / 2)
                    {
                        lastCalcCirclePosition = new Vector2D(lastNP, _circleObject.Circle.Radius);
                    }
                    else
                    {
                        lastCalcCirclePosition = new Vector2D(lastNP, _windowSize.Y - _circleObject.Circle.Radius);
                    }

                }


                double t = (_rectangleObject.Rectangle.Position.X - _circleObject.Circle.Radius - _rectangleObject.Rectangle.Size.X / 2 - lastCalcCirclePosition.X) / lastCalcCircleDirection.X;
                
                if (lastCalcCirclePosition.Y > _windowSize.Y / 2)
                {
                    _nextPosition = lastCalcCirclePosition.Y + lastCalcCircleDirection.Y * t;
                }
                else
                {
                    _nextPosition = lastCalcCircleDirection.Y * t;
                }



                if (sideHitNumber <= 1)
                {
                    calcCirclePosition = _circleObject.Circle.Position;
                    calcCircleDirection = _circleObject.Direction;
                    
                    t = (_rectangleObject.Rectangle.Position.X - calcCirclePosition.X) / calcCircleDirection.X;
                    _nextPosition = calcCirclePosition.Y + calcCircleDirection.Y * t;
                }

                if (_nextPosition < _windowSize.Y / 2)
                {
                    _nextPosition -= Random(0, (int)_rectangleObject.Rectangle.Size.Y / 3);
                }
                else
                {
                    _nextPosition += Random(0, (int)_rectangleObject.Rectangle.Size.Y / 3);
                }

            }
            else if (selfCollided)
            {
                _nextPosition = _windowSize.Y / 2;
            }

            
            

            GoToNextPosition(elapsedTime);
        }

        public void GoToNextPosition(double elapsedTime)
        {
            _buffer = elapsedTime;

            /*if (_rectangleObject.Position.Y < _nextPosition - _buffer)
            {
                _rectangleObject.RectangleMovementDown(elapsedTime, _windowSize.Y);
            }
            else if (_rectangleObject.Position.Y > _nextPosition + _buffer)
            {
                _rectangleObject.RectangleMovementUp(elapsedTime);
            }*/
            //System.Console.Out.WriteLine(_difficulty);
            if (_rectangleObject.Rectangle.Position.X < _nextPosition - _buffer)
            {
                _rectangleObject.RectangleMovementRight(elapsedTime, _windowSize.X);
            }
            else if (_rectangleObject.Rectangle.Position.X > _nextPosition + _buffer)
            {
                _rectangleObject.RectangleMovementLeft(elapsedTime);
            }
        }
    }
}






