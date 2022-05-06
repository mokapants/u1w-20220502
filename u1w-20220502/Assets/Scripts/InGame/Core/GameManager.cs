using UnityEngine;

namespace InGame.Core
{
    public class GameManager : MonoBehaviour
    {
        private bool isPlaying = true;
        private int score;

        // プロパティ
        public bool IsPlaying => isPlaying;
        public int Score => score;

        public void AddScore()
        {
            score++;
            Debug.Log($"SCORE {Score}");
        }
    }
}