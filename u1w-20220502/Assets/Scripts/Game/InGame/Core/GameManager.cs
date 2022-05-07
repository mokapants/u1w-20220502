using System;
using Cysharp.Threading.Tasks;
using Repositories.Core;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace InGame.Core
{
    public class GameManager : MonoBehaviour
    {
        private GameRepository gameRepository;
        private bool isPlaying = true;
        private float elapsedTime;
        private ReactiveProperty<int> scoreProperty;
        private ReactiveProperty<int> jackPotProperty;
        private Subject<int> onJackPotSubject;
        private static readonly int MaxScore = 100;

        // イベント
        public IReadOnlyReactiveProperty<int> ScoreProperty => scoreProperty;
        public IReadOnlyReactiveProperty<int> JackPotProperty => jackPotProperty;
        public IObservable<int> OnJackPotObservable => onJackPotSubject;

        // プロパティ
        public bool IsPlaying => isPlaying;
        public int Score => scoreProperty.Value;
        public int JackPotScore => jackPotProperty.Value;

        [Inject]
        public void Constructor(
            GameRepository gameRepository
        )
        {
            this.gameRepository = gameRepository;
        }

        private void Awake()
        {
            scoreProperty = new ReactiveProperty<int>(0);
            jackPotProperty = new ReactiveProperty<int>(0);
            onJackPotSubject = new Subject<int>();
        }

        private void Start()
        {
            // タイムを初期化
            elapsedTime = 0f;

            // スコアがゾロ目の時にジャックポットが起きる
            scoreProperty.Subscribe(score =>
            {
                if (IsSameNumbers(score))
                {
                    DoJackPot();
                }
            }).AddTo(this);

            // スコアが100,000に達したら終了
            scoreProperty.Subscribe(score =>
            {
                if (!isPlaying) return;
                if (MaxScore <= score)
                {
                    OnFinish();
                }
            });
        }

        private void Update()
        {
            if (!IsPlaying) return;

            elapsedTime += Time.deltaTime;
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
        /// 設定されたスコアに到達したとき
        /// </summary>
        private async UniTask OnFinish()
        {
            isPlaying = false;
            gameRepository.SaveScore(elapsedTime);
            
            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            SceneManager.LoadScene("Result");
        }

        /// <summary>
        /// スコアを加算
        /// </summary>
        public void AddScore(int score = 1)
        {
            scoreProperty.Value = Mathf.Clamp(Score + score, 0, MaxScore);
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