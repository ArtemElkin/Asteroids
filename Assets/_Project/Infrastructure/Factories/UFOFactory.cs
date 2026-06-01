using _Project.Core.Physics;
using _Project.Features.Gameplay.Bounds;
using _Project.Features.Gameplay.UFO;
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
            
            var movementController = _instantiator.Instantiate<UFOMovementController>(new object[] { movementModel, data});
            
            var rotationController = _instantiator.Instantiate<UFORotationController>(new object[] { movementModel });
            
            var targetFollower = _instantiator.Instantiate<UFOTargetFollower>(new object[] { movementModel });
            
            var boundsChecker = _instantiator.Instantiate<BoundsChecker>(new object[] { movementModel, movementController});
            
            var view = _viewPool.Get();
            view.Setup(movementModel);
            view.transform.localPosition = initialMovementData.initialPosition.ToUnity();
            
            var ufo = _instantiator.Instantiate<UFOFacade>(new object[]
            {
                movementController,
                rotationController,
                targetFollower,
                boundsChecker,
                view
            });
            
            return ufo;
        }
    }
}