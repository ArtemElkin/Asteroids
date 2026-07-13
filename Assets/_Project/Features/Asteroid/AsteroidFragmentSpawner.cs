using System;
using _Project.Core.Config;
using _Project.Core.EventBus;
using _Project.Core.Factories;
using _Project.Core.Math;
using _Project.Core.Physics.Movement;
using _Project.Core.Services;
using _Project.Core.StaticData;
using _Project.Core.Tools;
using _Project.Features.Asteroid.Config;
using _Project.Features.Common.EntitiesLifecycle.Events;

namespace _Project.Features.Asteroid
{
    public class AsteroidFragmentSpawner : IDisposable
    {
        private readonly AsteroidConfig _asteroidConfig;
        private readonly Storage<AsteroidFacade> _asteroidStorage;
        private readonly IFactory<AsteroidSpawnData, AsteroidFacade> _factory;
        private readonly IRandomService _randomService;
        private readonly IEventBus _eventBus;


        public AsteroidFragmentSpawner(
            IConfigProvider configProvider,
            Storage<AsteroidFacade> asteroidStorage,
            IFactory<AsteroidSpawnData, AsteroidFacade> factory,
            IRandomService randomService,
            IEventBus eventBus)
        {
            _asteroidStorage = asteroidStorage;
            _factory = factory;
            _randomService = randomService;
            _eventBus = eventBus;
            _eventBus.Subscribe<SpawnRequestedEvent<AsteroidFacade>>(OnAsteroidFragmentSpawnRequested);
            _asteroidConfig =  configProvider.GetConfig<AsteroidConfig>(FileNames.Config.Entities.Asteroid);
        }
        
        private void OnAsteroidFragmentSpawnRequested(SpawnRequestedEvent<AsteroidFacade> @event)
        {
            var mass = @event.SpawnData.mass;
            var originPosition = @event.SpawnData.initialPosition;
            var initialSpeed = GetRandomSpeed(_asteroidConfig.movementConfig.minFragmentSpeed, _asteroidConfig.movementConfig.maxFragmentSpeed);
            var originDirection = @event.SpawnData.initialVelocity.normalized;
            var initialDirection = GetRandomDirectionFromOriginDirection(originDirection);
            var initialVelocity = initialDirection * initialSpeed;
            var initialPosition = originPosition + initialDirection * _asteroidConfig.fragmentRadius;
            InitialMovementData initialMovementData = new (mass, initialPosition, initialVelocity);
            var spawnData = new AsteroidSpawnData(
                initialMovementData, 
                _asteroidConfig.fragmentRadius, 
                0,
                false,
                _asteroidConfig);
            var asteroidFragment = _factory.Create(spawnData);
            _asteroidStorage.Add(asteroidFragment);
        }
        
        private Vector2 GetRandomDirectionFromOriginDirection(Vector2 originDirection)
        {
            var randomAngle = _randomService.GetRandomFloat(
                -_asteroidConfig.movementConfig.maxfragmentMoveDirectionAgleOffsetFromAsteroid, 
                _asteroidConfig.movementConfig.maxfragmentMoveDirectionAgleOffsetFromAsteroid);
            return Vector2.Rotate(originDirection, randomAngle);
        }
        
        private float GetRandomSpeed(float min, float max)
        {
            return _randomService.GetRandomFloat(min: min, max: max);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<SpawnRequestedEvent<AsteroidFacade>>(OnAsteroidFragmentSpawnRequested);
        }
    }
}