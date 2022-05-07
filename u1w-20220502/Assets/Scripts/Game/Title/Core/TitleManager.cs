using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Title.Core
{
    public class TitleManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("InGame");
            }
        }
    }
}