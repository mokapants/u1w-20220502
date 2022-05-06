using System;
using System.IO;
using Data.Enum.Field;
using Data.ValueObjects.Field;
using UnityEngine;
using TT = Data.Enum.Field.TileType;

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
            // fieldData.fieldTileDataArray = new []
            // {
                // new FieldTileData(new [] {TT.Wall, TT.Wall, TT.Wall, TT.Wall}),
                // new FieldTileData(new [] {TT.None, TT.Goal, TT.Ground, TT.Ground}),
                // new FieldTileData(new [] {TT.Wall, TT.Wall, TT.Door, TT.Wall}),
                // new FieldTileData(new [] {TT.Ground, TT.Ground, TT.Ground, TT.Key})
            // };

            var path = $"{Application.dataPath}/Res/Master/Stage/Stage1.json";
            File.WriteAllText(path, JsonUtility.ToJson(fieldData));
            Debug.Log(path);
        }
    }
}