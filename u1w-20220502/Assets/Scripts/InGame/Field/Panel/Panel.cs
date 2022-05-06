using UnityEngine;

namespace InGame.Field.Panel
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private int multiply;
        [SerializeField] private Transform ballGeneratePoint;

        // プロパティ
        public int Multiply => multiply;
        public Vector3 BallGeneratePoint => ballGeneratePoint.position;
    }
}