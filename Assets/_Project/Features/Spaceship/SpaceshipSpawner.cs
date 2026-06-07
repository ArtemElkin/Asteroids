using System;
using _Project.Core.Config;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Common.Clone;
using _Project.Features.Common.Signals;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.Spaceship
{
    public class SpaceshipSpawner : IDisposable
    {
        private SpaceshipConfig _spaceshipConfig;
        private SpaceshipMovementConfig _spaceshipMovementConfig;
        private readonly Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> _spaceshipFactory;
        private readonly Core.Factories.IFactory<CloneSpawnData, CloneFacade<SpaceshipFacade>> _cloneFactory;
        private readonly Storage<SpaceshipFacade> _spaceshipStorage;
        private readonly ISignalBus _signalBus;
        private readonly IScreenService _screenService;
        private readonly IConfigProvider _configProvider;

        
        public SpaceshipSpawner(
            Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> spaceshipFactory, 
            Core.Factories.IFactory<CloneSpawnData, CloneFacade<SpaceshipFacade>> cloneFactory,
            Storage<SpaceshipFacade> spaceshipStorage,
            ISignalBus signalBus,
            IScreenService screenService,
            IConfigProvider configProvider)
        {
            _spaceshipFactory = spaceshipFactory;
            _cloneFactory =  cloneFactory;
            _spaceshipStorage = spaceshipStorage;
            _signalBus = signalBus;
            _screenService = screenService;
            _configProvider = configProvider;
            _signalBus.Subscribe<GameInitializeSignal>(OnGameInitialize);
            _signalBus.Subscribe<GameStartSignal>(OnGameStarted);
        }

        private void OnGameInitialize()
        {
            _spaceshipMovementConfig =  _configProvider.GetConfig<SpaceshipMovementConfig>("SpaceshipMovementConfig");
            _spaceshipConfig =  _configProvider.GetConfig<SpaceshipConfig>("SpaceshipConfig");
        }

        private void OnGameStarted()
        {
            SpawnSpaceship();
        }

        private void SpawnSpaceship()
        {
            // TODO: mass
            InitialMovementData initialMovementData = new  InitialMovementData(1000, Vector2.zero, Vector2.zero);
            var spawnData = new SpaceshipSpawnData(initialMovementData, _spaceshipMovementConfig, _spaceshipConfig.maxHp);
            var spaceship = _spaceshipFactory.Create(spawnData);
            Debug.Log("Firing clone spawn reqest for spaceship");
            _signalBus.Fire(new CloneSpawnRequestedSignal<SpaceshipFacade>(spaceship));
            _spaceshipStorage.Add(spaceship);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameInitializeSignal>(OnGameInitialize);
            _signalBus.Unsubscribe<GameStartSignal>(OnGameStarted);
        }
    }
}