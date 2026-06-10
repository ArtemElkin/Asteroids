using System;
using _Project.Core.Config;
using _Project.Core.EventBus;
using _Project.Core.Physics;
using _Project.Core.Tools;
using _Project.Features.Spaceship.Config;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.Spaceship
{
    public class SpaceshipSpawner : IDisposable
    {
        private SpaceshipConfig _config;
        private readonly Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> _factory;
        private readonly Storage<SpaceshipFacade> _storage;
        private readonly IConfigProvider _configProvider;
        private readonly IEventBus _eventBus;

        
        public SpaceshipSpawner(
            Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> factory, 
            Storage<SpaceshipFacade> storage,
            IConfigProvider configProvider,
            IEventBus eventBus)
        {
            _factory = factory;
            _storage = storage;
            _configProvider = configProvider;
            _eventBus = eventBus;
            _eventBus.Subscribe<GameInitializeEvent>(OnGameInitialize);
            _eventBus.Subscribe<GameStartEvent>(OnGameStarted);
        }

        private void OnGameInitialize()
        {
            _config =  _configProvider.GetConfig<SpaceshipConfig>("SpaceshipConfig");
        }

        private void OnGameStarted()
        {
            SpawnSpaceship();
        }

        private void SpawnSpaceship()
        {
            var initialMovementData = new  InitialMovementData(
                _config.movementConfig.mass, 
                Vector2.zero, 
                Vector2.zero);
            var spawnData = new SpaceshipSpawnData(initialMovementData, _config);
            var spaceship = _factory.Create(spawnData);
            _storage.Add(spaceship);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<GameInitializeEvent>(OnGameInitialize);
            _eventBus.Unsubscribe<GameStartEvent>(OnGameStarted);
        }
    }
}