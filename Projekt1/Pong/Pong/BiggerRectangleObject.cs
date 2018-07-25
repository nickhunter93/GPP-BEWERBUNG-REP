using SFML.Graphics;
using SFML.System;
using System;
using System.Threading;

namespace ConsoleApp2
{
    internal class BiggerRectangleObject : PowerUp
    {
        private CircleObject _circleObject;
        private int _bigRectangleTime = 10000;
        private static float _originalRectangleSizeY = 0;
        private float _bigRectangleSizeY = 200;

        public BiggerRectangleObject(CircleObject circleObject)
        {
            _circleObject = circleObject;
            this.Texture = new Texture("BiggerRectangleObject.png");
        }

        public override void Execute()
        {
            if (_originalRectangleSizeY == 0)
                _originalRectangleSizeY = this.RectangleObject.Size.Y;
            
            Thread newThread = new Thread(Resize);
            newThread.Start();
        }

        private void Resize()
        {
            this.RectangleObject.Size = new Vector2f(this.RectangleObject.Size.X, _bigRectangleSizeY);
            this.RectangleObject.Origin = new Vector2f(this.RectangleObject.Origin.X, _bigRectangleSizeY / 2);

            Thread.Sleep(_bigRectangleTime);
            
            this.RectangleObject.Size = new Vector2f(this.RectangleObject.Size.X, _originalRectangleSizeY);
            this.RectangleObject.Origin = new Vector2f(this.RectangleObject.Origin.X, _originalRectangleSizeY / 2);
            
        }

        public override PowerUp Clone()
        {
            return new BiggerRectangleObject(_circleObject);
        }
    }
}