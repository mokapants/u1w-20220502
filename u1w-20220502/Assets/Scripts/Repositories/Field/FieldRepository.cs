using Data.ScriptableObjects.Field;
using VContainer;

namespace Repositories.Field
{
    public class FieldRepository
    {
        private readonly FieldMasterData fieldMasterData;

        // プロパティ
        public int FieldSize => fieldMasterData.FieldSize;
        public float TileDistance => fieldMasterData.TileDistance;
        public int MinTilePoint => fieldMasterData.MinTilePoint;
        public int MaxTilePoint => fieldMasterData.MaxTilePoint;

        [Inject]
        public FieldRepository(
            FieldMasterData fieldMasterData
        )
        {
            this.fieldMasterData = fieldMasterData;
        }
    }
}