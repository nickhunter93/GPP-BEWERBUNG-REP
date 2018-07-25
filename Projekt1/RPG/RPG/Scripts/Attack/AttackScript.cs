using SFML.Window;

namespace ConsoleApp2
{
    public abstract class AttackScript : Script
    {
        protected double _attackTimer = 0;
        protected double _attackInterval;

        public double AttackInterval { get => _attackInterval; set => _attackInterval = value; }
    }
}