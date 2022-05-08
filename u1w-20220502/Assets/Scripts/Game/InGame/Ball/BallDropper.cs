using System;
using Cysharp.Threading.Tasks;
using InGame.Core;
using UniRx;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace InGame.Ball
{
    public class BallDropper : MonoBehaviour
    {
        private GameManager gameManager;
        private BallManager ballManager;
        [SerializeField] private Transform ballDropPoint;
        [SerializeField] private Transform ballDropDirection;
        [SerializeField] private Transform jackPotDropPoint;
        [SerializeField] private Transform jackPotDropDirection;
        [SerializeField] private float ballDropInterval;
        [SerializeField] private float jackPotDropInterval;

        // プロパティ
        private Vector3 BallDropPoint => ballDropPoint.position;
        private Vector3 JackPotDropPoint => jackPotDropPoint.position;
        private Vector3 BallDropDirection => ballDropDirection.position - ballDropPoint.position;
        private Vector3 JackPotDropDirection => jackPotDropDirection.position - jackPotDropPoint.position;

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
            gameManager.OnJackPotObservable.Subscribe(jackPotScore => OnJackPot(jackPotScore).Forget());
        }

        /// <summary>
        /// ボールを自動で排出する
        /// </summary>
        private async UniTask AutoDropBallTask()
        {
            await UniTask.WaitWhile(() => gameManager.IsReady);

            while (gameManager.IsPlaying)
            {
                var ball = ballManager.SetBall(BallDropPoint);
                ball.AddForce(BallDropDirection, Random.Range(14f, 28f));
                await UniTask.Delay(TimeSpan.FromSeconds(ballDropInterval));
            }
        }

        /// <summary>
        /// ジャックポット
        /// </summary>
        private async UniTask OnJackPot(int jackPotScore)
        {
            var droppedBall = 0;
            while (gameManager.IsPlaying && droppedBall < jackPotScore)
            {
                var ball = ballManager.SetBall(JackPotDropPoint);
                ball.AddForce(JackPotDropDirection, Random.Range(16f, 26f));
                droppedBall++;
                await UniTask.Delay(TimeSpan.FromSeconds(jackPotDropInterval));
            }
        }
    }
}