using _Project.Features.Common;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public abstract class AbstractFactory<TSpawnData, TFacade> : Core.Factories.IFactory<TSpawnData, TFacade> where TFacade : IFacade
    {
        protected readonly CustomPool<MovableView> _viewPool;
        protected readonly IInstantiator _instantiator;


        public AbstractFactory(
            IInstantiator instantiator,
            MovableView prefab,
            Transform parentTransform)
        {
            _instantiator = instantiator;
            _viewPool = new CustomPool<MovableView>(instantiator, prefab, defaultParentTransform: parentTransform);
        }

        public abstract TFacade Create(TSpawnData data);

        public void Release(TFacade facade)
        {
            IDrawable drawable = facade.GetDrawable();
            drawable.Reset();
            
            var view = (MovableView)drawable;
            _viewPool.Release(view);
            
            facade.Dispose();
        }
    }
}