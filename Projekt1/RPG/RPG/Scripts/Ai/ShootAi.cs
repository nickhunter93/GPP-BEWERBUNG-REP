using System.Collections.Generic;

namespace ConsoleApp2
{
    public class ShootAi : AiScript
    {
        private GameObject _target = null;

        public ShootAi(List<GameObject> players) : base(players)
        {
            _viewRange = 400;
            _attackInterval = 2000;
        }

        public override void Update(double elapsedTime)
        {
            _attackTimer -= elapsedTime;
            

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
                _parent.GetScripts<EnemyLookScript>()[0].TargetPosition = _target.transform.Position + _target.Parent.transform.Position;

                if (_attackTimer < 0 && !_parent.GetScripts<EnemyLookScript>()[0].Turning)
                {
                    _parent.GetScriptsInChilds<Crossbow>()[0].Attack();
                    _attackTimer = _attackInterval;
                }
            }
            else
            {
                State = States.idle;
                _parent.GetScripts<EnemyLookScript>()[0].IsActive = false;
            }
        }

    }
}