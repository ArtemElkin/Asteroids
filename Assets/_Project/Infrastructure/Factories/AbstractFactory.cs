using System.Collections.Generic;
using _Project.Core.Physics;
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

        protected T CreateComponent<T>(params object[] extraArgs)
        {
            return _instantiator.Instantiate<T>(extraArgs);
        }

        public void Release(TFacade facade)
        {
            IDrawable drawable = facade.GetDrawable();
            drawable.Reset();
            
            var view = (MovableView)drawable;
            _viewPool.Release(view);
            
            ICollidable collidable = view.GetComponent<ICollidable>();
            collidable.Reset();
            
            facade.Dispose();
        }
    }
}