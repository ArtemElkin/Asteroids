using _Project.Core.Config;
using _Project.Core.Factories;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Asteroid.Config;
using _Project.Features.Common;
using _Project.Features.Common.Signals;

namespace _Project.Features.Asteroid
{
    public class AsteroidSpawner : BaseSpawner<AsteroidFacade>
    {
        private GameConfig _gameConfig;
        private AsteroidConfig _asteroidConfig;
        private readonly IFactory<AsteroidSpawnData, AsteroidFacade> _factory;
        private readonly PositionGenerator _positionGenerator;
        private readonly IConfigProvider _configProvider;
        private readonly IRandomService _randomService;

        public AsteroidSpawner(
            IFactory<AsteroidSpawnData, AsteroidFacade> factory,
            PositionGenerator positionGenerator,
            IConfigProvider configProvider,
            IRandomService randomService,
            Storage<AsteroidFacade> storage,
            SpawnTimer spawnTimer,
            ISignalBus signalBus) : base(
            storage,
            spawnTimer,
            signalBus)
        {
            _factory = factory;
            _positionGenerator = positionGenerator;
            _configProvider = configProvider;
            _randomService =  randomService;
            _signalBus.Subscribe<SpawnRequestedSignal<AsteroidFacade>>(OnAsteroidFragmentSpawnRequested);
        }


        protected override void OnInitialize()
        {
            _gameConfig = _configProvider.GetConfig<GameConfig>("GameConfig");
            _asteroidConfig =  _configProvider.GetConfig<AsteroidConfig>("AsteroidConfig");
        }

        protected override int GetMaxCount()
        {
            return _gameConfig.maxAsteroidsCount;
        }

        protected override float GetSpawnInterval()
        {
            return _gameConfig.spawnInterval;
        }

        protected override AsteroidFacade Spawn()
        {
            var initialPosition = GetRandomPosition();
            var initialSpeed = GetRandomSpeed(_asteroidConfig.movementConfig.minSpeed, _asteroidConfig.movementConfig.maxSpeed);
            var initialDirection = GetRandomDirectionToGameArea(initialPosition);
            var initialVelocity = initialDirection * initialSpeed;
            InitialMovementData initialMovementData = new (_asteroidConfig.movementConfig.mass, initialPosition, initialVelocity);
            var spawnData = new AsteroidSpawnData(initialMovementData, _asteroidConfig.radius, _asteroidConfig.fragmentsCount);
            var asteroid = _factory.Create(spawnData);
            if (_asteroidConfig.hasClones)
            {
                _signalBus.Fire(new CloneSpawnRequestedSignal<AsteroidFacade>(asteroid));
            }
            return asteroid;
        }
        
        private void OnAsteroidFragmentSpawnRequested(SpawnRequestedSignal<AsteroidFacade> signal)
        {
            var mass = signal.initialMovementData.mass;
            var originPosition = signal.initialMovementData.initialPosition;
            var initialSpeed = GetRandomSpeed(_asteroidConfig.movementConfig.minFragmentSpeed, _asteroidConfig.movementConfig.maxFragmentSpeed);
            var originDirection = signal.initialMovementData.initialVelocity.normalized;
            var initialDirection = GetRandomDirectionFromOriginDirection(originDirection);
            var initialVelocity = initialDirection * initialSpeed;
            var initialPosition = originPosition + initialDirection * _asteroidConfig.fragmentRadius;
            InitialMovementData initialMovementData = new (mass, initialPosition, initialVelocity);
            var spawnData = new AsteroidSpawnData(initialMovementData, _asteroidConfig.fragmentRadius);
            _factory.Create(spawnData);
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
            _signalBus.Unsubscribe<SpawnRequestedSignal<AsteroidFacade>>(OnAsteroidFragmentSpawnRequested);
            base.Dispose();
        }
    }
}