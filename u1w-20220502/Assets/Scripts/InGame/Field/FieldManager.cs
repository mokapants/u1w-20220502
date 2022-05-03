using System;
using Data.ValueObjects.Field;
using Repositories.Field;
using UniRx;
using UnityEngine;
using VContainer;

namespace InGame.Field
{
    public class FieldManager : MonoBehaviour
    {
        private FieldRepository fieldRepository;
        private FieldGenerator fieldGenerator;
        private TileObject[,] tileObjects;

        [Inject]
        public void Constructor(
            FieldRepository fieldRepository,
            FieldGenerator fieldGenerator
        )
        {
            this.fieldRepository = fieldRepository;
            this.fieldGenerator = fieldGenerator;
        }

        private void Start()
        {
            tileObjects = fieldGenerator.Generate();
        }

        /// <summary>
        /// 指定した座標にタイルが存在するかどうか
        /// </summary>
        public bool IsExistsTile(int x, int z)
        {
            // フィールドの範囲内かどうか
            var isRangeX = 0 <= x && x < tileObjects.GetLength(0);
            var isRangeZ = 0 <= z && z < tileObjects.GetLength(1);
            if (!(isRangeX && isRangeZ)) return false;
            
            // 範囲内
            // TODO
            // 移動できるタイルかどうか
            return true;
        }
    }
}