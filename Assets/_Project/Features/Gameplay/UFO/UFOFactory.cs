using _Project.Core.Physics;
using _Project.Features.Gameplay.Bounds;
using _Project.Infrastructure.Factories;
using _Project.Infrastructure.Tools;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.UFO
{
    public class UFOFactory : Infrastructure.Factories.IFactory<UFOSpawnData, UFOFacade>
    {
        private readonly CustomPool<UFOView>  _viewPool;
        private readonly IInstantiator _instantiator;
        
        
        public UFOFactory(
            IInstantiator instantiator,
            UFOView prefab,
            Transform parentTransform)
        {
            _instantiator = instantiator;
            _viewPool = new CustomPool<UFOView>(instantiator, prefab, defaultParentTransform: parentTransform);
        }
        
        public UFOFacade Create(UFOSpawnData data)
        {
            var initialMovementData = new InitialMovementData(data.initialPosition, data.initialSpeed);
            var movementModel = _instantiator.Instantiate<MovementModel>(new object[] { initialMovementData });
            
            var movementController = _instantiator.Instantiate<UFOMovementController>(new object[] { movementModel, data});
            
            var rotationController = _instantiator.Instantiate<UFORotationController>(new object[] { movementModel });
            
            var targetFollower = _instantiator.Instantiate<UFOTargetFollower>(new object[] { movementModel });
            
            var boundsChecker = _instantiator.Instantiate<BoundsChecker>(new object[] { movementModel, movementController});
            
            var view = _viewPool.Get();
            view.Setup(movementModel);
            view.transform.localPosition = data.initialPosition.ToUnity();
            
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