using SFML.Graphics;
using SFML.System;

namespace ConsoleApp2
{
    public class PowerUpScript : Script
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
        
    }
}