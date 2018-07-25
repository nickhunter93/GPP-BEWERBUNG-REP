using SFML.Graphics;

namespace ConsoleApp2
{
    public class HealthBar : GuiElement
    {
        private RectangleShape _background;
        private RectangleShape _lifeLost;
        private RectangleShape _life;
        private double _timer = 0;
        private readonly double _TIME_TO_SHOW_LIFE_LOST = 1000;
        private double _maxLife;
        private double _savedLife = -1;
        private double _lifeLostDifference = 0;
        private readonly double _LIFE_LOST_SPEED = 300;

        public HealthBar(Vector2D position, Vector2D size, Font font, double maxLife) : base(position, size, font)
        {
            _maxLife = maxLife;

            _background = new RectangleShape(size);
            _background.FillColor = Color.Black;
            _background.OutlineColor = Color.White;
            _background.OutlineThickness = 2;
            _background.Position = position;

            _lifeLost = new RectangleShape(size);
            _lifeLost.FillColor = new Color(255,255,255,255/3);
            _lifeLost.Position = position;

            _life = new RectangleShape(size);
            _life.Position = position;

            _drawables.Add(_background);
            _drawables.Add(_lifeLost);
            _drawables.Add(_life);
        }

        public override bool Touched(Vector2D position)
        {
            return false;
        }

        public override void Update(double elapsedTime)
        {
            _timer -= elapsedTime;
            if (_timer >= 0)
            {
                _timer -= elapsedTime;
            }
            else
            {
                if (_lifeLost.Scale.X > _life.Scale.X)
                {
                    _lifeLost.Scale = new Vector2D(_lifeLost.Scale.X - _lifeLostDifference / _LIFE_LOST_SPEED * elapsedTime, _lifeLost.Scale.Y);
                }
            }
        }

        public void ChangeLife(double life)
        {
            if (life < 0)
                life = 0;
            else if (life > _maxLife)
                life = _maxLife;

            if (_savedLife == life)
                return;


            _lifeLostDifference = _lifeLost.Scale.X - life / _maxLife;
            _life.Scale = new Vector2D(life / _maxLife, _life.Scale.Y);

            if (_timer <= 0)
            {
                _savedLife = life;
                _timer = _TIME_TO_SHOW_LIFE_LOST;
            }

            if (_life.Scale.X >= _lifeLost.Scale.X)
            {
                _lifeLost.Scale = new Vector2D(_life.Scale.X, _lifeLost.Scale.Y);
                _timer = 0;
            }
            
        }
    }
}