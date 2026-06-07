using System;
using _Project.Core.Config;
using _Project.Core.Physics;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Common.Clone;
using _Project.Features.Common.Signals;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.Spaceship
{
    public class SpaceshipSpawner : IDisposable
    {
        private SpaceshipConfig _spaceshipConfig;
        private readonly Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> _spaceshipFactory;
        private readonly Storage<SpaceshipFacade> _spaceshipStorage;
        private readonly ISignalBus _signalBus;
        private readonly IConfigProvider _configProvider;

        
        public SpaceshipSpawner(
            Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> spaceshipFactory, 
            Storage<SpaceshipFacade> spaceshipStorage,
            ISignalBus signalBus,
            IConfigProvider configProvider)
        {
            _spaceshipFactory = spaceshipFactory;
            _spaceshipStorage = spaceshipStorage;
            _signalBus = signalBus;
            _configProvider = configProvider;
            _signalBus.Subscribe<GameInitializeSignal>(OnGameInitialize);
            _signalBus.Subscribe<GameStartSignal>(OnGameStarted);
        }

        private void OnGameInitialize()
        {
            _spaceshipConfig =  _configProvider.GetConfig<SpaceshipConfig>("SpaceshipConfig");
        }

        private void OnGameStarted()
        {
            SpawnSpaceship();
        }

        private void SpawnSpaceship()
        {
            var initialMovementData = new  InitialMovementData(
                _spaceshipConfig.movementConfig.mass, 
                Vector2.zero, 
                Vector2.zero);
            var spawnData = new SpaceshipSpawnData(initialMovementData, _spaceshipConfig, _spaceshipConfig.maxHp);
            var spaceship = _spaceshipFactory.Create(spawnData);
            _spaceshipStorage.Add(spaceship);

            if (_spaceshipConfig.hasClones)
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