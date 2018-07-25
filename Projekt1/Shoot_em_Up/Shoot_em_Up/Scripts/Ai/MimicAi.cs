using System.Collections.Generic;

namespace ConsoleApp2
{
    public class MimicAi : AiScript
    {
        private GameObject _target = null;

        public MimicAi(List<GameObject> players) : base(players)
        {
            _viewRange = 40;
        }

        public override void Update(double elapsedTime)
        {
            if (Program.windowState == Program.WindowState.GameOver)
                return;

            bool targetFound = false;

            double minDist = _viewRange;

            foreach (GameObject player in _players)
            {
                double dist = _parent.transform.Position.GetDistance(player.transform.Position);
                if (dist < minDist)
                {
                    _target = player;// player.transform.Position + player.Parent.transform.Position;
                    minDist = dist;
                    targetFound = true;
                }
            }

            if (targetFound)
            {
                State = States.attack;
                SFML.Graphics.Texture texture = new SFML.Graphics.Texture("Pictures/chest_attack.png");
                _parent.GetComponent<RenderComponent>().Sprite.Texture = texture;
                //_parent.GetScripts<EnemyLookScript>()[0].TargetPosition = _target.transform.Position + _target.Parent.transform.Position;

                Vector2D TargetPosition = _target.transform.Position + _target.Parent.transform.Position;
                
                double angle = (TargetPosition - (_parent.transform.Position + _parent.Parent.transform.Position)).GetAngleBetween(Vector2D.Up());

                if (_parent.transform.Position.Y + _parent.Parent.transform.Position.Y < TargetPosition.Y)
                {
                    angle = 180 - angle;
                }
                
                _parent.transform.Rotation = angle;
                
                Program.windowState = Program.WindowState.GameOver;
            }
            else
            {
                State = States.idle;
                //_parent.GetScripts<EnemyLookScript>()[0].IsActive = false;
            }
        }
    }
}