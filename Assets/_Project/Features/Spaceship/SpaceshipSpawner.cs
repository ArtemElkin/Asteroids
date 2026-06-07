using System;
using _Project.Core.Config;
using _Project.Core.Physics;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Common.Signals;
using _Project.Features.Spaceship.Config;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.Spaceship
{
    public class SpaceshipSpawner : IDisposable
    {
        private SpaceshipConfig _config;
        private readonly Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> _factory;
        private readonly Storage<SpaceshipFacade> _storage;
        private readonly ISignalBus _signalBus;
        private readonly IConfigProvider _configProvider;

        
        public SpaceshipSpawner(
            Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> factory, 
            Storage<SpaceshipFacade> storage,
            ISignalBus signalBus,
            IConfigProvider configProvider)
        {
            _factory = factory;
            _storage = storage;
            _signalBus = signalBus;
            _configProvider = configProvider;
            _signalBus.Subscribe<GameInitializeSignal>(OnGameInitialize);
            _signalBus.Subscribe<GameStartSignal>(OnGameStarted);
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

            if (_config.hasClones)
            {
                _signalBus.Fire(new CloneSpawnRequestedSignal<SpaceshipFacade>(spaceship));
            }
            
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameInitializeSignal>(OnGameInitialize);
            _signalBus.Unsubscribe<GameStartSignal>(OnGameStarted);
        }
    }
}