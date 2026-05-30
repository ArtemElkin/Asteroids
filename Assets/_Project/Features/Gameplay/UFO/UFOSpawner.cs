using System;
using _Project.Core.Config;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Signals;
using _Project.Features.Gameplay.Spaceship;

namespace _Project.Features.Gameplay.UFO
{
    public class UFOSpawner : IDisposable
    {
        private float _spawnOffsetFromBounds;
        private int _maxUFOsCount;
        private readonly Storage<UFOComponent> _ufoStorage;
        private readonly Storage<SpaceshipComponent> _spaceshipStorage;
        private readonly UFOBuilder _ufoBuilder;
        private readonly IConfigProvider _configProvider;
        private readonly ISignalBus _signalBus;

        public UFOSpawner(
            UFOBuilder ufoBuilder,
            Storage<UFOComponent> ufoStorage,
            Storage<SpaceshipComponent> spaceshipStorage,
            IConfigProvider configProvider,
            ISignalBus signalBus)
        {
            _ufoBuilder = ufoBuilder;
            _ufoStorage = ufoStorage;
            _spaceshipStorage = spaceshipStorage;
            _configProvider = configProvider;
            _signalBus = signalBus;
            _signalBus.Subscribe<InitializeGameSignal>(Initialize);
            _signalBus.Subscribe<SpawnRequestedSignal<UFOComponent>>(OnSpawnRequested);
        }

        public void Initialize()
        {
            
            var gameConfig = _configProvider.GetConfig<GameConfig>("GameConfig");
            _maxUFOsCount = gameConfig.maxUFOsCount;
            _spawnOffsetFromBounds = gameConfig.spawnOffsetFromBounds;
            
            _ufoBuilder.SetSpawnOffsetFromBounds(_spawnOffsetFromBounds);

        }

        private void OnSpawnRequested()
        {
            if (_ufoStorage.Count < _maxUFOsCount)
            {
                SpawnUFO();
            }
        }

        private void SpawnUFO()
        {
            var ufo = _ufoBuilder
                .AddMovementController()
                .AddRotationController()
                .AddTargetFollower(_spaceshipStorage)
                .AddBoundsChecker()
                .Build();
            
            _ufoStorage.Add(ufo);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<InitializeGameSignal>(Initialize);
            _signalBus.Unsubscribe<SpawnRequestedSignal<UFOComponent>>(OnSpawnRequested);
        }
    }
}