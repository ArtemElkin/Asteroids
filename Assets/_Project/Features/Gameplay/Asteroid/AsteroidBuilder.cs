using _Project.Core.Config;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Bounds;
using _Project.Infrastructure.Factories;
using Zenject;

namespace _Project.Features.Gameplay.Asteroid
{
    public class AsteroidBuilder
    {
        private float _spawnOffsetFromBounds;
        private float _maxAsteroidSpeed;
        private float _minAsteroidSpeed;
        private MovementModel _movementModel;
        private AsteroidMovementController _movementController;
        private BoundsChecker _boundsChecker;
        private readonly PositionGenerator _positionGenerator;
        private readonly IRandomService _randomService;
        private readonly FactoryWithPool<AsteroidComponent> _factory;
        private readonly IConfigProvider _configProvider;
        private readonly DiContainer _diContainer;
        private readonly ISignalBus _signalBus;


        public AsteroidBuilder(
            PositionGenerator positionGenerator,
            IRandomService randomService,
            FactoryWithPool<AsteroidComponent> factory,
            IConfigProvider configProvider,
            DiContainer diContainer,
            ISignalBus signalBus)
        {
            _positionGenerator = positionGenerator;
            _randomService = randomService;
            _factory = factory;
            _configProvider = configProvider;
            _diContainer = diContainer;
            _signalBus = signalBus;
            _signalBus.Subscribe<InitializeGameSignal>(Initialize);
        }
        
        private void Initialize()
        {
            var asteroidConfig =  _configProvider.GetConfig<AsteroidConfig>("AsteroidConfig");
            _minAsteroidSpeed = asteroidConfig.minSpeed;
            _maxAsteroidSpeed = asteroidConfig.maxSpeed;
        }

        public AsteroidBuilder SetSpawnOffsetFromBounds(float offsetFromBounds)
        {
            _spawnOffsetFromBounds = offsetFromBounds;
            return this;
        }

        private AsteroidBuilder AddMovementModel()
        {
            if (_movementModel != null) return this;
            
            var initialPosition = GetRandomInitialPosition();
            var initialSpeed = GetRandomInitialSpeed();
            var initialDirection = GetRandomInitialDirection(initialPosition);
            
            _movementModel = _diContainer.Resolve<MovementModel>();
            _movementModel.Init(initialPosition, initialSpeed);
            _movementModel.UpdateMoveDirection(initialDirection);
            
            return this;
        }

        public AsteroidBuilder AddMovementController()
        {
            if (_movementModel == null) AddMovementModel();
            if (_movementController != null) return this;
            
            _movementController = _diContainer.Resolve<AsteroidMovementController>();
            _movementController.Setup(
                _movementModel);
            return this;
        }

        public AsteroidBuilder AddBoundsChecker()
        {
            if (_boundsChecker != null) return this;
            if (_movementModel == null) AddMovementModel();
            if (_movementController == null) AddMovementController();
            
            _boundsChecker = _diContainer.Resolve<BoundsChecker>();
            _boundsChecker.Setup(_movementModel, _movementController);
            return this;
        }

        public AsteroidComponent Build()
        {
            var asteroid = _factory.Create(_movementModel.Position);
            asteroid.Setup(
                _movementModel, 
                _movementController,
                _boundsChecker);
            
            Clear();
            
            return asteroid;
        }
        
        private Vector2 GetRandomInitialPosition()
        {
            return _positionGenerator.GenerateRandomPositionOutOfScreen(_spawnOffsetFromBounds);
        }

        private float GetRandomInitialSpeed()
        {
            return _randomService.GetRandomFloat(min: _minAsteroidSpeed, max: _maxAsteroidSpeed);
        }
        
        private Vector2 GetRandomInitialDirection(Vector2 initialPosition)
        {
            var target = _positionGenerator.GenerateRandomPositionOnScreen();
            return (target - initialPosition).normalized;
        }

        private void Clear()
        {
            _movementModel = null;
            _movementController = null;
            _boundsChecker = null;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<InitializeGameSignal>(Initialize);
            Clear();
        }
    }
}