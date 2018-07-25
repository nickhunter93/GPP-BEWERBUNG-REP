using SFML.Graphics;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Animation
    {
        private Vector2D _translateVector;
        private Vector2D _endPosition;
        private Transformable[] _transformables;
        private double _durationLeft;
        private double _timeOfStart;

        public Animation(Vector2D positionOfFirst, Transformable[] transformables, double duration, double timeOfStart, Vector2D positionOfEnd)
        {
            if (!Program.playAnimations)
                return;

            Transformables = (Transformable[])transformables.Clone();
            _durationLeft = duration;
            _timeOfStart = timeOfStart;

            Vector2D endPosition = positionOfEnd;

            /*Vector2D translateNow = positionOfFirst - positionOfEnd;

            foreach (Transformable transformable in _objects)
            {
                if (transformable is GuiGroup guiGroup)
                {
                    guiGroup.Position += translateNow;
                }
                else
                    transformable.Position += translateNow;
            }*/

            //positionOfFirst = endPosition;
        
            _endPosition = positionOfEnd;
            _translateVector = -(positionOfFirst - positionOfEnd) / duration;
        }

        public Animation(Vector2D positionOfFirst, Transformable[] objects, double duration, double timeOfStart, bool isStartPosition)
        {
            if (!Program.playAnimations)
                return;

            Transformables = (Transformable[])objects.Clone();
            _durationLeft = duration;
            _timeOfStart = timeOfStart;

            if (isStartPosition)
            {
                Vector2D endPosition = Transformables[0].Position;

                Vector2D translateNow = positionOfFirst - Transformables[0].Position;

                foreach (Transformable transformable in Transformables)
                {
                    if (transformable is GuiGroup guiGroup)
                    {
                        guiGroup.Position += translateNow;
                    }
                    else
                        transformable.Position += translateNow;
                }

                positionOfFirst = endPosition;
            }
            _endPosition = positionOfFirst;
            _translateVector = (positionOfFirst - Transformables[0].Position) / duration;
            
        }

        public double DurationLeft { get => _durationLeft; }
        public Transformable[] Transformables { get => _transformables; set => _transformables = value; }

        public void Update(double elapsedTime)
        {
            
            if (_timeOfStart > 0)
            {
                _timeOfStart -= elapsedTime;
                return;
            }

            for (int i = 0; i < Transformables.Length; i++)
            {
                if (Transformables[i] is GuiGroup guiGroup)
                {
                    guiGroup.Position += _translateVector * elapsedTime;
                }
                else
                {
                    Transformables[i].Position += _translateVector * elapsedTime;
                }

            }
            _durationLeft -= elapsedTime;
        }

        public void GoToEndPosition()
        {
            Vector2D endPosition = Transformables[0].Position;

            Vector2D translateNow = _endPosition - Transformables[0].Position;

            foreach (Transformable transformable in Transformables)
            {
                if (transformable is GuiGroup guiGroup)
                {
                    guiGroup.Position += translateNow;
                }
                else
                    transformable.Position += translateNow;
            }

            /*
            for (int i = 0; i < _objects.Length; i++)
            {
                if (_objects[i] is GuiGroup guiGroup)
                {
                    guiGroup.Position = _endPosition;
                }
                else
                {
                    _objects[i].Position += _endPosition;
                }

            }*/
        }
        
    }
}