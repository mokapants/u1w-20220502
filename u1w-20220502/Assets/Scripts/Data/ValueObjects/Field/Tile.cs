using Data.Enum.Field;

namespace Data.ValueObjects.Field
{
    public class Tile
    {
        public int Point;
        public TileState TileState;

        public Tile(int point, TileState tileState)
        {
            Point = point;
            TileState = tileState;
        }

        public Tile(int point)
        {
            Point = point;
            TileState = TileState.None;
        }
    }
}