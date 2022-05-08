using System;
using Cysharp.Threading.Tasks;
using Game.Base.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Title.Core
{
    public class TitleManager : CoreManager
    {
        private bool isInputtedAnyKeyDown;

        private void Update()
        {
            if (isInputtedAnyKeyDown) return;
            if (Input.anyKeyDown)
            {
                OnAnyKeyDown().Forget();
            }
        }

        private async UniTask OnAnyKeyDown()
        {
            isInputtedAnyKeyDown = true;
            PlayLoadAnySceneAnimation();

            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

            SceneManager.LoadScene("InGame");
        }
    }
}