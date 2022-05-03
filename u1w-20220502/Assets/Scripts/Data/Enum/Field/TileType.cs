using System;

namespace Data.Enum.Field
{
    [Serializable]
    public enum TileType
    {
        None = 0,
        Goal = 1,
        Key = 2,
        Ground = 10,
        Wall = 50,
        Door = 100,
        Bridge = 101
    }
}