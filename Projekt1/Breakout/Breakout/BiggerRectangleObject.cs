using SFML.Graphics;
using SFML.System;
using System;
using System.Threading;

namespace ConsoleApp2
{
    public class BiggerRectangleObject : State
    {
        private CircleObject _circleObject;
        private double _originalRectangleSizeX;
        private double _bigRectangleSizeX = 200;
        private bool _isBig = false;

        public BiggerRectangleObject(CircleObject circleObject)
        {
            _circleObject = circleObject;
            this.Texture = new Texture("BiggerRectangleObject.png");
            this._duration = 10000;

        }
        
        public override void Update(RectangleObject player, double elapsedTime)
        {
            if (!_isBig)
            {
                _originalRectangleSizeX = player.Rectangle.Size.X;

                player.Rectangle.Size = new Vector2D(_bigRectangleSizeX, player.Rectangle.Size.Y);
                player.Rectangle.Origin = new Vector2D(_bigRectangleSizeX / 2, player.Rectangle.Origin.Y);

                _isBig = true;
            }
            
        }

        public override void Finish(RectangleObject player)
        {
            player.Rectangle.Size = new Vector2D(_originalRectangleSizeX, player.Rectangle.Size.Y);
            player.Rectangle.Origin = new Vector2D(_originalRectangleSizeX / 2, player.Rectangle.Origin.Y);
            _isBig = false;
        }
        
        public PowerUp Clone()
        {
            return new PowerUp(this);
        }
    }
}