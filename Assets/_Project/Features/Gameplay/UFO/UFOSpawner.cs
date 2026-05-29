using _Project.Core.Infrastructure.Config;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Asteroid;
using _Project.Features.Gameplay.Signals;
using _Project.Features.Gameplay.Spaceship;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.UFO
{
    public class UFOSpawner
    {
        private float _spawnOffsetFromBounds;
        private float _minUFOSpeed;
        private float _maxUFOSpeed;
        private int _maxUFOsCount;
        private readonly Storage<UFOComponent> _ufoStorage;
        private readonly Storage<SpaceshipComponent> _spaceshipStorage;
        private readonly FactoryWithPool<UFOComponent> _ufoFactory;
        private readonly RandomService _randomService;
        private readonly PositionGenerator _positionGenerator;
        private readonly IConfigProvider _configProvider;
        private readonly SignalBus _signalBus;
        private readonly DiContainer _diContainer;


        public UFOSpawner(
            Storage<UFOComponent> ufoStorage,
            Storage<SpaceshipComponent> spaceshipStorage,
            FactoryWithPool<UFOComponent> ufoFactory,
            RandomService randomService,
            PositionGenerator positionGenerator,
            IConfigProvider configProvider,
            SignalBus signalBus,
            DiContainer diContainer)
        {
            _ufoStorage = ufoStorage;
            _spaceshipStorage = spaceshipStorage;
            _ufoFactory = ufoFactory;
            _randomService = randomService;
            _positionGenerator = positionGenerator;
            _configProvider = configProvider;
            _signalBus = signalBus;
            _diContainer = diContainer;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<SpawnRequestedSignal<UFOComponent>>(OnSpawnRequested);
            
            var gameConfig = _configProvider.GetConfigFromJson<GameConfig>("GameConfig");
            _maxUFOsCount = gameConfig.maxUFOsCount;
            _spawnOffsetFromBounds = gameConfig.spawnOffsetFromBounds;
            
            var ufoConfig =  _configProvider.GetConfigFromJson<UFOConfig>("UFOConfig");
            _minUFOSpeed = ufoConfig.minSpeed;
            _maxUFOSpeed = ufoConfig.maxSpeed;
        }

        private void OnSpawnRequested()
        {
            if (_ufoStorage.Count < _maxUFOsCount)
            {
                SpawnUFO();
            }
        }

        private void SpawnUFO()
        {
            var initialPosition = GetRandomInitialUFOPosition();
            var initialSpeed = GetRandomInitialUFOSpeed();
            
            var ufo = _ufoFactory.Create(initialPosition);
            _ufoStorage.Add(ufo);
            
            var movementModel = _diContainer.Resolve<MovementModel>();
            movementModel.Init((Vector2)ufo.transform.position, initialSpeed);

            var movementController = _diContainer.Resolve<UFOMovementController>();
            movementController.Setup(movementModel);
            
            var rotationController = _diContainer.Resolve<UFORotationController>();
            rotationController.Setup(movementModel);
            
            var targetFollower = _diContainer.Resolve<UFOTargetFollower>();
            targetFollower.Setup(movementModel,_spaceshipStorage);
            
            
            ufo.Setup(
                movementModel, 
                movementController,
                rotationController,
                targetFollower);
        }

        private CustomVector2 GetRandomInitialUFOPosition()
        {
            return _positionGenerator.GenerateRandomPositionOutOfScreen(_spawnOffsetFromBounds);
        }

        private float GetRandomInitialUFOSpeed()
        {
            return _randomService.GetRandomFloat(min: _minUFOSpeed, max: _maxUFOSpeed);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SpawnRequestedSignal<AsteroidComponent>>(OnSpawnRequested);
        }
    }
}