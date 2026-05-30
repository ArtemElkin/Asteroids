using System;
using _Project.Core.Config;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Common;
using _Project.Features.Gameplay.Spaceship;
using _Project.Infrastructure.Factories;
using Zenject;

namespace _Project.Features.Gameplay.UFO
{
    public class UFOBuilder : IDisposable
    {
        private float _spawnOffsetFromBounds;
        private float _minUFOSpeed;
        private float _maxUFOSpeed;
        private float _ufoAccelerationMultiplier;
        private float _ufoInertiaMultiplier;
        private MovementModel _movementModel;
        private UFOMovementController _movementController;
        private UFORotationController _rotationController;
        private UFOTargetFollower _targetFollower;
        private BoundsChecker _boundsChecker;
        private readonly PositionGenerator _positionGenerator;
        private readonly IRandomService _randomService;
        private readonly FactoryWithPool<UFOComponent> _ufoFactory;
        private readonly IConfigProvider _configProvider;
        private readonly DiContainer _diContainer;
        private readonly ISignalBus _signalBus;


        public UFOBuilder(
            PositionGenerator positionGenerator,
            IRandomService randomService,
            FactoryWithPool<UFOComponent> ufoFactory,
            IConfigProvider configProvider,
            DiContainer diContainer,
            ISignalBus signalBus)
        {
            _positionGenerator = positionGenerator;
            _randomService = randomService;
            _ufoFactory = ufoFactory;
            _configProvider = configProvider;
            _diContainer = diContainer;
            _signalBus = signalBus;
            _signalBus.Subscribe<InitializeGameSignal>(Initialize);
        }
        
        private void Initialize()
        {
            var ufoConfig =  _configProvider.GetConfig<UFOConfig>("UFOConfig");
            _minUFOSpeed = ufoConfig.minSpeed;
            _maxUFOSpeed = ufoConfig.maxSpeed;
            _ufoAccelerationMultiplier = ufoConfig.accelerationMultiplier;
            _ufoInertiaMultiplier = ufoConfig.inertiaMultiplier;
        }

        public UFOBuilder SetSpawnOffsetFromBounds(float offsetFromBounds)
        {
            _spawnOffsetFromBounds = offsetFromBounds;
            return this;
        }

        private UFOBuilder AddMovementModel()
        {
            if (_movementModel != null) return this;
            
            var initialPosition = GetRandomInitialUFOPosition();
            var initialSpeed = GetRandomInitialUFOSpeed();
            
            _movementModel = _diContainer.Resolve<MovementModel>();
            _movementModel.Init(initialPosition, initialSpeed);
            return this;
        }

        public UFOBuilder AddMovementController()
        {
            if (_movementModel == null) AddMovementModel();
            if (_movementController != null) return this;
            
            _movementController = _diContainer.Resolve<UFOMovementController>();
            _movementController.Setup(
                _movementModel,
                _ufoAccelerationMultiplier,
                _ufoInertiaMultiplier);
            return this;
        }

        public UFOBuilder AddRotationController()
        {
            if (_rotationController != null) return this;
            if (_movementModel == null) AddMovementModel();
            
            _rotationController = _diContainer.Resolve<UFORotationController>();
            _rotationController.Setup(_movementModel);
            return this;
        }

        public UFOBuilder AddTargetFollower(Storage<SpaceshipComponent> spaceshipStorage)
        {
            if (_targetFollower != null) return this;
            if (_movementModel == null) AddMovementModel();
            
            _targetFollower = _diContainer.Resolve<UFOTargetFollower>();
            _targetFollower.Setup(_movementModel, spaceshipStorage);
            return this;
        }

        public UFOBuilder AddBoundsChecker()
        {
            if (_boundsChecker != null) return this;
            if (_movementModel == null) AddMovementModel();
            if (_movementController == null) AddMovementController();
            
            _boundsChecker = _diContainer.Resolve<BoundsChecker>();
            _boundsChecker.Setup(_movementModel, _movementController);
            return this;
        }

        public UFOComponent Build()
        {
            var ufo = _ufoFactory.Create(_movementModel.Position);
            ufo.Setup(
                _movementModel, 
                _movementController,
                _rotationController,
                _targetFollower,
                _boundsChecker);
            
            Clear();
            
            return ufo;
        }
        
        private Vector2 GetRandomInitialUFOPosition()
        {
            return _positionGenerator.GenerateRandomPositionOutOfScreen(_spawnOffsetFromBounds);
        }

        private float GetRandomInitialUFOSpeed()
        {
            return _randomService.GetRandomFloat(min: _minUFOSpeed, max: _maxUFOSpeed);
        }

        private void Clear()
        {
            _movementModel = null;
            _movementController = null;
            _rotationController = null;
            _targetFollower = null;
            _boundsChecker = null;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<InitializeGameSignal>(Initialize);
            Clear();
        }
    }
}