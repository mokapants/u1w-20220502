using DG.Tweening;
using UnityEngine;

namespace InGame.Field.Object
{
    public class MovementObject : MonoBehaviour
    {
        [SerializeField] private Transform pivot;

        private void Start()
        {
            var tmp = pivot.position;
            tmp.x = 9f;
            pivot.position = tmp;

            DOTween.Sequence()
                .Append(pivot.DOMoveX(11f, 3f))
                .Append(pivot.DOMoveX(9f, 3f))
                .SetLoops(-1);
        }
    }
}