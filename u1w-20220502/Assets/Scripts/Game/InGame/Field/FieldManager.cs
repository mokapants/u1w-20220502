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