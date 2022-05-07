using InGame.Core;
using UI.Views.Field.JackPot;
using UniRx;
using UnityEngine;
using VContainer;

namespace UI.Presenters.Field.JackPot
{
    public class JackPotPresenter : MonoBehaviour
    {
        private GameManager gameManager;
        [SerializeField] private JackPotView jackPotView;

        [Inject]
        public void Constructor(
            GameManager gameManager
        )
        {
            this.gameManager = gameManager;
        }

        private void Start()
        {
            gameManager.JackPotProperty.Subscribe(jackPotView.SetScore).AddTo(this);
        }
    }
}