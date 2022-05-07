using Game.Result.Core;
using Repositories.Result.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LifetimeScopes.Result.Core
{
    public class ResultLifetimeScope : LifetimeScope
    {
        [SerializeField] private ResultManager resultManager;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ResultRepository>(Lifetime.Singleton);

            builder.RegisterComponent(resultManager);
        }
    }
}