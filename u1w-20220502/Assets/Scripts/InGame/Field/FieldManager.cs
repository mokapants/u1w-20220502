using System;
using Data.ValueObjects.Field;
using UnityEngine;
using VContainer;

namespace InGame.Field
{
    public class FieldManager : MonoBehaviour
    {
        private FieldGenerator fieldGenerator;
        private Tile[,] tiles;
        
        [Inject]
        public void Constructor(
            FieldGenerator fieldGenerator
        )
        {
            this.fieldGenerator = fieldGenerator;
        }

        private void Start()
        {
            tiles = fieldGenerator.Generate();
        }

        /// <summary>
        /// 指定した座標にタイルが存在するかどうか
        /// </summary>
        public bool IsExistsTile(int x, int z)
        {
            var isRangeX = 0 <= x && x < tiles.GetLength(0);
            var isRangeZ = 0 <= z && z < tiles.GetLength(1);
            return isRangeX && isRangeZ;
        }
    }
}