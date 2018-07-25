using SFML.Graphics;
using SFML.System;
using System;

namespace ConsoleApp2
{
    public class RenderComponent : Component
    {
        private Sprite _sprite;
        private Vector2D _size;

        private Shape _shape;
        private Vector2D _position;
        private double _rotation;

        public Shape Shape { get => _shape; }
        public Vector2D Size { get => _size; set => _size = value; }
        public Vector2D Position { get => _position; set => _position = value; }
        public Sprite Sprite { get => _sprite; set => _sprite = value; }

        public RenderComponent(Shape shape)
        {
            Position = Vector2D.Zero();
            _shape = shape;
        }

        public RenderComponent(Texture texture)
        {
            Sprite = new Sprite(texture);
            this.Size = (Vector2f)Sprite.Texture.Size;
            Sprite.Origin = new Vector2D(this.Size.X / 2, this.Size.Y / 2);
            Position = Vector2D.Zero();
            Sprite.Position = Vector2D.Zero();
        }

        public override void Update(double elapsedTime)
        {
            GameObject parent = this.Parent;
            GameObject grandParent = null;
            Vector2D position = Vector2D.Zero();// parent.transform.Position;
            double rotation = 0;// parent.transform.Rotation;
            while(parent != null)
            {
                //position =  position.Rotate((Math.PI / 180) * parent.transform.Rotation) + parent.transform.Position;
                position += parent.transform.Position;
                position = position.RotateAround((Math.PI * parent.transform.Rotation / 180) , parent.transform.Position);
                
                rotation += parent.transform.Rotation;
                grandParent = parent;
                parent = parent.Parent;
            }
            if(Shape != null)
            {
                Shape.Position = position;
                Shape.Rotation = (float)rotation;
            }
            if(Sprite != null)
            {
                Sprite.Position = position;
                Sprite.Rotation = (float)rotation;
            }
        }

        public Shape DrawShape()
        {
            return Shape;
        }

        public Sprite DrawSprite()
        {
            return Sprite;
        }
    }
}