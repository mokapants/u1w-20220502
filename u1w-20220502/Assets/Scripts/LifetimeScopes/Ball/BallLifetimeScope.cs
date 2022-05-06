using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LifetimeScopes.Ball
{
    public class BallLifetimeScope : LifetimeScope
    {
        [SerializeField] private InGame.Ball.Ball ball;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(ball);
        }
    }
}