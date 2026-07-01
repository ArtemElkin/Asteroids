using _Project.Core.Factories;
using _Project.Core.Render;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public abstract class AbstractFacadeFactory<TSpawnData, TFacade, TView> 
        : AbstractFactory<TSpawnData, TFacade, TView>, IReleaser<TFacade> 
        where TFacade : IFacade 
        where TView : BaseGameEntityView
    {
        protected AbstractFacadeFactory(
            IInstantiator instantiator,
            TView prefab,
            Transform parentTransform) : base(instantiator, prefab, parentTransform)
        {
        }

        public void Release(TFacade facade)
        {
            IDrawable drawable = facade.Drawable;
            facade.Dispose();
            drawable.Reset();
            
            var view = (TView)drawable;
            _pool.Release(view);
        }
    }
}