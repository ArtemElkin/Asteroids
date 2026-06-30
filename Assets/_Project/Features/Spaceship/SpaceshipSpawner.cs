using System;
using _Project.Core.Config;
using _Project.Core.EventBus;
using _Project.Core.GameLifecycle;
using _Project.Core.Physics.Movement;
using _Project.Core.StaticData;
using _Project.Core.Tools;
using _Project.Features.Common.Settings;
using _Project.Features.Spaceship.Config;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.Spaceship
{
    public class SpaceshipSpawner : IDisposable
    {
        private readonly SpaceshipConfig _config;
        private readonly Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> _factory;
        private readonly Storage<SpaceshipFacade> _storage;
        private readonly SettingsModel _settingsModel;
        private readonly IGameStateService _gameStateService;
        private readonly IEventBus _eventBus;

        
        public SpaceshipSpawner(
            Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade> factory, 
            Storage<SpaceshipFacade> storage,
            SettingsModel settingsModel,
            IConfigProvider configProvider,
            IGameStateService gameStateService)
        {
            _factory = factory;
            _storage = storage;
            _settingsModel = settingsModel;
            _gameStateService = gameStateService;
            _config =  configProvider.GetConfig<SpaceshipConfig>(FileNames.Config.Entities.Spaceship);
            _gameStateService.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Initialize) SpawnSpaceship();
        }

        private void SpawnSpaceship()
        {
            var initialMovementData = new  InitialMovementData(
                _config.movementConfig.mass, 
                Vector2.zero, 
                Vector2.zero);
            var spawnData = new SpaceshipSpawnData(
                initialMovementData,
                _settingsModel.SpaceshipClonesEnabled,
                _config);
            var spaceship = _factory.Create(spawnData);
            _storage.Add(spaceship);
        }

        public void Dispose()
        {
            _gameStateService.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}