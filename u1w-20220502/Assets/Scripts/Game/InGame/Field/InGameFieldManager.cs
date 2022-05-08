using System;
using Data.ValueObjects.Field;
using Repositories.Core;
using Repositories.Field;
using UniRx;
using UnityEngine;
using VContainer;

namespace InGame.Field
{
    public class InGameFieldManager : MonoBehaviour
    {
        private GameRepository gameRepository;
        private FieldRepository fieldRepository;
        private InGameFieldGenerator inGameFieldGenerator;
        private TileObject[,] tileObjects;

        [Inject]
        public void Constructor(
            GameRepository gameRepository,
            FieldRepository fieldRepository,
            InGameFieldGenerator inGameFieldGenerator
        )
        {
            this.gameRepository = gameRepository;
            this.fieldRepository = fieldRepository;
            this.inGameFieldGenerator = inGameFieldGenerator;
        }

        private void Awake()
        {
            fieldRepository.LoadFieldData(gameRepository.CurrentStageId);
        }

        private void Start()
        {
            tileObjects = inGameFieldGenerator.Generate();
        }
        
        /// <summary>
        /// 移動可能なタイルかどうか
        /// </summary>
        public bool IsMovableTile(int x, int z)
        {
            // フィールドの範囲内かどうか
            var isRangeX = 0 <= x && x < tileObjects.GetLength(0);
            var isRangeZ = 0 <= z && z < tileObjects.GetLength(1);
            if (!(isRangeX && isRangeZ)) return false;

            // 移動できるタイルかどうか
            if (!tileObjects[x, z].Tile.isWalkable) return false;

            return true;
        }
    }
}