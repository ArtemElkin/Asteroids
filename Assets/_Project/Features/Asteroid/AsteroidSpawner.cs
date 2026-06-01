using _Project.Core.Config;
using _Project.Core.Factories;
using _Project.Core.Math;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Common;

namespace _Project.Features.Asteroid
{
    public class AsteroidSpawner : BaseSpawner<AsteroidFacade>
    {
        private GameConfig _gameConfig;
        private AsteroidConfig _asteroidConfig;
        private readonly IFactory<AsteroidSpawnData, AsteroidFacade> _asteroidFactory;
        private readonly IConfigProvider _configProvider;
        private readonly PositionGenerator _positionGenerator;
        private readonly IRandomService _randomService;


        public AsteroidSpawner(
            Storage<AsteroidFacade> asteroidsStorage,
            SpawnTimer spawnTimer,
            ISignalBus signalBus,
            IFactory<AsteroidSpawnData, AsteroidFacade> asteroidFactory,
            IConfigProvider configProvider,
            PositionGenerator positionGenerator,
            IRandomService randomService) : base(
            asteroidsStorage,
            spawnTimer,
            signalBus)
        {
            _asteroidFactory = asteroidFactory;
            _configProvider = configProvider;
            _positionGenerator = positionGenerator;
            _randomService =  randomService;
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
            var initialPosition = GetRandomInitialAsteroidPosition();
            var initialSpeed = GetRandomInitialAsteroidSpeed();
            var initialDirection = GetRandomInitialDirection(initialPosition);
            var spawnData = new AsteroidSpawnData(initialPosition, initialSpeed, initialDirection);
            var asteroid = _asteroidFactory.Create(spawnData);
            return asteroid;
        }
        
        private Vector2 GetRandomInitialAsteroidPosition()
        {
            return _positionGenerator.GenerateRandomPositionOutOfScreen(_gameConfig.spawnOffsetFromBounds);
        }

        private float GetRandomInitialAsteroidSpeed()
        {
            return _randomService.GetRandomFloat(min: _asteroidConfig.minSpeed, max: _asteroidConfig.maxSpeed);
        }
        
        private Vector2 GetRandomInitialDirection(Vector2 initialPosition)
        {
            var target = _positionGenerator.GenerateRandomPositionOnScreen();
            return (target - initialPosition).normalized;
        }
    }
}