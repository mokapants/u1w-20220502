using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileDecoration : MonoBehaviour
{
    [SerializeField] private List<GameObject> tileDecorationList;

    private void Start()
    {
        var activeTile = Random.Range(0, tileDecorationList.Count);
        for (var i = 0; i < tileDecorationList.Count; i++)
        {
            tileDecorationList[i].SetActive(i == activeTile);
        }
    }
}
