using System;
using _Project.Core.Config;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Common;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipSpawner : IDisposable
    {
        private IReadOnlyPositionable _positionableModel;
        private IReadOnlyRotatable _rotatableModel;
        private float _spaceshipMaxSpeed;
        private float _spaceshipAccelerationMultiplier;
        private float _spaceshipInertiaMultiplier;
        private readonly SpaceshipCloneComponent _spaceshipClonePrefab;
        private readonly SpaceshipComponent _spaceshipPrefab;
        private readonly Transform _spaceshipParentTransform;
        private readonly Storage<SpaceshipComponent> _spaceshipStorage;
        private readonly IInstantiator _instantiator;
        private readonly ISignalBus _signalBus;
        private readonly IScreenService _screenService;
        private readonly IConfigProvider _configProvider;
        private readonly DiContainer _diContainer;

        
        public SpaceshipSpawner(
            SpaceshipComponent spaceshipPrefab,
            SpaceshipCloneComponent spaceshipClonePrefab,
            Transform spaceshipParentTransform,
            Storage<SpaceshipComponent> spaceshipStorage,
            IInstantiator instantiator,
            ISignalBus signalBus,
            IScreenService screenService,
            IConfigProvider configProvider,
            DiContainer diContainer)
        {
            _spaceshipPrefab = spaceshipPrefab;
            _spaceshipClonePrefab = spaceshipClonePrefab;
            _spaceshipParentTransform =  spaceshipParentTransform;
            _spaceshipStorage = spaceshipStorage;
            _instantiator = instantiator;
            _signalBus = signalBus;
            _screenService = screenService;
            _configProvider = configProvider;
            _diContainer =  diContainer;
            _signalBus.Subscribe<InitializeGameSignal>(Initialize);
            _signalBus.Subscribe<StartGameSignal>(OnGameStarted);
        }

        public void Initialize()
        {
            var config = _configProvider.GetConfig<SpaceshipMovementConfig>("SpaceshipMovementConfig");
            _spaceshipMaxSpeed = config.maxSpeed;
            _spaceshipAccelerationMultiplier = config.accelerationMultiplier;
            _spaceshipInertiaMultiplier = config.inertiaMultiplier;
        }

        private void OnGameStarted()
        {
            SpawnSpaceship();
            SpawnSpaceshipClones();
        }

        private void SpawnSpaceship()
        {
            var spaceship = _instantiator.InstantiatePrefabForComponent<SpaceshipComponent>(_spaceshipPrefab, _spaceshipParentTransform);
            
            var movementModel = _diContainer.Resolve<MovementModel>();
            movementModel.Init(Vector2.zero, 0);
            _positionableModel = movementModel;
            _rotatableModel = movementModel;

            var movementController = _diContainer.Resolve<SpaceshipMovementController>();
            movementController.Setup(
                movementModel, 
                _spaceshipMaxSpeed, 
                _spaceshipAccelerationMultiplier,
                _spaceshipInertiaMultiplier);
            
            var rotationController = _diContainer.Resolve<SpaceshipRotationController>();
            rotationController.Setup(movementModel);
            
            var boundsChecker = _diContainer.Resolve<BoundsChecker>();
            boundsChecker.Setup(movementModel, movementController);
            
            spaceship.Setup(
                movementModel,
                movementController,
                rotationController,
                boundsChecker);
            
            _spaceshipStorage.Add(spaceship);
            
            _signalBus.Fire(new SpawnedSignal<SpaceshipComponent>(spaceship));
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
                var clone = _instantiator.InstantiatePrefabForComponent<SpaceshipCloneComponent>(_spaceshipClonePrefab, _spaceshipParentTransform);
                clone.Setup(offset, _positionableModel, _rotatableModel);
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<InitializeGameSignal>(Initialize);
            _signalBus.Unsubscribe<StartGameSignal>(OnGameStarted);
        }
    }
}