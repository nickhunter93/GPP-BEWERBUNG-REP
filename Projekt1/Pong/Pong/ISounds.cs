namespace ConsoleApp2
{
    public interface ISounds
    {
        void Notify(SoundManager.SoundNumbers i);

        void Attach(SoundObserver observer);

        void Detach(SoundObserver observer);
    }
}