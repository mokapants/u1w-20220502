using System;
using Data.ValueObjects.Field;
using Repositories.Core;
using Repositories.Field;
using UniRx;
using UnityEngine;
using VContainer;

namespace InGame.Field
{
    public class FieldManager : MonoBehaviour
    {
        private GameRepository gameRepository;
        private FieldRepository fieldRepository;
        private FieldGenerator fieldGenerator;
        private TileObject[,] tileObjects;

        [Inject]
        public void Constructor(
            GameRepository gameRepository,
            FieldRepository fieldRepository,
            FieldGenerator fieldGenerator
        )
        {
            this.gameRepository = gameRepository;
            this.fieldRepository = fieldRepository;
            this.fieldGenerator = fieldGenerator;
        }

        private void Awake()
        {
            fieldRepository.LoadFieldData(gameRepository.CurrentStageId);
        }

        private void Start()
        {
            tileObjects = fieldGenerator.Generate();
        }

        /// <summary>
        /// 配列内にタイルが存在するかどうか
        /// </summary>
        public bool IsExistsTile(int x, int z)
        {
            // フィールドの範囲内かどうか
            var isRangeX = 0 <= x && x < tileObjects.GetLength(0);
            var isRangeZ = 0 <= z && z < tileObjects.GetLength(1);
            return isRangeX && isRangeZ;
        }

        /// <summary>
        /// 移動可能なタイルかどうか
        /// </summary>
        public bool IsMovableTile(int x, int z)
        {
            if (!IsExistsTile(x, z)) return false;

            // 移動できるタイルかどうか
            if (!tileObjects[x, z].Tile.isWalkable) return false;

            // 鍵が必要かどうか
            if (tileObjects[x, z].Tile.isNeedKey) return false;

            return true;
        }

        /// <summary>
        /// 鍵が必要なタイルかどうか
        /// </summary>
        public bool IsNeedKeyTile(int x, int z)
        {
            if (!IsExistsTile(x, z)) return false;
            return tileObjects[x, z].Tile.isNeedKey;
        }
    }
}