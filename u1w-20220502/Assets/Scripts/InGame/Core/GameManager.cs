using System;
using UniRx;
using UnityEngine;

namespace InGame.Core
{
    public class GameManager : MonoBehaviour
    {
        private bool isPlaying = true;
        private ReactiveProperty<int> scoreProperty;
        private ReactiveProperty<int> jackPotProperty;
        private Subject<int> onJackPotSubject;

        // イベント
        public IReadOnlyReactiveProperty<int> ScoreProperty => scoreProperty;
        public IReadOnlyReactiveProperty<int> JackPotProperty => jackPotProperty;
        public IObservable<int> OnJackPotObservable => onJackPotSubject;

        // プロパティ
        public bool IsPlaying => isPlaying;
        public int Score => scoreProperty.Value;
        public int JackPotScore => jackPotProperty.Value;

        private void Awake()
        {
            scoreProperty = new ReactiveProperty<int>(0);
            jackPotProperty = new ReactiveProperty<int>(0);
            onJackPotSubject = new Subject<int>();
        }

        private void Start()
        {
            // スコアがゾロ目の時にジャックポットが起きる
            scoreProperty.Subscribe(score =>
            {
                if (IsSameNumbers(score))
                {
                    DoJackPot();
                }
            }).AddTo(this);
        }

        /// <summary>
        /// 数字がゾロ目か判定する
        /// </summary>
        private bool IsSameNumbers(int number)
        {
            var str = number.ToString();
            if (str.Length <= 1) return false;
            for (var i = 1; i < str.Length; i++)
            {
                if (str[0] != str[i]) return false;
            }

            return true;
        }

        /// <summary>
        /// ジャックポット
        /// </summary>
        private void DoJackPot()
        {
            onJackPotSubject.OnNext(JackPotScore);
            ResetJackPotPoint();
        }

        /// <summary>
        /// スコアを加算
        /// </summary>
        public void AddScore(int score = 1)
        {
            scoreProperty.Value += score;
        }

        /// <summary>
        /// ジャックポットのポイントを加算
        /// </summary>
        public void AddJackPotPoint(int point = 1)
        {
            jackPotProperty.Value += point;
        }

        /// <summary>
        /// ジャックポットのポイントをリセット
        /// </summary>
        public void ResetJackPotPoint()
        {
            jackPotProperty.Value = 0;
        }
    }
}