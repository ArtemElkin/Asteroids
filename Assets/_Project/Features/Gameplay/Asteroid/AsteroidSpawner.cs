using System;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Asteroid
{
    public class AsteroidSpawner : IInitializable, IDisposable
    {
        private float _spawnOffsetFromBounds;
        private float _minAsteroidSpeed;
        private float _maxAsteroidSpeed;
        private int _maxAsteroidsCount;
        private readonly FactoryWithPool<AsteroidComponent> _asteroidFactory;
        private readonly RandomService _randomService;
        private readonly PositionGenerator _positionGenerator;
        private readonly IConfigProvider _configProvider;
        private readonly SignalBus _signalBus;


        public AsteroidSpawner(
            FactoryWithPool<AsteroidComponent> asteroidFactory,
            RandomService randomService,
            PositionGenerator positionGenerator,
            IConfigProvider configProvider,
            SignalBus signalBus)
        {
            _asteroidFactory = asteroidFactory;
            _randomService = randomService;
            _positionGenerator = positionGenerator;
            _configProvider = configProvider;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<SpawnRequestedSignal<AsteroidComponent>>(SpawnAsteroid);
            
            var gameConfig = _configProvider.GetConfigFromJson<GameConfig>("GameConfig");
            _maxAsteroidsCount = gameConfig.maxAsteroidsCount;
            _spawnOffsetFromBounds = gameConfig.spawnOffsetFromBounds;
            
            var asteroidConfig =  _configProvider.GetConfigFromJson<AsteroidConfig>("AsteroidConfig");
            _minAsteroidSpeed = asteroidConfig.minSpeed;
            _maxAsteroidSpeed = asteroidConfig.maxSpeed;
        }

        private void SpawnAsteroid()
        {
            var initialPosition = GetRandomInitialAsteroidPosition();
            var initialDirection = GetRandomInitialAsteroidDirection(initialPosition);
            var initialSpeed = GetRandomInitialAsteroidSpeed();
            var asteroid = _asteroidFactory.Create(initialPosition);
            asteroid.Setup(initialDirection, initialSpeed);
        }

        private Vector2 GetRandomInitialAsteroidPosition()
        {
            return _positionGenerator.GenerateRandomPositionOutOfScreen(_spawnOffsetFromBounds);
        }

        private Vector2 GetRandomInitialAsteroidDirection(Vector2 initialPosition)
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
            _signalBus.Unsubscribe<SpawnRequestedSignal<AsteroidComponent>>(SpawnAsteroid);
        }
    }
}