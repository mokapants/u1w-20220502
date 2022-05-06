using Data.ScriptableObjects.Field;
using Data.ScriptableObjects.Stage;
using InGame.Ball;
using InGame.Core;
using InGame.Field;
using InGame.Player;
using Repositories.Core;
using Repositories.Field;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private StageMasterData stageMasterData;
        [SerializeField] private FieldMasterData fieldMasterData;
        [SerializeField] private FieldPartsMasterData fieldPartsMasterData;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private FieldManager fieldManager;
        [SerializeField] private FieldGenerator fieldGenerator;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerAction playerAction;
        [SerializeField] private BallManager ballManager;
        [SerializeField] private BallDropper ballDropper;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(stageMasterData);
            builder.RegisterComponent(fieldMasterData);
            builder.RegisterComponent(fieldPartsMasterData);

            builder.Register<GameRepository>(Lifetime.Singleton);
            builder.Register<FieldRepository>(Lifetime.Singleton);

            builder.RegisterComponent(gameManager);
            builder.RegisterComponent(fieldManager);
            builder.RegisterComponent(fieldGenerator);
            builder.RegisterComponent(playerManager);
            builder.RegisterComponent(playerController);
            builder.RegisterComponent(playerAction);
            builder.RegisterComponent(ballManager);
            builder.RegisterComponent(ballDropper);
        }
    }
}