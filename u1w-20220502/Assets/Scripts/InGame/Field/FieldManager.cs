using System;
using Data.ValueObjects.Field;
using UnityEngine;

namespace InGame.Field
{
    public class FieldManager : MonoBehaviour
    {
        [SerializeField] private FieldGenerator fieldGenerator;
        private Tile[,] tiles;

        private void Start()
        {
            tiles = fieldGenerator.Generate();
        }
    }
}