using _Project.Core.Factories;
using _Project.Core.Render;
using _Project.Features.Common.ScreenWrapClone;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class ScreenWrapCloneFactory<TOriginEntity> : IScreenWrapCloneFactory<ScreenWrapCloneSpawnData, TOriginEntity>
    {
        private readonly CustomPool<TransformView> _viewPool;
        private readonly IInstantiator _instantiator;
        
        public ScreenWrapCloneFactory(
            IInstantiator instantiator, 
            TransformView prefab, 
            Transform parentTransform)
        {
            _viewPool = new CustomPool<TransformView>(instantiator, prefab, defaultParentTransform: parentTransform);
        }

        public IDrawable Create(ScreenWrapCloneSpawnData data)
        {
            var view = _viewPool.Get();
            
            IDrawable drawable = view;
            drawable.Setup(data.originMovementModel.Position + data.cloneOffset, data.originMovementModel.RotationAngle);
            var originView = (MovableView) data.originDrawable;
            view.transform.localScale = originView.transform.localScale;
            return drawable;
        }

        public void Release(IDrawable screenWrapCloneDrawable)
        {
            var cloneView = (TransformView)screenWrapCloneDrawable;
            if (cloneView == null) return;
            screenWrapCloneDrawable.Reset();
            _viewPool.Release(cloneView);
        }
    }
}