﻿using Data.ScriptableObjects.Field;
using InGame.Field;
using InGame.Player;
using Repositories.Field;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private string fieldDataPath;
        [SerializeField] private FieldMasterData fieldMasterData;
        [SerializeField] private FieldPartsMasterData fieldPartsMasterData;
        [SerializeField] private FieldManager fieldManager;
        [SerializeField] private FieldGenerator fieldGenerator;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerAction playerAction;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(fieldMasterData);
            builder.RegisterComponent(fieldPartsMasterData);
            
            builder.Register<FieldRepository>(Lifetime.Singleton).WithParameter(fieldDataPath);

            builder.RegisterComponent(fieldManager);
            builder.RegisterComponent(fieldGenerator);
            builder.RegisterComponent(playerManager);
            builder.RegisterComponent(playerController);
            builder.RegisterComponent(playerAction);
        }
    }
}