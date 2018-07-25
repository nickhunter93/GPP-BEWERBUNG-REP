using SFML.Graphics;

namespace ConsoleApp2
{
    public class WeaponFactory
    {
        public RenderComponent GetSword()
        {
            RenderComponent textureComponent = new RenderComponent(new Texture("Pictures/sword.png"));
            textureComponent.Sprite.Scale = textureComponent.Sprite.Scale * 1.2f;
            //textureComponent.Sprite.Origin = new Vector2D(textureComponent.Sprite.Texture.Size.X / 2, textureComponent.Sprite.Texture.Size.Y);
            //MyConsole.Out(textureComponent.Sprite.Texture.Size + "");
            return textureComponent;
        }

        public RenderComponent GetCrossbow()
        {
            RenderComponent textureComponent = new RenderComponent(new Texture("Pictures/crossbow.png"));
            textureComponent.Sprite.Scale = textureComponent.Sprite.Scale * 2f;
            //textureComponent.Sprite.Origin = new Vector2D(textureComponent.Sprite.Texture.Size.X / 2, textureComponent.Sprite.Texture.Size.Y);
            return textureComponent;
        }

        public RenderComponent GetArrow()
        {
            RenderComponent textureComponent = new RenderComponent(new Texture("Pictures/arrow.png"));
            textureComponent.Sprite.Scale = textureComponent.Sprite.Scale * 1f;
            //textureComponent.Sprite.Origin = new Vector2D(textureComponent.Sprite.Texture.Size.X / 2, textureComponent.Sprite.Texture.Size.Y + 15);
            return textureComponent;
        }
    }
}