using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class EstusScript : Script, IRegisterEvent
    {
        private int _estusCount;
        private double _estusLife;

        public EstusScript(int estusCount, double estusLife)
        {
            _estusCount = estusCount;
            _estusLife = estusLife;
            MessageBus.RegisterEvent(this);
        }

        public event EventHandler Play;

        public void OnPlay(String sound)
        {
            if (sound == "estusdrink")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Estusdrink));
            else if (sound == "estusempty")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Estusempty));
        }

        public int EstusCount { get => _estusCount; set => _estusCount = value; }
        
        public void UseEstus()
        {
            if (_estusCount > 0)
            {
                _parent.GetScript<CharacterScript>().Life += _estusLife;
                _estusCount--;
                OnPlay("estusdrink");
            }
            else
            {
                OnPlay("estusempty");
            }
        }
    }
}