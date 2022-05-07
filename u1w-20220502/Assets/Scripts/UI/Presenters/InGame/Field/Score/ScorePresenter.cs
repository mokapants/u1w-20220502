using System;
using InGame.Core;
using UI.Views.Field.Score;
using UniRx;
using UnityEngine;
using VContainer;

namespace UI.Presenters.Field.Score
{
    public class ScorePresenter : MonoBehaviour
    {
        private GameManager gameManager;
        [SerializeField] private ScoreView scoreView;

        [Inject]
        public void Constructor(
            GameManager gameManager
        )
        {
            this.gameManager = gameManager;
        }

        private void Start()
        {
            gameManager.ScoreProperty.Subscribe(scoreView.SetScore).AddTo(this);
        }
    }
}