using System.Collections.Generic;

namespace ConsoleApp2
{
    public abstract class AiScript : Script
    {
        private States _state = States.idle;
        protected List<GameObject> _players;
        protected double _viewRange;
        protected double _attackTimer = 0;
        protected double _attackInterval;

        public States State { get => _state; set => _state = value; }

        public enum States
        {
            idle,
            move,
            attack
        }

        public AiScript(List<GameObject> players)
        {
            _players = players;
        }
    }
}