using Data.ScriptableObjects.Field;
using UnityEngine;

namespace Repositories.Field
{
    public class FieldRepository : MonoBehaviour
    {
        [SerializeField] private FieldMasterData fieldMasterData;

        public int FieldSize => fieldMasterData.FieldSize;
        public float TileDistance => fieldMasterData.TileDistance;
        public int MinTilePoint => fieldMasterData.MinTilePoint;
        public int MaxTilePoint => fieldMasterData.MaxTilePoint;
    }
}