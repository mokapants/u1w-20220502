using System;
using Cysharp.Threading.Tasks;
using InGame.Core;
using UniRx;
using UnityEngine;
using VContainer;

namespace InGame.Ball
{
    public class BallDropper : MonoBehaviour
    {
        private GameManager gameManager;
        private BallManager ballManager;
        [SerializeField] private Transform ballDropPoint;
        [SerializeField] private Transform jackPotDropPoint;
        [SerializeField] private float ballDropInterval;
        [SerializeField] private float jackPotDropInterval;

        // プロパティ
        private Vector3 BallDropPoint => ballDropPoint.position;
        private Vector3 JackPotDropPoint => jackPotDropPoint.position;

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
                ballManager.SetBall(BallDropPoint);
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
                ballManager.SetBall(JackPotDropPoint);
                droppedBall++;
                await UniTask.Delay(TimeSpan.FromSeconds(jackPotDropInterval));
            }
        }
    }
}