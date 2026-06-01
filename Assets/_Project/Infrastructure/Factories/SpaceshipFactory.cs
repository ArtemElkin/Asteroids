using _Project.Core.Physics;
using _Project.Features.Gameplay.Bounds;
using _Project.Features.Gameplay.Spaceship;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class SpaceshipFactory : Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade>
    {
        private readonly IInstantiator _instantiator;
        private readonly MovableView _spaceshipPrefab;
        private readonly Transform _spaceshipParentTransform;
        
        
        public SpaceshipFactory(
            IInstantiator instantiator,
            MovableView spaceshipPrefab,
            Transform parentTransform)
        {
            _instantiator = instantiator;
            _spaceshipPrefab = spaceshipPrefab;
            _spaceshipParentTransform = parentTransform;
        }
        
        public SpaceshipFacade Create(SpaceshipSpawnData data)
        {
            var initialMovementData = new InitialMovementData(data.initialPosition, 0);
            var movementModel = _instantiator.Instantiate<MovementModel>(new object[] { initialMovementData });
            
            var movementController = _instantiator.Instantiate<SpaceshipMovementController>(new object[] { movementModel, data.movementConfig});
            
            var rotationController = _instantiator.Instantiate<SpaceshipRotationController>(new object[] { movementModel });
            
            var boundsChecker = _instantiator.Instantiate<BoundsChecker>(new object[] { movementModel, movementController});

            var view = _instantiator.InstantiatePrefabForComponent<MovableView>(_spaceshipPrefab, _spaceshipParentTransform);
            view.Setup(movementModel);
            
            var spaceship = _instantiator.Instantiate<SpaceshipFacade>(new object[]
            {
                movementModel,
                movementController,
                rotationController,
                boundsChecker,
                view
            });
            
            return spaceship;
        }
    }
}