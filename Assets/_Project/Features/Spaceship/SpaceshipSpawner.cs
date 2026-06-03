using System;
using _Project.Core.Config;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Spaceship.SpaceshipClone;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.Spaceship
{
    public class SpaceshipSpawner : IDisposable
    {
        private SpaceshipConfig _spaceshipConfig;
        private SpaceshipMovementConfig _spaceshipMovementConfig;
        private readonly Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> _spaceshipFactory;
        private readonly Core.Factories.IFactory<SpaceshipCloneSpawnData, SpaceshipCloneFacade> _spaceshipCloneFactory;
        private readonly Storage<SpaceshipFacade> _spaceshipStorage;
        private readonly Storage<SpaceshipCloneFacade> _spaceshipCloneStorage;
        private readonly ISignalBus _signalBus;
        private readonly IScreenService _screenService;
        private readonly IConfigProvider _configProvider;

        
        public SpaceshipSpawner(
            Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> spaceshipFactory, 
            Core.Factories.IFactory<SpaceshipCloneSpawnData, SpaceshipCloneFacade> spaceshipCloneFactory,
            Storage<SpaceshipFacade> spaceshipStorage,
            Storage<SpaceshipCloneFacade> spaceshipCloneStorage,
            ISignalBus signalBus,
            IScreenService screenService,
            IConfigProvider configProvider)
        {
            _spaceshipFactory = spaceshipFactory;
            _spaceshipCloneFactory =  spaceshipCloneFactory;
            _spaceshipStorage = spaceshipStorage;
            _spaceshipCloneStorage = spaceshipCloneStorage;
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
            SpawnSpaceshipClones();
        }

        private void SpawnSpaceship()
        {
            InitialMovementData initialMovementData = new  InitialMovementData(Vector2.zero, 0f, Vector2.zero);
            var spawnData = new SpaceshipSpawnData(initialMovementData, _spaceshipMovementConfig, _spaceshipConfig.maxHp);
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
                var clone = _spaceshipCloneFactory.Create(spawnData);
                _spaceshipCloneStorage.Add(clone);
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameInitializeSignal>(OnGameInitialize);
            _signalBus.Unsubscribe<GameStartSignal>(OnGameStarted);
        }
    }
}