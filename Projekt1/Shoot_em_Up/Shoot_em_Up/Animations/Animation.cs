namespace ConsoleApp2
{
    public abstract class Animation
    {
        protected double _durationLeft;
        protected double _timeOfStart;



        public double DurationLeft { get => _durationLeft; }


        public abstract void Update(double elapsedTime);

    }

}