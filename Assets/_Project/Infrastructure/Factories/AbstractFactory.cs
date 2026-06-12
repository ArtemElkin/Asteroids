using _Project.Core.Factories;
using _Project.Core.Physics;
using _Project.Core.Render;
using _Project.Features.Common;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public abstract class AbstractFactory<TSpawnData, TFacade> 
        : Core.Factories.IFactory<TSpawnData, TFacade>, 
            IReleaser<TFacade> 
        where TFacade : IFacade
    {
        protected readonly CustomPool<BaseGameEntityView> _viewPool;
        protected readonly IInstantiator _instantiator;


        public AbstractFactory(
            IInstantiator instantiator,
            BaseGameEntityView prefab,
            Transform parentTransform)
        {
            _instantiator = instantiator;
            _viewPool = new CustomPool<BaseGameEntityView>(instantiator, prefab, defaultParentTransform: parentTransform);
        }

        public abstract TFacade Create(TSpawnData data);

        protected T CreateComponent<T>(params object[] extraArgs)
        {
            return _instantiator.Instantiate<T>(extraArgs);
        }

        public void Release(TFacade facade)
        {
            IDrawable drawable = facade.Drawable;
            drawable.Reset();
            
            var view = (BaseGameEntityView)drawable;
            _viewPool.Release(view);
            
            facade.Dispose();
        }
    }
}