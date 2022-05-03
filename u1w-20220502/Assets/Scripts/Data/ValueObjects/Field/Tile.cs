using Data.Enum.Field;

namespace Data.ValueObjects.Field
{
    public class Tile
    {
        public int X;
        public int Z;
        public TileType TileType;

        public Tile(int x, int z, TileType tileType)
        {
            X = x;
            Z = z;
            TileType = tileType;
        }
    }
}