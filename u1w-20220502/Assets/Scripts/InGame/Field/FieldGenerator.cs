using System;
using Data.ValueObjects.Field;
using Repositories.Field;
using Unity.Mathematics;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace InGame.Field
{
    public class FieldGenerator : MonoBehaviour
    {
        private FieldRepository fieldRepository;
        [SerializeField] private TileObject tileObject;

        [Inject]
        public void Constructor(
            FieldRepository fieldRepository
        )
        {
            this.fieldRepository = fieldRepository;
        }

        /// <summary>
        /// タイルの生成を行う
        /// </summary>
        public Tile[,] Generate()
        {
            var tiles = new Tile[fieldRepository.FieldSize, fieldRepository.FieldSize];

            var tileHalfDistance = fieldRepository.TileDistance / 2f;
            var offset = new Vector3(tileHalfDistance, -1f, tileHalfDistance);
            for (var i = 0; i < fieldRepository.FieldSize; i++)
            {
                for (var k = 0; k < fieldRepository.FieldSize; k++)
                {
                    tiles[i, k] = new Tile(Random.Range(fieldRepository.MinTilePoint, fieldRepository.MaxTilePoint));
                    var position = new Vector3(i * fieldRepository.TileDistance, 0f, k * fieldRepository.TileDistance);
                    var instanceTileObject = Instantiate(tileObject, position + offset, quaternion.identity, transform);
                    instanceTileObject.Init(i, k, tiles[i, k]);
                }
            }

            return tiles;
        }
    }
}