using System;
using Cysharp.Threading.Tasks;
using InGame.Core;
using UnityEngine;
using VContainer;

namespace InGame.Ball
{
    public class BallDropper : MonoBehaviour
    {
        private GameManager gameManager;
        private BallManager ballManager;
        [SerializeField] private Transform ballDropPoint;
        [SerializeField] private float ballDropInterval;

        // プロパティ
        private Vector3 BallDropPoint => ballDropPoint.position;

        [Inject]
        public void Constructor(
            GameManager gameManager,
            BallManager ballManager
        )
        {
            this.gameManager = gameManager;
            this.ballManager = ballManager;
        }

        private void Start()
        {
            AutoDropBallTask().Forget();
        }

        /// <summary>
        /// ボールを自動で排出する
        /// </summary>
        private async UniTask AutoDropBallTask()
        {
            while (gameManager.IsPlaying)
            {
                ballManager.SetBall(BallDropPoint);
                await UniTask.Delay(TimeSpan.FromSeconds(ballDropInterval));
            }
        }
    }
}