using System.Collections.Generic;

namespace ConsoleApp2
{
    public class StateManager
    {
        private List<State> _states;
        private List<RectangleObject> _players;
        private double[,] _stateActive;

        public StateManager(List<State> states, List<RectangleObject> players)
        {
            _states = states;
            _players = players;
            
            _stateActive = new double[_states.Count, _players.Count];
            
        }    
        

        public void AddState(RectangleObject player, State state, double duration)
        {
            _stateActive[_states.FindIndex(x => x.ID == state.ID), _players.FindIndex(x => x.GetHashCode() == player.GetHashCode())] = duration;

            /*for (int i = 0; i < _states.Count; i++)
            {
                for (int j = 0; j < _players.Count; j++)
                {
                    if (state.ID == _states[i].ID && player.GetHashCode() == _players[j].GetHashCode())
                    {
                        _stateActive[i, j] = duration;
                        return;
                    }
                }
            }*/
        }

        public void Update(double elapsedTime)
        {
            for (int i = 0; i < _states.Count; i++)
            {
                for (int j = 0; j < _players.Count; j++)
                {
                    if(_stateActive[i, j] > 0)
                    {
                        _states[i].Update(_players[j],elapsedTime);
                        _stateActive[i, j] -= elapsedTime;

                        if (_stateActive[i, j] <= 0)
                        {
                            _states[i].Finish(_players[j]);
                        }
                    }
                }
            }
        }
    }
}