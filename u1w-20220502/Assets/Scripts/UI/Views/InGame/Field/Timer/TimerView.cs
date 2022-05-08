using TMPro;
using UnityEngine;

namespace UI.Views.InGame.Field.Timer
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        /// <summary>
        /// タイマーの表示を更新
        /// </summary>
        public void SetTimer(float score)
        {
            text.text = $"{score:0.0} sec";
        }
    }
}