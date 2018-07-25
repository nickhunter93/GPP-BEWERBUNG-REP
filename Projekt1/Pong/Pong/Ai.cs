using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Ai
    {
        private RectangleObject _rectangleObject;
        private CircleObject _circleObject;
        private Vector2f _windowSize;
        private float _nextPosition;
        private float _randomNumber;
        private Difficulty _difficulty;
        private float _timer = 0;

        private float _buffer = 0;

        public enum Difficulty
        {
            Human,
            TargetMode,
            Easy,
            Normal,
            Hard
        }

        public Ai(RectangleObject rectangleObject, CircleObject circleObject, Vector2f windowSize, Difficulty difficulty)
        {
            _rectangleObject = rectangleObject;
            _circleObject = circleObject;
            _windowSize = windowSize;
            _nextPosition = windowSize.Y / 2;
            _difficulty = difficulty;
        }

        public void ChangeDifficulty(Difficulty difficulty)
        {
            _difficulty = difficulty;
        }
        
        public void Update(double elapsedTime, bool opponendCollided, bool selfCollided, bool resetCircleObject)
        {
            if (_rectangleObject == null || _circleObject == null || _difficulty == Difficulty.Human)
                return;

            if (_difficulty == Difficulty.TargetMode)
                TargetMode(elapsedTime, opponendCollided, selfCollided, resetCircleObject);
            else if (_difficulty == Difficulty.Easy)
                Easy(elapsedTime, opponendCollided, selfCollided, resetCircleObject);
            else if (_difficulty == Difficulty.Normal)
                Normal(elapsedTime, opponendCollided, selfCollided, resetCircleObject);
            else if (_difficulty == Difficulty.Hard)
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
            _nextPosition = _circleObject.Position.Y;

            if (selfCollided || resetCircleObject)
            {
                _randomNumber = Random(-(int)(_rectangleObject.Size.Y / 2.5f), (int)(_rectangleObject.Size.Y / 2.5f));
            }
            
            _nextPosition += _randomNumber;
            
            GoToNextPosition(elapsedTime);
        }

        public bool SideCondition(float nP, float rectangleX)
        {
            if (_rectangleObject.Position.X < _windowSize.X / 2)
            {
                return nP > _rectangleObject.Position.X && nP < _windowSize.X + 0.1f;
            }
            else
            {
                return nP < _rectangleObject.Position.X && nP > -0.1f;
            }
        }

        public void Hard(double elapsedTime, bool opponendCollided, bool selfCollided, bool resetCircleObject)
        {
            
            if (resetCircleObject)
            {

                if (_rectangleObject.Position.X < _windowSize.X / 2)
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
                Vector2f calcCirclePosition = _circleObject.Position;
                Vector2f calcCircleDirection = _circleObject.Direction;
                Vector2f lastCalcCirclePosition = _circleObject.Position;
                Vector2f lastCalcCircleDirection = _circleObject.Direction;

                if (_circleObject.Direction.Y != 0)
                {
                    
                    float lastNP = 0;
                    float nP = _windowSize.Y / 2;

                    sideHitNumber = 0;

                    while (SideCondition(nP, _rectangleObject.Position.X))
                    {
                        lastNP = nP;
                        
                        if (calcCircleDirection.Y > 0)
                        {
                            float tt = (_windowSize.Y - _circleObject.Radius - calcCirclePosition.Y) / calcCircleDirection.Y;
                            nP = calcCirclePosition.X + calcCircleDirection.X * tt;
                        }
                        else
                        {
                            float tt = (_circleObject.Radius + calcCirclePosition.Y) / calcCircleDirection.Y;
                            nP = calcCirclePosition.X - calcCircleDirection.X * tt;
                        }

                        
                        if (calcCircleDirection.Y > 0)
                            calcCirclePosition = new Vector2f(nP, _windowSize.Y - _circleObject.Radius);
                        else
                            calcCirclePosition = new Vector2f(nP, _circleObject.Radius);
                        

                        if (SideCondition(nP, _rectangleObject.Position.X))
                        {
                            calcCircleDirection = new Vector2f(calcCircleDirection.X, -calcCircleDirection.Y);
                        }

                        sideHitNumber++;
                    }
                    
                    lastCalcCircleDirection = calcCircleDirection;
                    

                    if (calcCirclePosition.Y > _windowSize.Y / 2)
                    {
                        lastCalcCirclePosition = new Vector2f(lastNP, _circleObject.Radius);
                    }
                    else
                    {
                        lastCalcCirclePosition = new Vector2f(lastNP, _windowSize.Y - _circleObject.Radius);
                    }

                }


                float t = (_rectangleObject.Position.X - _circleObject.Radius - _rectangleObject.Size.X / 2 - lastCalcCirclePosition.X) / lastCalcCircleDirection.X;
                
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
                    calcCirclePosition = _circleObject.Position;
                    calcCircleDirection = _circleObject.Direction;
                    
                    t = (_rectangleObject.Position.X - calcCirclePosition.X) / calcCircleDirection.X;
                    _nextPosition = calcCirclePosition.Y + calcCircleDirection.Y * t;
                }

                if (_nextPosition < _windowSize.Y / 2)
                {
                    _nextPosition -= Random(0, (int)_rectangleObject.Size.Y / 3);
                }
                else
                {
                    _nextPosition += Random(0, (int)_rectangleObject.Size.Y / 3);
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
            _buffer = (float)elapsedTime;

            if (_rectangleObject.Position.Y < _nextPosition - _buffer)
            {
                _rectangleObject.RectangleMovementDown(elapsedTime, _windowSize.Y);
            }
            else if (_rectangleObject.Position.Y > _nextPosition + _buffer)
            {
                _rectangleObject.RectangleMovementUp(elapsedTime);
            }
        }
    }
}






