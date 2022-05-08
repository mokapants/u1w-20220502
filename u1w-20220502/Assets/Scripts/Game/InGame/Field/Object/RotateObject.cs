using System;
using DG.Tweening;
using UnityEngine;

namespace InGame.Field.Object
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] private Transform pivot;

        private void Start()
        {
            pivot.DORotate(new Vector3(0f, 0f, -360f), 10f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }
    }
}