using _Project.Core.Physics;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.UFO;
using _Project.Infrastructure.UnityRender;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class UFOFactory : AbstractFactory<UFOSpawnData, UFOFacade>
    {
        public UFOFactory(IInstantiator instantiator, MovableView prefab, Transform parentTransform) :
            base(instantiator, prefab, parentTransform) { }

        public override UFOFacade Create(UFOSpawnData data)
        {
            var initialMovementData = data.initialMovementData;
            var movementModel = _instantiator.Instantiate<MovementModel>(new object[] { initialMovementData });
            
            MovableView view = _viewPool.Get();
            view.transform.localPosition = initialMovementData.initialPosition.ToUnity();

            IDrawable drawable = view;
            drawable.Setup(movementModel);
            
            ICollidable collidable = view.GetComponent<ICollidable>();
            
            IHitable hitable = view.GetComponent<IHitable>();

            IMovable movable = _instantiator.Instantiate<BaseMovementController>(new object[]
            {
                movementModel, 
            });
            
            IRotatable rotatable = _instantiator.Instantiate<UFORotationController>(new object[] { movementModel });
            
            var targetFollower = _instantiator.Instantiate<UFOTargetFollower>(new object[] { movementModel });
            
            var boundsChecker = _instantiator.Instantiate<BoundsChecker>(new object[]
            {
                movementModel, 
                movable
            });
           
            var ufo = _instantiator.Instantiate<UFOFacade>(new object[]
            {
                movable,
                rotatable,
                targetFollower,
                boundsChecker,
                view,
                collidable,
                hitable
            });
            
            return ufo;
        }
    }
}