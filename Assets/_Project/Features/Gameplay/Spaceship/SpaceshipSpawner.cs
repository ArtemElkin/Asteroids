using System;
using _Project.Core.Config;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Core.Tools;
using UnityEngine;
using Zenject;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipSpawner : IDisposable
    {
        private SpaceshipMovementConfig _spaceshipConfig;
        private readonly Infrastructure.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> _spaceshipFactory;
        private readonly Infrastructure.Factories.IFactory<SpaceshipCloneSpawnData, SpaceshipCloneFacade> _spaceshipCloneFactory;
        private readonly Storage<SpaceshipFacade> _spaceshipStorage;
        private readonly ISignalBus _signalBus;
        private readonly IScreenService _screenService;
        private readonly IConfigProvider _configProvider;

        
        public SpaceshipSpawner(
            Infrastructure.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> spaceshipFactory, 
            Infrastructure.Factories.IFactory<SpaceshipCloneSpawnData, SpaceshipCloneFacade> spaceshipCloneFactory,
            Storage<SpaceshipFacade> spaceshipStorage,
            ISignalBus signalBus,
            IScreenService screenService,
            IConfigProvider configProvider)
        {
            _spaceshipFactory = spaceshipFactory;
            _spaceshipCloneFactory =  spaceshipCloneFactory;
            _spaceshipStorage = spaceshipStorage;
            _signalBus = signalBus;
            _screenService = screenService;
            _configProvider = configProvider;
            _signalBus.Subscribe<InitializeGameSignal>(OnGameInitialize);
            _signalBus.Subscribe<StartGameSignal>(OnGameStarted);
        }

        private void OnGameInitialize()
        {
            _spaceshipConfig =  _configProvider.GetConfig<SpaceshipMovementConfig>("SpaceshipMovementConfig");
        }

        private void OnGameStarted()
        {
            SpawnSpaceship();
            SpawnSpaceshipClones();
        }

        private void SpawnSpaceship()
        {
            var spawnData = new SpaceshipSpawnData(Vector2.zero, _spaceshipConfig);
            var spaceship = _spaceshipFactory.Create(spawnData);
            
            _spaceshipStorage.Add(spaceship);
        }

        private void SpawnSpaceshipClones()
        {
            var width = _screenService.ScreenWidth;
            var height = _screenService.ScreenHeight;

            Vector2[] cloneOffsets = 
            {
                new (0, height),
                new (width, height),
                new (width, 0),
                new (width, -height),
                new (0, -height),
                new (-width, -height),
                new (-width, 0),
                new (-width, height)
            };

            foreach (var offset in cloneOffsets)
            {
                var spawnData = new SpaceshipCloneSpawnData(offset);
                _spaceshipCloneFactory.Create(spawnData);
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<InitializeGameSignal>(OnGameInitialize);
            _signalBus.Unsubscribe<StartGameSignal>(OnGameStarted);
        }
    }
}