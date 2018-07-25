using SFML.Graphics;
using System.Threading;

namespace ConsoleApp2
{
    public class Disco : State
    {
        private int _colorChangeRange = 2;
        private static int _nextColor = -1;


        private System.Random _random = new System.Random();

        public Disco()
        {
            this.Texture = new Texture("Pictures/Disco.png");
            this._duration = 5000;
        }

        public override void Update(GameObject player, double elapsedTime)
        {
            if (player.GetComponent<RenderComponent>() != null)
                player.GetComponent<RenderComponent>().Shape.FillColor = RandomColor(elapsedTime);
        }
        
        public override void Finish(GameObject player)
        {
            if (player.GetComponent<RenderComponent>() != null)
                player.GetComponent<RenderComponent>().Shape.FillColor = Color.White;
        }

        public PowerUpScript Clone()
        {
            return new PowerUpScript(this);
        }

        private Color RandomColor(double elapsedTime)
        {
            Color newColor = new Color();
            
            float[] newColorHsv = new float[3];

            if (_nextColor == -1)
            {
                _nextColor = _random.Next(0, 255);
            }
            else
            {
                _nextColor += _colorChangeRange;
                if (_nextColor > 255)
                {
                    _nextColor -= 255;
                }
            }

            newColorHsv[0] = _nextColor;
            newColorHsv[1] = 1;
            newColorHsv[2] = 255;

            float[] newColorRgb = HsvToRgb(newColorHsv);
            
            newColor.R = (byte)newColorRgb[0];
            newColor.G = (byte)newColorRgb[1];
            newColor.B = (byte)newColorRgb[2];
            newColor.A = 255;
            
            return newColor;
        }

        private float[] HsvToRgb(float[] value)
        {
            var h = value[0];
            var s = value[1];
            var v = value[2];
            if (s == 0)
            {
                return new[] { v, v, v }; // achromatic (grey)
            }
            h /= 60F;                   // sector 0 to 5
            var i = (int)System.Math.Floor(h);
            var f = h - i;                      // factorial part of h
            var p = v * (1F - s);
            var q = v * (1F - s * f);
            var t = v * (1F - s * (1F - f));
            switch (i)
            {
                case 0: return new[] { v, t, p };
                case 1: return new[] { q, v, p };
                case 2: return new[] { p, v, t };
                case 3: return new[] { p, q, v };
                case 4: return new[] { t, p, v };
                default: return new[] { v, p, q };
            }
        }
        
    }
}