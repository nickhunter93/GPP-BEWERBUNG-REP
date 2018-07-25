using SFML.Graphics;
using SFML.System;

namespace ConsoleApp2
{
    public class SpeedDown : State
    {
        private bool _isSpeedDown = false;
        private int _strength = 700;

        public SpeedDown()
        {
            this.Texture = new Texture("Pictures/SpeedDown.png");
            this._duration = 5000;
        }
        
        public override void Update(GameObject player, double elapsedTime)
        {
            if (!_isSpeedDown)
            {
                Program.slowMotion += _strength;

                _isSpeedDown = true;
            }
        }

        public override void Finish(GameObject player)
        {
            Program.slowMotion -= _strength;
            _isSpeedDown = false;
        }

        public PowerUpScript Clone()
        {
            return new PowerUpScript(this);
        }
    }
}