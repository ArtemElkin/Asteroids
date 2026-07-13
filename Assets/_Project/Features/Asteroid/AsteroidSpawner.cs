using _Project.Core.Config;
using _Project.Core.Factories;
using _Project.Core.GameLifecycle;
using _Project.Core.Math;
using _Project.Core.Physics.Movement;
using _Project.Core.Services;
using _Project.Core.StaticData;
using _Project.Core.Tools;
using _Project.Features.Asteroid.Config;
using _Project.Features.Common.Config;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Common.Settings;

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
        private readonly SettingsModel _settingsModel;
        private readonly IRandomService _randomService;

        
        public AsteroidSpawner(
            IFactory<AsteroidSpawnData, AsteroidFacade> factory,
            PositionGenerator positionGenerator,
            IConfigProvider configProvider,
            SettingsModel settingsModel,
            IRandomService randomService,
            Storage<AsteroidFacade> storage,
            Timer spawnTimer,
            IGameStateService gameStateService) : base(
            storage,
            spawnTimer,
            gameStateService)
        {
            _factory = factory;
            _positionGenerator = positionGenerator;
            _settingsModel = settingsModel;
            _randomService =  randomService;
            _gameConfig = configProvider.GetConfig<GameConfig>(FileNames.Config.Game);
            _asteroidConfig =  configProvider.GetConfig<AsteroidConfig>(FileNames.Config.Entities.Asteroid);
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
                _settingsModel.AsteroidsClonesEnabled,
                _asteroidConfig);
            var asteroid = _factory.Create(spawnData);
            return asteroid;
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
    }
}