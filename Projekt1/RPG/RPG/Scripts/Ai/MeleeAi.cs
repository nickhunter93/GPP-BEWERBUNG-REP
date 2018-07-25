using System.Collections.Generic;

namespace ConsoleApp2
{
    public class MeleeAi : AiScript
    {
        private GameObject _target = null;
        
        public MeleeAi(List<GameObject> players) : base(players)
        {
            _viewRange = 300;
            _attackInterval = 1000;
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
                if ((_parent.transform.Position + _parent.Parent.transform.Position).GetDistance(_target.transform.Position + _target.Parent.transform.Position) < 100)
                {
                    State = States.attack;
                }
                else
                {
                    State = States.move;
                }


                if (_attackTimer >= 0)
                {
                    State = States.move;
                }
                else
                {
                    if (State == States.attack)
                        _attackTimer = _attackInterval;
                }

                if ((_parent.transform.Position + _parent.Parent.transform.Position).GetDistance(_target.transform.Position + _target.Parent.transform.Position) < 50)
                {
                    State = States.attack;
                }

            }
            else
            {
                State = States.idle;
            }


            if (State == States.idle)
            {
                _parent.GetScripts<EnemyMovementScript>()[0].IsActive = false;
                _parent.GetScripts<EnemyLookScript>()[0].IsActive = false;
            }
            if (State == States.move)
            {
                _parent.GetScripts<EnemyMovementScript>()[0].TargetPosition = _target.transform.Position + _target.Parent.transform.Position;
                _parent.GetScripts<EnemyLookScript>()[0].TargetPosition = _target.transform.Position + _target.Parent.transform.Position;
            }
            if (State == States.attack)
            {
                _parent.GetScripts<EnemyMovementScript>()[0].IsActive = false;

                if (_parent.GetScriptsInChilds<Sword>()[0].Timer < -800)
                {
                    _parent.GetScriptsInChilds<Sword>()[0].Attack();
                }
            }

        }
    }
}