using SFML.Graphics;
using SFML.System;

namespace ConsoleApp2
{
    public class SpeedDown : PowerUp
    {
        private CircleObject _circleObject;

        public SpeedDown(CircleObject circleObject)
        {
            this._circleObject = circleObject;
            this.Texture = new Texture("SpeedDown.png");
        }

        public override void Execute()
        {
            _circleObject.AdditionalSpeed = _circleObject.AdditionalSpeed / 2 - _circleObject.Speed / 2;

            if (_circleObject.AdditionalSpeed < 0)
            {
                _circleObject.AdditionalSpeed = 0;
            }
        }

        public override PowerUp Clone()
        {
            return new SpeedDown(_circleObject);
        }
    }
}