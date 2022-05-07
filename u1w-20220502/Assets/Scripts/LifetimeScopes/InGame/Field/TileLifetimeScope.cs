using InGame.Field;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LifetimeScopes.Field
{
    /// <summary>
    /// タイル用のLifetimeScope
    /// GameLifetimeScopeを親に持つ
    /// </summary>
    public class TileLifetimeScope : LifetimeScope
    {
        [SerializeField] private TileObject tileObject;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(tileObject);
        }
    }
}