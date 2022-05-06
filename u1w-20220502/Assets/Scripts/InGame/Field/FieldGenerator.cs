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
        public TileObject[,] Generate()
        {
            var tiles = new TileObject[fieldRepository.FieldSize, fieldRepository.FieldSize];

            var offset = new Vector3(0f, 0f, fieldRepository.TileDistance);
            for (var z = 0; z < fieldRepository.FieldSize; z++)
            {
                for (var x = 0; x < fieldRepository.FieldSize; x++)
                {
                    var position = new Vector3(x * fieldRepository.TileDistance, z * fieldRepository.TileDistance, 0f);
                    var tileType = fieldRepository.GetTileType(x, fieldRepository.FieldSize - 1 - z);
                    var prefab = fieldRepository.GetTileObjectPrefab(tileType);
                    tiles[x, z] = Instantiate(prefab, position + offset, Quaternion.identity, transform);
                    tiles[x, z].Init(x, z, tileType);
                }
            }

            return tiles;
        }
    }
}