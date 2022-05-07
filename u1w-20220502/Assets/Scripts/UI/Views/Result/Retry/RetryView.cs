using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Views.Result.Retry
{
    public class RetryView : MonoBehaviour
    {
        public void OnClickRetryButton()
        {
            SceneManager.LoadScene("InGame");
        }
    }
}