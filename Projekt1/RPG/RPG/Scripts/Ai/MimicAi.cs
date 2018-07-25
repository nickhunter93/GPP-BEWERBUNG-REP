using System.Collections.Generic;

namespace ConsoleApp2
{
    public class MimicAi : AiScript
    {

        public MimicAi(List<GameObject> players) : base(players)
        {
        }

        public void Attack(GameObject player)
        {
            SFML.Graphics.Texture texture = new SFML.Graphics.Texture("Pictures/chest_attack.png");
            _parent.GetComponent<RenderComponent>().Sprite.Texture = texture;

            Vector2D TargetPosition = player.transform.Position + player.Parent.transform.Position;

            double angle = (TargetPosition - (_parent.transform.Position + _parent.Parent.transform.Position)).GetAngleBetween(Vector2D.Up());
            
            _parent.transform.Rotation = angle;

            //new Factory().CreateMimic(_parent.transform.Position, angle);
        }
        
    }
}