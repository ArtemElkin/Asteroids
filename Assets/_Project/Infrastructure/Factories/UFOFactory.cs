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
    public class UFOFactory : Core.Factories.IFactory<UFOSpawnData, UFOFacade>
    {
        private readonly CustomPool<MovableView>  _viewPool;
        private readonly IInstantiator _instantiator;
        
        
        public UFOFactory(
            IInstantiator instantiator,
            MovableView prefab,
            Transform parentTransform)
        {
            _instantiator = instantiator;
            _viewPool = new CustomPool<MovableView>(instantiator, prefab, defaultParentTransform: parentTransform);
        }
        
        public UFOFacade Create(UFOSpawnData data)
        {
            var initialMovementData = data.initialMovementData;
            var movementModel = _instantiator.Instantiate<MovementModel>(new object[] { initialMovementData });
            
            var view = _viewPool.Get();
            view.Setup(movementModel);
            view.transform.localPosition = initialMovementData.initialPosition.ToUnity();

            var collidable = view.GetComponent<ICollidable>();
            
            var hitable = view.GetComponent<IHitable>();

            var movementController = _instantiator.Instantiate<UFOMovementController>(new object[]
            {
                movementModel, 
                data
            });
            
            var rotationController = _instantiator.Instantiate<UFORotationController>(new object[] { movementModel });
            
            var targetFollower = _instantiator.Instantiate<UFOTargetFollower>(new object[] { movementModel });
            
            var boundsChecker = _instantiator.Instantiate<BoundsChecker>(new object[]
            {
                movementModel, 
                movementController
            });
           
            var ufo = _instantiator.Instantiate<UFOFacade>(new object[]
            {
                movementController,
                rotationController,
                targetFollower,
                boundsChecker,
                view,
                collidable,
                hitable
            });
            
            return ufo;
        }

        public void Release(UFOFacade facade)
        {
            var drawable = facade.GetDrawable();
            var view = (MovableView)drawable;
            view.Reset();
            _viewPool.Release(view);
            facade.Dispose();
        }
    }
}