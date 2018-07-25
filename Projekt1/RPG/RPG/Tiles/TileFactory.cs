using SFML.Graphics;

namespace ConsoleApp2
{
    public class TileFactory
    {

        public enum TileType
        {
            nothing,
            wall
        }

        public TileFactory()
        {

        }

        public GameObject CreateTile(TileType tileType, int tileNumber)
        {
            switch (tileType)
            {
                case TileType.wall:
                    return CreateWall(tileNumber);
                default:
                    return null;
            }
        }

        private GameObject CreateWall(int tileNumber)
        {
            GameObject wallTile = new GameObject();
            tileNumber--;
            int x = ((int)(tileNumber % 4)) * 48;
            int y = ((int)(tileNumber / 4)) * 48;

            IntRect intRect = new IntRect(new Vector2D(x, y), new Vector2D(48, 48));
            RenderComponent renderComponent = new RenderComponent(new Texture("TileAssets/wall.png", intRect));
            wallTile.AddComponent(renderComponent);
            RectangleCollider rectangleCollider = new RectangleCollider(new Vector2D(48, 48), true);
            wallTile.AddComponent(rectangleCollider);
            return wallTile;
        }
    }
}