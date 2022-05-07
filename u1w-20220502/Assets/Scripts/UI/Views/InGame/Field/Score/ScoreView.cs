using TMPro;
using UnityEngine;

namespace UI.Views.Field.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        /// <summary>
        /// スコアの表示を更新
        /// </summary>
        public void SetScore(int score)
        {
            text.text = score.ToString();
        }
    }
}