using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Crossbow : WeaponScript
    {
        List<GameObject> _addToEnvironment;
        List<GameObject> _arrows;

        public Crossbow(List<GameObject> addToEnvironment, List<GameObject> arrows, List<GameObject> friendly)
        {
            _addToEnvironment = addToEnvironment;
            _arrows = arrows;
            Friendly = friendly;
        }

        public override void Update(double elapsedTime)
        {
            
        }

        public override void Attack()
        {

            GameObject arrow = new GameObject(_parent.transform.Position);
            arrow.transform.Rotation = _parent.transform.Rotation;
            RenderComponent textureComponent = WeaponCreator.GetArrow();
            textureComponent.Sprite.Scale = _parent.GetScripts<CharacterScript>()[0].Weapon.GetComponent<RenderComponent>().Sprite.Scale / 2;
            arrow.AddComponent(textureComponent);
            Vector2D size = new Vector2D(textureComponent.Sprite.TextureRect.Height, textureComponent.Sprite.TextureRect.Width);
            RectangleCollider rc = new RectangleCollider(size * textureComponent.Sprite.Scale,false, 200);
            arrow.AddComponent(rc);
            

            Vector2D direction = Vector2D.Up().Rotate(arrow.transform.Rotation * (Math.PI / 180));

            arrow.AddScript(new ArrowScript(direction, 2, Friendly));
            arrow.transform.Position += direction.Normalize().Orthogonal() * 25;

            _addToEnvironment.Add(arrow);
            _arrows.Add(arrow);
        }

        public override RenderComponent GetTextureComponent()
        {
            return WeaponCreator.GetCrossbow();
        }
    }
}