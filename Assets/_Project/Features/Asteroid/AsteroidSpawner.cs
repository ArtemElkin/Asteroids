using _Project.Core.Config;
using _Project.Core.EventBus;
using _Project.Core.Factories;
using _Project.Core.GameLifecycle;
using _Project.Core.Math;
using _Project.Core.Physics.Movement;
using _Project.Core.Services;
using _Project.Core.Tools;
using _Project.Features.Asteroid.Config;
using _Project.Features.Common.Config;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Common.EntitiesLifecycle.Events;

namespace _Project.Features.Asteroid
{
    public class AsteroidSpawner : BaseEnemySpawner<AsteroidFacade>
    {
        protected override int MaxCount => _gameConfig.maxAsteroidsCount;
        protected override float SpawnInterval => _gameConfig.spawnInterval;
        private readonly GameConfig _gameConfig;
        private readonly AsteroidConfig _asteroidConfig;
        private readonly IFactory<AsteroidSpawnData, AsteroidFacade> _factory;
        private readonly PositionGenerator _positionGenerator;
        private readonly IRandomService _randomService;
        private readonly IEventBus _eventBus;

        public AsteroidSpawner(
            IFactory<AsteroidSpawnData, AsteroidFacade> factory,
            PositionGenerator positionGenerator,
            IConfigProvider configProvider,
            IRandomService randomService,
            Storage<AsteroidFacade> storage,
            Timer spawnTimer,
            IGameStateService gameStateService,
            IEventBus eventBus) : base(
            storage,
            spawnTimer,
            gameStateService)
        {
            _factory = factory;
            _positionGenerator = positionGenerator;
            _randomService =  randomService;
            _eventBus = eventBus;
            _eventBus.Subscribe<SpawnRequestedEvent<AsteroidFacade>>(OnAsteroidFragmentSpawnRequested);
            _gameConfig = configProvider.GetConfig<GameConfig>("GameConfig");
            _asteroidConfig =  configProvider.GetConfig<AsteroidConfig>("AsteroidConfig");
        }

        protected override AsteroidFacade Spawn()
        {
            var initialPosition = GetRandomPosition();
            var initialSpeed = GetRandomSpeed(_asteroidConfig.movementConfig.minSpeed, _asteroidConfig.movementConfig.maxSpeed);
            var initialDirection = GetRandomDirectionToGameArea(initialPosition);
            var initialVelocity = initialDirection * initialSpeed;
            InitialMovementData initialMovementData = new (_asteroidConfig.movementConfig.mass, initialPosition, initialVelocity);
            var spawnData = new AsteroidSpawnData(
                initialMovementData,
                _asteroidConfig.radius,
                _asteroidConfig.fragmentsCount,
                _asteroidConfig.hasClones,
                _asteroidConfig);
            var asteroid = _factory.Create(spawnData);
            return asteroid;
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
            _storage.Add(asteroidFragment);
        }
        
        private Vector2 GetRandomPosition()
        {
            return _positionGenerator.GenerateRandomPositionOutOfScreen(_gameConfig.spawnOffsetFromBounds);
        }

        private float GetRandomSpeed(float min, float max)
        {
            return _randomService.GetRandomFloat(min: min, max: max);
        }
        
        private Vector2 GetRandomDirectionToGameArea(Vector2 initialPosition)
        {
            var target = _positionGenerator.GenerateRandomPositionOnScreen();
            return (target - initialPosition).normalized;
        }

        private Vector2 GetRandomDirectionFromOriginDirection(Vector2 originDirection)
        {
            var randomAngle = _randomService.GetRandomFloat(
                -_asteroidConfig.movementConfig.maxfragmentMoveDirectionAgleOffsetFromAsteroid, 
                _asteroidConfig.movementConfig.maxfragmentMoveDirectionAgleOffsetFromAsteroid);
            return Vector2.Rotate(originDirection, randomAngle);
        }

        public override void Dispose()
        {
            _eventBus.Unsubscribe<SpawnRequestedEvent<AsteroidFacade>>(OnAsteroidFragmentSpawnRequested);
            base.Dispose();
        }
    }
}