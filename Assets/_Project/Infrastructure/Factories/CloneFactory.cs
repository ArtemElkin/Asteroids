using _Project.Core.Physics;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Clone;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class CloneFactory<TOriginFacade> : AbstractFactory<CloneSpawnData, CloneFacade<TOriginFacade>> where TOriginFacade : IFacade
    {
        public CloneFactory(IInstantiator instantiator, MovableView prefab, Transform parentTransform) : base(instantiator, prefab, parentTransform)
        {
        }

        public override CloneFacade<TOriginFacade> Create(CloneSpawnData data)
        {
            var originView = (MovableView)data.originDrawable;
            var view = _viewPool.Get();
            IDrawable drawable = view;
            view.Setup(data.originMovementModel.Position + data.cloneOffset, data.originMovementModel.RotationAngle);
            view.transform.localScale = originView.transform.localScale;
            ICollidable collidable = view.GetComponent<ICollidable>();
            collidable.Setup(data.originMovementModel);
            
            BoundsChecker originBoundsChecker = CreateComponent<BoundsChecker>(data.originMovementModel); 
            
            var facade = CreateComponent<CloneFacade<TOriginFacade>>(
                drawable,
                data.originMovementModel,
                originBoundsChecker,
                collidable,
                data.cloneOffset);
            return facade;
        }
    }
}