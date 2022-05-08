using Data.ScriptableObjects.Field;
using Data.ScriptableObjects.Stage;
using InGame.Ball;
using InGame.Core;
using InGame.Field;
using InGame.Player;
using Repositories.Core;
using Repositories.Field;
using UI.Presenters.Field.JackPot;
using UI.Presenters.Field.Score;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        // Data
        [SerializeField] private StageMasterData stageMasterData;
        [SerializeField] private FieldMasterData fieldMasterData;
        [SerializeField] private FieldPartsMasterData fieldPartsMasterData;

        // InGame
        [SerializeField] private GameManager gameManager;
        [SerializeField] private InGameFieldManager inGameFieldManager;
        [SerializeField] private InGameFieldGenerator inGameFieldGenerator;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerAction playerAction;
        [SerializeField] private BallManager ballManager;
        [SerializeField] private BallDropper ballDropper;

        // UI
        [SerializeField] private ScorePresenter scorePresenter;
        [SerializeField] private JackPotPresenter jackPotPresenter;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(stageMasterData);
            builder.RegisterComponent(fieldMasterData);
            builder.RegisterComponent(fieldPartsMasterData);

            builder.Register<GameRepository>(Lifetime.Singleton);
            builder.Register<FieldRepository>(Lifetime.Singleton);

            builder.RegisterComponent(gameManager);
            builder.RegisterComponent(inGameFieldManager);
            builder.RegisterComponent(inGameFieldGenerator);
            builder.RegisterComponent(playerManager);
            builder.RegisterComponent(playerController);
            builder.RegisterComponent(playerAction);
            builder.RegisterComponent(ballManager);
            builder.RegisterComponent(ballDropper);

            builder.RegisterComponent(scorePresenter);
            builder.RegisterComponent(jackPotPresenter);
        }
    }
}