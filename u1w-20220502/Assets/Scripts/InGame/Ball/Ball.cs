using System;
using InGame.Core;
using InGame.Field.Panel;
using UnityEngine;
using VContainer;

namespace InGame.Ball
{
    public class Ball : MonoBehaviour
    {
        private GameManager gameManager;
        private BallManager ballManager;
        [SerializeField] private Rigidbody ballRigidbody;

        [Inject]
        public void Constructor(
            GameManager gameManager,
            BallManager ballManager
        )
        {
            this.gameManager = gameManager;
            this.ballManager = ballManager;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Goal"))
            {
                OnCollisionEnterGoal();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Panel"))
            {
                OnTriggerEnterPanel(other.gameObject);
            }
        }

        /// <summary>
        /// ボールの状態を設定
        /// </summary>
        public void SetStatus(bool isActive)
        {
            ballRigidbody.isKinematic = !isActive;
            ballRigidbody.velocity = isActive ? ballRigidbody.velocity : Vector3.zero;
            ballRigidbody.position = isActive ? ballRigidbody.position : new Vector3(-1000, -1000, 0);
        }

        /// <summary>
        /// ボールの位置を設定
        /// </summary>
        public void SetPosition(Vector3 position)
        {
            ballRigidbody.position = position;
        }

        /// <summary>
        /// 倍率のパネルに衝突したときに呼ばれる
        /// </summary>
        private void OnTriggerEnterPanel(GameObject panelObject)
        {
            var panel = panelObject.GetComponent<Panel>();
            ballManager.SetBall(panel.BallGeneratePoint, panel.Multiply - 1);
        }

        /// <summary>
        /// ゴールと衝突したときに呼ばれる
        /// </summary>
        private void OnCollisionEnterGoal()
        {
            gameManager.AddScore();
            ballManager.EnqueueBall(this);
        }
    }
}