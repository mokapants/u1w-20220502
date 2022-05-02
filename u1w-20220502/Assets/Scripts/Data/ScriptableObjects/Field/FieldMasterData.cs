using UnityEngine;

namespace Data.ScriptableObjects.Field
{
    [CreateAssetMenu(fileName = "FieldMasterData", menuName = "mokapants/FieldMasterData", order = 0)]
    public class FieldMasterData : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private int fieldSize;
        [SerializeField] private float tileDistance;
        [SerializeField] private int minTilePoint;
        [SerializeField] private int maxTilePoint;

        public int FieldSize => fieldSize;
        public float TileDistance => tileDistance;
        public int MinTilePoint => minTilePoint;
        public int MaxTilePoint => maxTilePoint;


        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            if (maxTilePoint < minTilePoint)
            {
                (minTilePoint, maxTilePoint) = (maxTilePoint, minTilePoint);
            }
        }
    }
}