using System;
using Data.Enum.Field;

namespace Data.ValueObjects.Field
{
    [Serializable]
    public class FieldData
    {
        public int fieldSize;
        public int startPointX;
        public int startPointZ;
        public FieldTileData[] fieldTileDataArray; // データだけ[z, x]の扱い
    }

    [Serializable]
    public class FieldTileData
    {
        public TileType[] tiles;

        public FieldTileData(TileType[] tiles)
        {
            this.tiles = tiles;
        }
    }
}