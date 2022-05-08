using System;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Ball
{
    public class BallManager : MonoBehaviour
    {
        [SerializeField] private Ball ballPrefab;
        [SerializeField] private Transform ballPoolParent;
        private Queue<Ball> ballPool;

        private void Start()
        {
            InitBall();
        }

        private void InitBall()
        {
            var ballNum = 500;
            ballPool = new Queue<Ball>();
            for (var i = 0; i < ballNum; i++)
            {
                var ball = Instantiate(ballPrefab, new Vector3(-1000, -1000, 0), Quaternion.identity, ballPoolParent);
                ball.SetStatus(false);
                ballPool.Enqueue(ball);
            }
        }

        /// <summary>
        /// ボールをプールにためる
        /// </summary>
        public void EnqueueBall(Ball ball)
        {
            ball.SetStatus(false);
            ballPool.Enqueue(ball);
        }

        /// <summary>
        /// ボールを特定の場所に呼び出す
        /// </summary>
        public Ball SetBall(Vector3 position)
        {
            if (ballPool.Count <= 0) return null;
            var ball = ballPool.Dequeue();
            ball.SetStatus(true);
            ball.SetPosition(position);
            return ball;
        }
    }
}