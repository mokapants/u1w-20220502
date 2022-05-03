using UnityEngine;

namespace Data.ScriptableObjects.Field
{
    [CreateAssetMenu(fileName = "FieldMasterData", menuName = "mokapants/FieldMasterData", order = 0)]
    public class FieldMasterData : ScriptableObject
    {
        [SerializeField] private float tileDistance;

        public float TileDistance => tileDistance;
    }
}