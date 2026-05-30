using System;
using _Project.Core.Config;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Common;

namespace _Project.Features.Gameplay.Asteroid
{
    public class AsteroidSpawner : IDisposable
    {
        private int _maxAsteroidsCount;
        private readonly Storage<AsteroidComponent> _asteroidsStorage;
        private readonly AsteroidBuilder _asteroidBuilder;
        private readonly IConfigProvider _configProvider;
        private readonly ISignalBus _signalBus;
        private readonly SpawnTimer<AsteroidComponent> _spawnTimer;


        public AsteroidSpawner(
            Storage<AsteroidComponent> asteroidsStorage,
            AsteroidBuilder asteroidBuilder,
            IConfigProvider configProvider,
            ISignalBus signalBus,
            SpawnTimer<AsteroidComponent> spawnTimer)
        {
            _asteroidsStorage = asteroidsStorage;
            _asteroidBuilder = asteroidBuilder;
            _configProvider = configProvider;
            _signalBus = signalBus;
            _spawnTimer = spawnTimer;
            _signalBus.Subscribe<InitializeGameSignal>(Initialize);
            _spawnTimer.OnSpawnRequested += OnSpawnRequested;;
        }

        public void Initialize()
        {
            var gameConfig = _configProvider.GetConfig<GameConfig>("GameConfig");
            _maxAsteroidsCount = gameConfig.maxAsteroidsCount;
            var spawnOffsetFromBounds = gameConfig.spawnOffsetFromBounds;
            
            _asteroidBuilder.SetSpawnOffsetFromBounds(spawnOffsetFromBounds);

        }

        private void OnSpawnRequested()
        {
            if (_asteroidsStorage.Count < _maxAsteroidsCount)
            {
                SpawnAsteroid();
            }
        }

        private void SpawnAsteroid()
        {
            var asteroid = _asteroidBuilder
                .AddMovementController()
                .AddBoundsChecker()
                .Build();
            _asteroidsStorage.Add(asteroid);
        }

        public void Dispose()
        {
            _spawnTimer.OnSpawnRequested -= OnSpawnRequested;
            _signalBus.Unsubscribe<InitializeGameSignal>(Initialize);
        }
    }
}