using System;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Signals;
using Zenject;


namespace _Project.Features.Gameplay.Asteroid
{
    public class AsteroidSpawner : IInitializable, IDisposable
    {
        private float _spawnOffsetFromBounds;
        private float _minAsteroidSpeed;
        private float _maxAsteroidSpeed;
        private int _maxAsteroidsCount;
        private readonly Storage<AsteroidComponent> _asteroidsStorage;
        private readonly FactoryWithPool<AsteroidComponent> _asteroidFactory;
        private readonly RandomService _randomService;
        private readonly PositionGenerator _positionGenerator;
        private readonly IConfigProvider _configProvider;
        private readonly SignalBus _signalBus;
        private readonly DiContainer _diContainer;


        public AsteroidSpawner(
            Storage<AsteroidComponent> asteroidsStorage,
            FactoryWithPool<AsteroidComponent> asteroidFactory,
            RandomService randomService,
            PositionGenerator positionGenerator,
            IConfigProvider configProvider,
            SignalBus signalBus,
            DiContainer diContainer)
        {
            _asteroidsStorage = asteroidsStorage;
            _asteroidFactory = asteroidFactory;
            _randomService = randomService;
            _positionGenerator = positionGenerator;
            _configProvider = configProvider;
            _signalBus = signalBus;
            _diContainer = diContainer;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<SpawnRequestedSignal<AsteroidComponent>>(OnSpawnRequested);
            var gameConfig = _configProvider.GetConfigFromJson<GameConfig>("GameConfig");
            _maxAsteroidsCount = gameConfig.maxAsteroidsCount;
            _spawnOffsetFromBounds = gameConfig.spawnOffsetFromBounds;
            
            var asteroidConfig =  _configProvider.GetConfigFromJson<AsteroidConfig>("AsteroidConfig");
            _minAsteroidSpeed = asteroidConfig.minSpeed;
            _maxAsteroidSpeed = asteroidConfig.maxSpeed;
        }

        private void OnSpawnRequested()
        {
            if (_asteroidsStorage.Count < _maxAsteroidsCount)
            {
                SpawnAsteroid();
            }
        }

        private void SpawnAsteroid()
        {
            var initialPosition = GetRandomInitialAsteroidPosition();
            var initialDirection = GetRandomInitialAsteroidDirection(initialPosition);
            var initialSpeed = GetRandomInitialAsteroidSpeed();
            
            var asteroid = _asteroidFactory.Create(initialPosition);
            
            var movementModel = _diContainer.Resolve<MovementModel>();
            movementModel.Init(initialPosition, initialSpeed);
            movementModel.UpdateMoveDirection(initialDirection);

            var movementController = _diContainer.Resolve<AsteroidMovementController>();
            movementController.Setup(movementModel);
            
            var boundsChecker = _diContainer.Resolve<BoundsChecker>();
            boundsChecker.Setup(movementModel, movementController);
            
            asteroid.Setup(movementModel, movementController, boundsChecker);
            
            _asteroidsStorage.Add(asteroid);
        }

        private CustomVector2 GetRandomInitialAsteroidPosition()
        {
            return _positionGenerator.GenerateRandomPositionOutOfScreen(_spawnOffsetFromBounds);
        }

        private CustomVector2 GetRandomInitialAsteroidDirection(CustomVector2 initialPosition)
        {
            var target = _positionGenerator.GenerateRandomPositionOnScreen();
            return (target - initialPosition).normalized;
        }

        private float GetRandomInitialAsteroidSpeed()
        {
            return _randomService.GetRandomFloat(min: _minAsteroidSpeed, max: _maxAsteroidSpeed);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SpawnRequestedSignal<AsteroidComponent>>(OnSpawnRequested);
        }
    }
}