using SFML.Graphics;
using System.Threading;

namespace ConsoleApp2
{
    internal class Disco : PowerUp
    {
        private CircleObject _circleObject;
        private int _colorChangeLength = 100;
        private int _colorChangeNumbers = 30;
        private static int _lastColor = 6;

        public Disco(CircleObject circleObject)
        {
            _circleObject = circleObject;
            this.Texture = new Texture("Disco.png");
        }

        public override void Execute()
        {
            Thread newThread = new Thread(ColorChange);
            newThread.Start();
        }

        private void ColorChange()
        {
            for (int i = 0; i < _colorChangeNumbers; i++)
            {
                _circleObject.FillColor = RandomColor();
                Thread.Sleep(_colorChangeLength);
            }

            _circleObject.FillColor = Color.White;
            
        }

        public override PowerUp Clone()
        {
            return new Disco(_circleObject);
        }

        private Color RandomColor()
        {
            Color newColor;

            int rnd = Random(0, 5);
            if (rnd == _lastColor)
            {
                rnd++;
                if (rnd > 5)
                    rnd = 0;
            }

            switch (rnd)
            {
                case 0:
                    newColor = Color.Blue;
                    break;
                case 1:
                    newColor = Color.Red;
                    break;
                case 2:
                    newColor = Color.Yellow;
                    break;
                case 3:
                    newColor = Color.Magenta;
                    break;
                case 4:
                    newColor = Color.Cyan;
                    break;
                case 5:
                    newColor = Color.Green;
                    break;
                default:
                    newColor = Color.White;
                    break;
            }
            _lastColor = rnd;

            return newColor;
        }

        private int Random(int min, int max)
        {
            System.Random rnd = new System.Random();
            return rnd.Next(min, max);
        }
    }
}