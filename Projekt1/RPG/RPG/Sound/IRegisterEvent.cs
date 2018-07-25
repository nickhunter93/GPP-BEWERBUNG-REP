using System;

namespace ConsoleApp2
{
    public interface IRegisterEvent
    {
        event EventHandler Play;
        void OnPlay(String sound);
    }
}