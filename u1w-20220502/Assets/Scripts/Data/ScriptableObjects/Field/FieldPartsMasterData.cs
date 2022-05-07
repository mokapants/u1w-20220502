using System;
using System.Collections.Generic;
using Data.Enum.Field;
using InGame.Field;
using UnityEngine;

namespace Data.ScriptableObjects.Field
{
    [CreateAssetMenu(fileName = "FieldPartsMasterData", menuName = "mokapants/FieldPartsMasterData", order = 0)]
    public class FieldPartsMasterData : ScriptableObject
    {
        [SerializeField] private List<FieldParts> fieldPartsList;

        // プロパティ
        private List<FieldParts> copyFieldPartsList = new List<FieldParts>();

        public List<FieldParts> FieldPartsList
        {
            get
            {
                if (copyFieldPartsList.Count != fieldPartsList.Count)
                {
                    copyFieldPartsList = new List<FieldParts>();
                    for (var i = 0; i < fieldPartsList.Count; i++)
                    {
                        copyFieldPartsList.Add(fieldPartsList[i].Copy());
                    }
                }

                return copyFieldPartsList;
            }
        }
    }

    [Serializable]
    public class FieldParts
    {
        public TileType TileType;
        public TileObject TileObject;

        public FieldParts(TileType tileType, TileObject tileObject)
        {
            TileType = tileType;
            TileObject = tileObject;
        }

        public FieldParts Copy()
        {
            return new FieldParts(TileType, TileObject);
        }
    }
}