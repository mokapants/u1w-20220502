using System;
using System.IO;
using Data.Enum.Field;
using Data.ValueObjects.Field;
using UnityEngine;

namespace InGame.Field
{
    public class FieldMaker : MonoBehaviour
    {
        private void Start()
        {
            var fieldData = new FieldData();
            fieldData.fieldSize = 4;
            fieldData.startPointX = 3;
            fieldData.startPointZ = 3;
            // データだけ[z, x]の扱い
            fieldData.fieldTileDataArray = new []
            {
                new FieldTileData(new [] {TileType.Wall, TileType.Wall, TileType.Wall, TileType.Wall}),
                new FieldTileData(new [] {TileType.None, TileType.Goal, TileType.Ground, TileType.Ground}),
                new FieldTileData(new [] {TileType.Wall, TileType.Wall, TileType.Door, TileType.Wall}),
                new FieldTileData(new [] {TileType.Ground, TileType.Ground, TileType.Ground, TileType.Key})
            };

            var path = $"{Application.dataPath}/Res/Master/Stage/Stage1.json";
            File.WriteAllText(path, JsonUtility.ToJson(fieldData));
            Debug.Log(path);
        }
    }
}