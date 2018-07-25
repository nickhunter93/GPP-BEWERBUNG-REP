using SFML.Graphics;
using SFML.System;

namespace ConsoleApp2
{
    public abstract class PowerUp : Sprite
    {
        private Vector2f _direction;
        private float _speed;
        private RectangleObject _rectangleObject;
        private Texture _texture;
        private Vector2f _size;

        public Vector2f Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public RectangleObject RectangleObject
        {
            get { return _rectangleObject; }
            set { _rectangleObject = value; }
        }
        public Vector2f Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public PowerUp()
        {
            this.Texture = new Texture("Prefab.png");
            this.Size = (Vector2f)this.Texture.Size;
            this.Origin = new Vector2f(this.Size.X / 2, this.Size.Y / 2);
        }

        public void Spawn(Vector2f position, Vector2f direction, float speed, RectangleObject rectangleObject)
        {
            this.Position = position;
            this.Direction = direction;
            this.Speed = speed;
            this.RectangleObject = rectangleObject;

        }

        public abstract void Execute();
        public abstract PowerUp Clone();

        public void Update(double elapsedTime)
        {
            this.Position += _direction * _speed * (float)elapsedTime;
        }
    }
}