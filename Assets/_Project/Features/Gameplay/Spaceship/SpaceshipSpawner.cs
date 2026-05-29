using System;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Signals;
using _Project.Core.Tools;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipSpawner : IInitializable, IDisposable
    {
        private IReadOnlyPositionable _positionableModel;
        private IReadOnlyRotatable _rotatableModel;
        private float _spaceshipMaxSpeed;
        private readonly SpaceshipCloneComponent _spaceshipClonePrefab;
        private readonly SpaceshipComponent _spaceshipPrefab;
        private readonly IInstantiator _instantiator;
        private readonly SignalBus _signalBus;
        private readonly ScreenService _screenService;
        private readonly IConfigProvider _configProvider;

        
        public SpaceshipSpawner(
            SpaceshipComponent spaceshipPrefab,
            SpaceshipCloneComponent spaceshipClonePrefab,
            IInstantiator instantiator,
            SignalBus signalBus,
            ScreenService screenService,
            IConfigProvider configProvider)
        {
            _spaceshipPrefab = spaceshipPrefab;
            _spaceshipClonePrefab = spaceshipClonePrefab;
            _instantiator = instantiator;
            _signalBus = signalBus;
            _screenService = screenService;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
            var config = _configProvider.GetConfigFromJson<SpaceshipMovementConfig>("SpaceshipMovementConfig");
            _spaceshipMaxSpeed = config.maxSpeed;
        }

        private void OnGameStarted()
        {
            SpawnSpaceship();
            SpawnSpaceshipClones();
        }

        private void SpawnSpaceship()
        {
            var spaceship = _instantiator.InstantiatePrefabForComponent<SpaceshipComponent>(_spaceshipPrefab);
            _positionableModel = spaceship.GetPositionable();
            _rotatableModel = spaceship.GetRotatable();
            spaceship.Setup(_spaceshipMaxSpeed);
        }

        private void SpawnSpaceshipClones()
        {
            var width = _screenService.ScreenWidth;
            var height = _screenService.ScreenHeight;

            CustomVector2[] cloneOffsets = 
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
                var clone = _instantiator.InstantiatePrefabForComponent<SpaceshipCloneComponent>(_spaceshipClonePrefab);
                clone.Setup(offset, _positionableModel, _rotatableModel);
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        }
    }
}