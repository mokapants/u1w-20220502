using System;
using Cysharp.Threading.Tasks;
using Game.Base.Core;
using Repositories.Result.Core;
using UI.Views.Result.Retry;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Game.Result.Core
{
    public class ResultManager : CoreManager
    {
        private ResultRepository resultRepository;

        [SerializeField] private RetryView retryView;

        // SE
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip retryClip;

        [Inject]
        public void Constructor(
            ResultRepository resultRepository
        )
        {
            this.resultRepository = resultRepository;
        }

        protected override void Start()
        {
            base.Start();
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(resultRepository.LoadScore());

            retryView.OnClickRetryButtonListener += () => OnClickRetryButton().Forget();
        }

        private async UniTask OnClickRetryButton()
        {
            if (retryClip != null)
            {
                audioSource.PlayOneShot(retryClip);
            }

            PlayLoadAnySceneAnimation();

            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

            SceneManager.LoadScene("InGame");
        }
    }
}