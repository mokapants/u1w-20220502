using System;
using System.Collections.Generic;
using Data.Enum.Field;
using Data.ScriptableObjects.Field;
using Data.ScriptableObjects.Stage;
using Data.ValueObjects.Field;
using InGame.Field;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Repositories.Field
{
    public class FieldRepository
    {
        private readonly StageMasterData stageMasterData;
        private readonly FieldMasterData fieldMasterData;
        private readonly FieldPartsMasterData fieldPartsMasterData;
        private FieldData fieldData;
        private Subject<Unit> onLoadedFieldDataSubject;

        // イベント
        public IObservable<Unit> OnLoadedFieldDataObservable => onLoadedFieldDataSubject;

        // プロパティ
        public int FieldSize => fieldData.fieldSize;
        public (int x, int z) PlayerStartPoint => (fieldData.startPointX, fieldData.startPointZ);
        public float TileDistance => fieldMasterData.TileDistance;

        [Inject]
        public FieldRepository(
            StageMasterData stageMasterData,
            FieldMasterData fieldMasterData,
            FieldPartsMasterData fieldPartsMasterData
        )
        {
            this.stageMasterData = stageMasterData;
            this.fieldMasterData = fieldMasterData;
            this.fieldPartsMasterData = fieldPartsMasterData;
        }

        /// <summary>
        /// フィールドデータを取得
        /// </summary>
        public void LoadFieldData(int stageId)
        {
            var data = stageMasterData.StageDataList[stageId];
            fieldData = JsonUtility.FromJson<FieldData>(data.text);
        }

        /// <summary>
        /// 指定した座標にタイルが存在するかどうか
        /// </summary>
        private bool IsExistsTile(int x, int z)
        {
            var isRangeX = 0 <= x && x < fieldData.fieldTileDataArray[0].tiles.Length;
            var isRangeZ = 0 <= z && z < fieldData.fieldTileDataArray.Length;
            return isRangeX && isRangeZ;
        }

        /// <summary>
        /// タイルの種類を取得
        /// </summary>
        public TileType GetTileType(int x, int z)
        {
            return IsExistsTile(x, z) ? fieldData.fieldTileDataArray[z].tiles[x] : TileType.None;
        }

        /// <summary>
        /// タイルの種類からプレハブを取得
        /// </summary>
        public TileObject GetTileObjectPrefab(TileType tileType)
        {
            foreach (var fieldParts in fieldPartsMasterData.FieldPartsList)
            {
                if (fieldParts.TileType == tileType) return fieldParts.TileObject;
            }

            return null;
        }
    }
}