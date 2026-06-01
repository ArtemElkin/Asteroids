using _Project.Core.Physics;
using _Project.Features.Common.Bounds;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.Health;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Factories
{
    public class SpaceshipFactory : Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade>
    {
        private MovableView _view;
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
            
            var boundsChecker = _instantiator.Instantiate<BoundsChecker>(new object[]
            {
                movementModel, 
                movementController
            });
            
            if (_view == null)
                _view = _instantiator.InstantiatePrefabForComponent<MovableView>(_spaceshipPrefab, _spaceshipParentTransform);
            _view.Setup(movementModel);
            _view.gameObject.SetActive(true);
            
            var collisionHandler = _view.GetComponent<SpaceshipCollisionHandler>();

            var healthModel = _instantiator.Instantiate<HealthModel>(new object[] { data.initialHp });
            var healthController = _instantiator.Instantiate<HealthController>(new object[] { healthModel });
            
            var spaceship = _instantiator.Instantiate<SpaceshipFacade>(new object[]
            {
                movementModel,
                movementController,
                rotationController,
                boundsChecker,
                _view,
                collisionHandler,
                healthController
            });
            
            return spaceship;
        }

        public void Release(SpaceshipFacade facade)
        {
            _view.Reset();
            _view.gameObject.SetActive(false);
            facade.Dispose();
        }
    }
}