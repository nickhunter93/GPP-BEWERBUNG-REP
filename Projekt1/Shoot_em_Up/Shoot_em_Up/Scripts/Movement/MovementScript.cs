namespace ConsoleApp2
{
    public abstract class MovementScript : Script
    {
        private double _speed = 1;
        private int _id = 0;

        public double Speed { get => _speed; set => _speed = value; }
        public int Id { get => _id; set => _id = value; }

        public abstract MovementScript Clone();
    }
}