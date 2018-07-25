using System;

namespace ConsoleApp2
{
    public interface ISoundEvent
    {
        event EventHandler Play;
        void OnPlay();
    }
}