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
            Debug.Log("Creating clone"); 
            MovementModel movementModel = CreateComponent<MovementModel>(new InitialMovementData(data.cloneOffset));
            var originView = (MovableView)data.drawable;
            var view = _viewPool.Get();
            view.transform.localScale = originView.transform.localScale;
            IDrawable drawable = view;
            drawable.Setup(movementModel);
            
            ICollidable collidable = view.GetComponent<ICollidable>();
                
            IReadOnlyPositionable originPositionable = data._originPositionable;
            IReadOnlyRotationable originRotationable = data._originRotationable;
            
            BoundsChecker originBoundsChecker = CreateComponent<BoundsChecker>(data._originPositionable); 
            
            var facade = CreateComponent<CloneFacade<TOriginFacade>>(
                drawable,
                movementModel,
                originPositionable,
                originRotationable,
                originBoundsChecker,
                collidable,
                data.cloneOffset);
            return facade;
        }
    }
}