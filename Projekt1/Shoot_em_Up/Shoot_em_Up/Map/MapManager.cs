
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class MapManager
    {
        private DataManager _dataManager;
        private List<Map> _maps = new List<Map>();
        private int _activeMap;

        public MapManager(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public int ActiveMap { get => _activeMap; set => _activeMap = value; }

        public void CreateMap(int i)
        {
            if (i == 0)
            {
                _maps.Add(new Map0(_dataManager));
                ActiveMap = 0;
                _maps[i].CreateEnvironment();
                _maps[i].InitialiseStates();
            }
            if (i == 1)
            {
                _maps.Add(new Map1(_dataManager));
                ActiveMap = 1;
            }
            if (i == 2)
            {
                _maps.Add(new Map2(_dataManager));
                ActiveMap = 2;
            }

            _maps[i].AddBonfires();
            _maps[i].AddChests();
            _maps[i].AddWalls();
            _maps[i].AddPowerUps();
            _maps[i].AddEnemies();
        }

        public Map GetMap(int i)
        {
            return _maps[i];
        }

        public void AddPlayer()
        {

            if (_dataManager.PlayerCount >= 1)
            {
                GetMap(ActiveMap).AddFirstPlayer();
            }
            if (_dataManager.PlayerCount >= 2)
            {
                _dataManager.Players[0].transform.Position = Program.windowSize / 2 - Vector2D.Right() * 50;

                GetMap(ActiveMap).AddSecondPlayer();

                List<MovementScript> playerMovementScripts = _dataManager.Environment.GetScripts<MovementScript>();  //slower env because 2 players

                foreach (MovementScript script in playerMovementScripts)
                {
                    script.Speed = script.Speed / (playerMovementScripts.Count);
                }

            }

            _dataManager.StateManager.Players = _dataManager.Players;
        }


        public void TeleportPlayer(Vector2D position)
        {
            _dataManager.Environment.transform.Position = -position;

            if (_dataManager.PlayerCount >= 1)
            {
                _dataManager.Players[0].transform.Position = Program.windowSize / 2 + position;
            }
            if (_dataManager.PlayerCount >= 2)
            {
                _dataManager.Players[0].transform.Position = Program.windowSize / 2 - Vector2D.Right() * 50 + position;
                _dataManager.Players[1].transform.Position = Program.windowSize / 2 + Vector2D.Right() * 50 + position;
                
            }

        }
    }
}