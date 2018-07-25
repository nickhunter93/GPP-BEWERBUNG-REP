using SFML.Graphics;
using SFML.System;
using System;

namespace ConsoleApp2
{
    public class TextureComponent : Component
    {
        private Sprite _sprite;
        private Vector2D _size;

        private Vector2D _position;
        private double _rotation;

        public TextureComponent(Texture texture)
        {
            _sprite = new Sprite(texture);
            this.Size = (Vector2f)_sprite.Texture.Size;
            _sprite.Origin = new Vector2D(this.Size.X / 2, this.Size.Y / 2);
            _position = Vector2D.Zero();
            Sprite.Position = Vector2D.Zero();
        }

        public Vector2D Size { get => _size; set => _size = value; }
        public Sprite Sprite { get => _sprite; set => _sprite = value; }

        
        

        public override void Update(double elapsedTime)
        {
            GameObject parent = this.Parent;
            GameObject grandParent = null;
            Vector2D position = Vector2D.Zero();// parent.transform.Position;
            double rotation = 0;// parent.transform.Rotation;
            while (parent != null)
            {
                //position += parent.transform.Position;
                if (grandParent == null)
                    position += parent.transform.Position;
                else
                {
                    position = position.Rotate((Math.PI / 180) * parent.transform.Rotation) + parent.transform.Position;
                    //position += parent.transform.Position;
                    //position = position.RotateAround((Math.PI / 180) * parent.transform.Rotation, parent.transform.Position);
                }
                rotation += parent.transform.Rotation;
                grandParent = parent;
                parent = parent.Parent;
            }
            Sprite.Position = position;
            Sprite.Rotation = (float)rotation;
        }

        public Sprite Draw()
        {
            return Sprite;
        }
    }
}