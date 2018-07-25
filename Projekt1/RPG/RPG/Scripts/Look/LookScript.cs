namespace ConsoleApp2
{
    public abstract class LookScript : Script
    {
        private double _speed;
        protected bool _turning = true;
        public double Speed { get => _speed; set => _speed = value; }
        public bool Turning { get => _turning; }
    }
}