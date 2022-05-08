using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Views.Result.Retry
{
    public class RetryView : MonoBehaviour
    {
        // イベント
        public Action OnClickRetryButtonListener;

        public void OnClickRetryButton()
        {
            OnClickRetryButtonListener?.Invoke();
        }
    }
}