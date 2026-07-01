using _Project.Core.Config;
using _Project.Core.Factories;
using _Project.Core.GameLifecycle;
using _Project.Core.Math;
using _Project.Core.Physics.Movement;
using _Project.Core.Services;
using _Project.Core.StaticData;
using _Project.Core.Tools;
using _Project.Features.Common.Config;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.UFO.Config;

namespace _Project.Features.UFO
{
    public class UFOSpawner : BaseEnemySpawner<UFOFacade>
    {
        protected override int MaxCount => _gameConfig.maxUFOsCount;
        protected override float SpawnInterval => _gameConfig.spawnInterval;
        private readonly GameConfig _gameConfig;
        private readonly UFOConfig _ufoConfig;
        private readonly IFactory<UFOSpawnData, UFOFacade> _ufoFactory;
        private readonly PositionGenerator _positionGenerator;
        private readonly IRandomService _randomService;
        public UFOSpawner(
            Storage<UFOFacade> ufoStorage,
            Timer spawnTimer,
            IGameStateService gameStateService,
            IFactory<UFOSpawnData, UFOFacade> ufoFactory,
            PositionGenerator positionGenerator,
            IRandomService randomService,
            IConfigProvider configProvider) : base (
            ufoStorage,
            spawnTimer,
            gameStateService)
        {
            _ufoFactory = ufoFactory;
            _positionGenerator = positionGenerator;
            _randomService = randomService;
            _gameConfig = configProvider.GetConfig<GameConfig>(FileNames.Config.Game);
            _ufoConfig =  configProvider.GetConfig<UFOConfig>(FileNames.Config.Entities.Ufo);
        }

        protected override UFOFacade Spawn()
        {
            var initialPosition = GetRandomInitialUFOPosition();
            var initialSpeed = GetRandomInitialUFOSpeed();
            var initialMovementData = new InitialMovementData(_ufoConfig.mass, initialPosition);
            var spawnData = new UFOSpawnData(
                initialMovementData,
                initialSpeed);
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