using System;
using Cysharp.Threading.Tasks;
using Game.Base.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Title.Core
{
    public class TitleManager : CoreManager
    {
        private bool isStartable;
        private bool isInputtedAnyKeyDown;

        // SE
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip gameStartClip;

        protected override void Start()
        {
            base.Start();

            WaitScroll().Forget();
        }

        private void Update()
        {
            if (isInputtedAnyKeyDown || !isStartable) return;
            if (Input.anyKeyDown)
            {
                OnAnyKeyDown().Forget();
            }
        }

        private async UniTask WaitScroll()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

            isStartable = true;
        }

        private async UniTask OnAnyKeyDown()
        {
            if (gameStartClip != null)
            {
                audioSource.PlayOneShot(gameStartClip);
            }

            isInputtedAnyKeyDown = true;
            PlayLoadAnySceneAnimation();

            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

            SceneManager.LoadScene("InGame");
        }
    }
}