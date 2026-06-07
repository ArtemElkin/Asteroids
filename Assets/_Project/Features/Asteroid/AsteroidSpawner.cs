using _Project.Core.Config;
using _Project.Core.Factories;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Common;
using _Project.Features.Common.Clone;
using _Project.Features.Common.Signals;

namespace _Project.Features.Asteroid
{
    public class AsteroidSpawner : BaseSpawner<AsteroidFacade>
    {
        private GameConfig _gameConfig;
        private AsteroidConfig _asteroidConfig;
        private readonly IFactory<AsteroidSpawnData, AsteroidFacade> _asteroidFactory;
        private readonly IFactory<CloneSpawnData, CloneFacade<AsteroidFacade>> _cloneFactory;
        private readonly IConfigProvider _configProvider;
        private readonly PositionGenerator _positionGenerator;
        private readonly IRandomService _randomService;


        public AsteroidSpawner(
            Storage<AsteroidFacade> asteroidsStorage,
            SpawnTimer spawnTimer,
            ISignalBus signalBus,
            IFactory<AsteroidSpawnData, AsteroidFacade> asteroidFactory,
            IFactory<CloneSpawnData, CloneFacade<AsteroidFacade>> cloneFactory,
            IConfigProvider configProvider,
            PositionGenerator positionGenerator,
            IRandomService randomService) : base(
            asteroidsStorage,
            spawnTimer,
            signalBus)
        {
            _asteroidFactory = asteroidFactory;
            _cloneFactory =  cloneFactory;
            _configProvider = configProvider;
            _positionGenerator = positionGenerator;
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
            var initialSpeed = GetRandomSpeed(_asteroidConfig.minSpeed, _asteroidConfig.maxSpeed);
            var initialDirection = GetRandomDirectionToGameArea(initialPosition);
            var initialVelocity = initialDirection * initialSpeed;
            InitialMovementData initialMovementData = new (_asteroidConfig.mass, initialPosition, initialVelocity);
            var spawnData = new AsteroidSpawnData(initialMovementData, _asteroidConfig.radius, _asteroidConfig.fragmentsCount);
            var asteroid = _asteroidFactory.Create(spawnData);
            // _signalBus.Fire(new CloneSpawnRequestedSignal<AsteroidFacade>(asteroid));
            return asteroid;
        }
        
        private void OnAsteroidFragmentSpawnRequested(SpawnRequestedSignal<AsteroidFacade> signal)
        {
            var mass = signal.initialMovementData.mass;
            var originPosition = signal.initialMovementData.initialPosition;
            var initialSpeed = GetRandomSpeed(_asteroidConfig.minFragmentSpeed, _asteroidConfig.maxFragmentSpeed);
            var originDirection = signal.initialMovementData.initialVelocity.normalized;
            var initialDirection = GetRandomDirectionFromOriginDirection(originDirection);
            var initialVelocity = initialDirection * initialSpeed;
            var initialPosition = originPosition + initialDirection * _asteroidConfig.fragmentRadius;
            InitialMovementData initialMovementData = new (mass, initialPosition, initialVelocity);
            var spawnData = new AsteroidSpawnData(initialMovementData, _asteroidConfig.fragmentRadius);
            _asteroidFactory.Create(spawnData);
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
                -_asteroidConfig.maxfragmentMoveDirectionAgleOffsetFromAsteroid, 
                _asteroidConfig.maxfragmentMoveDirectionAgleOffsetFromAsteroid);
            return Vector2.Rotate(originDirection, randomAngle);
        }

        public override void Dispose()
        {
            _signalBus.Unsubscribe<SpawnRequestedSignal<AsteroidFacade>>(OnAsteroidFragmentSpawnRequested);
            base.Dispose();
        }
    }
}