using InGame.Core;
using UI.Views.InGame.Field.Timer;
using UniRx;
using UnityEngine;
using VContainer;

namespace UI.Presenters.InGame.Field.Timer
{
    public class TimerPresenter : MonoBehaviour
    {
        private GameManager gameManager;
        [SerializeField] private TimerView timerView;

        [Inject]
        public void Constructor(
            GameManager gameManager
        )
        {
            this.gameManager = gameManager;
        }

        private void Start()
        {
            gameManager.ElapsedTimeProperty.Subscribe(timerView.SetTimer).AddTo(this);
        }
    }
}