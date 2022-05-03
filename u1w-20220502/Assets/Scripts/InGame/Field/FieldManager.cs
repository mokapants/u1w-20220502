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
        private Tile[,] tiles;
        private Subject<Tile>[,] tileSubjects;

        // イベント
        public IObservable<Tile>[,] TileObservable => tileSubjects;

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
            // 初期化
            tileSubjects = new Subject<Tile>[fieldRepository.FieldSize, fieldRepository.FieldSize];
            for (var i = 0; i < fieldRepository.FieldSize; i++)
            {
                for (var k = 0; k < fieldRepository.FieldSize; k++)
                {
                    tileSubjects[i, k] = new Subject<Tile>();
                }
            }

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

        /// <summary>
        /// タイルを取得
        /// </summary>
        public Tile GetTile(int x, int z)
        {
            return IsExistsTile(x, z) ? tiles[x, z] : null;
        }

        /// <summary>
        /// タイルの情報を設定
        /// </summary>
        public void SetTile(int x, int z, Tile tile)
        {
            if (!IsExistsTile(x, z)) return;

            tiles[x, z] = tile;
            tileSubjects[x, z].OnNext(tile);
        }
    }
}