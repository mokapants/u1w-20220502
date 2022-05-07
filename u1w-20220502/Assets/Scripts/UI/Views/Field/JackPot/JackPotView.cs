using TMPro;
using UnityEngine;

namespace UI.Views.Field.JackPot
{
    public class JackPotView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        /// <summary>
        /// ジャックポットのスコアの表示を更新
        /// </summary>
        public void SetScore(int score)
        {
            text.text = score.ToString();
        }
    }
}