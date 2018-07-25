using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace ConsoleApp2
{
    public class PowerUpScript : Script, IRegisterEvent
    {
        private StateManager _stateManager;
        private State _state; 


        public StateManager StateManager
        {
            set { _stateManager = value; }
        }

        public PowerUpScript(State state)
        {
            _state = state;
            MessageBus.RegisterEvent(this);
        }

        public event EventHandler Play;

        public void OnPlay(String sound)
        {
            if (sound == "powerup")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Powerup));
        }

        public void ExecutePowerUp(GameObject rectangleObject)
        {
            _stateManager.AddState(rectangleObject, _state, _state.Duration);
        }

        public PowerUpScript Clone(GameObject gameObjectOfClonedScript)
        {
            PowerUpScript clone = new PowerUpScript(_state);
            clone.StateManager = this._stateManager;
            gameObjectOfClonedScript.AddComponent(new RenderComponent(_state.Texture));
            return clone;
        }

        public override void OnCollide(List<GameObject> collider)
        {
            
            foreach (var item in collider)
            {
                if (item.ObjectName == "MyPlayer")
                {
                    ExecutePowerUp(item);

                    DataManager dataManager = DataManager.GetInstance();
                    dataManager.Environment.RemoveChildLate(_parent);
                    dataManager.PowerUps.Remove(_parent);
                    dataManager.BackgroundObjects.Remove(_parent);
                    
                    OnPlay("powerup");
                }
            }

            
        }

    }
}