using _Project.Core.Config;
using _Project.Core.EventBus;
using _Project.Core.Factories;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Tools;
using _Project.Features.Common;
using _Project.Features.Spaceship;

namespace _Project.Features.UFO
{
    public class UFOSpawner : BaseSpawner<UFOFacade>
    {
        private GameConfig _gameConfig;
        private UFOConfig _ufoConfig;
        private readonly Storage<SpaceshipFacade> _spaceshipStorage;
        private readonly IConfigProvider _configProvider;
        private readonly IFactory<UFOSpawnData, UFOFacade> _ufoFactory;
        private readonly PositionGenerator _positionGenerator;
        private readonly IRandomService _randomService;
        public UFOSpawner(
            Storage<UFOFacade> ufoStorage,
            SpawnTimer spawnTimer,
            IEventBus eventBus,
            IFactory<UFOSpawnData, UFOFacade> ufoFactory,
            PositionGenerator positionGenerator,
            IRandomService randomService,
            Storage<SpaceshipFacade> spaceshipStorage,
            IConfigProvider configProvider) : base (
            ufoStorage,
            spawnTimer,
            eventBus)
        {
            _ufoFactory = ufoFactory;
            _positionGenerator = positionGenerator;
            _randomService = randomService;
            _spaceshipStorage = spaceshipStorage;
            _configProvider = configProvider;
        }

        protected override void OnInitialize()
        {
            _gameConfig = _configProvider.GetConfig<GameConfig>("GameConfig");
            
            _ufoConfig =  _configProvider.GetConfig<UFOConfig>("UFOConfig");
        }

        protected override int GetMaxCount()
        {
            return _gameConfig.maxUFOsCount;
        }

        protected override float GetSpawnInterval()
        {
            return _gameConfig.spawnInterval;
        }

        protected override UFOFacade Spawn()
        {
            var initialPosition = GetRandomInitialUFOPosition();
            var initialSpeed = GetRandomInitialUFOSpeed();
            // TODO: mass
            var initialMovementData = new InitialMovementData(1000, initialPosition, Vector2.zero);
            var spawnData = new UFOSpawnData(
                initialMovementData,
                initialSpeed,
                _ufoConfig.accelerationMultiplier,
                _ufoConfig.inertiaMultiplier);
            var ufo = _ufoFactory.Create(spawnData);
            return ufo;
        }
        private Vector2 GetRandomInitialUFOPosition()
        {
            return _positionGenerator.GenerateRandomPositionOutOfScreen(_gameConfig.spawnOffsetFromBounds);
        }

        private float GetRandomInitialUFOSpeed()
        {
            return _randomService.GetRandomFloat(min: _ufoConfig.minSpeed, max: _ufoConfig.maxSpeed);
        }
    }
}