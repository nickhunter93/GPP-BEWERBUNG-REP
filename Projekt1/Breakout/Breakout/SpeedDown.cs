using SFML.Graphics;
using SFML.System;

namespace ConsoleApp2
{
    public class SpeedDown : State
    {
        private CircleObject _circleObject;
        private bool _isSpeedDown = false;

        public SpeedDown(CircleObject circleObject)
        {
            this._circleObject = circleObject;
            this.Texture = new Texture("SpeedDown.png");
            this._duration = 1;
        }
        
        public override void Update(RectangleObject player, double elapsedTime)
        {
            if (!_isSpeedDown)
            {
                _circleObject.AdditionalSpeed = _circleObject.AdditionalSpeed / 2 - _circleObject.Speed / 2;

                if (_circleObject.AdditionalSpeed < 0)
                {
                    _circleObject.AdditionalSpeed = 0;
                }

                _isSpeedDown = true;
            }
        }

        public override void Finish(RectangleObject player)
        {
            _isSpeedDown = false;
        }

        public PowerUp Clone()
        {
            return new PowerUp(this);
        }
    }
}