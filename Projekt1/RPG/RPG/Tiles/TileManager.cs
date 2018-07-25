using SFML.Graphics;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class TileManager
    {
        private GameObject[,] _tiles = new GameObject[1024, 1024];
        private Vector2D _tileSize = new Vector2D(48, 48);
        private DataManager _dataManager;
        private readonly Vector2D _OFFSET = new Vector2D(512,512);

        public Vector2D TileSize { get => _tileSize;}

        public TileManager()
        {
            _dataManager = DataManager.GetInstance();
        }

        public void AddTile(GameObject tile, Vector2D arrayPosition)
        {
            if (tile == null)
                return;

            _dataManager.BackgroundObjects.Add(tile);
            _dataManager.Environment.SetChild(tile);
            
            tile.transform.Position = (arrayPosition - _OFFSET) * TileSize;
            _tiles[(int) arrayPosition.X, (int) arrayPosition.Y] = tile;
            tile.Update(0);
        }

        public void RemoveTile(Vector2D arrayPosition)
        {
            GameObject tile = _tiles[(int)arrayPosition.X, (int)arrayPosition.Y];
            _dataManager.BackgroundObjects.Remove(tile);
            _dataManager.Environment.RemoveChild(tile);
        }

    }
}