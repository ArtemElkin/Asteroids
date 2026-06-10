using System;
using _Project.Core.EventBus;
using _Project.Core.Tools;

namespace _Project.Features.Common
{
    public abstract class BaseSpawner<T> : IDisposable where T : class
    {
        private int _maxCount;
        private readonly Storage<T> _storage;
        private readonly SpawnTimer _spawnTimer;
        protected readonly IEventBus _signalBus;

        protected BaseSpawner(
            Storage<T> storage,
            SpawnTimer spawnTimer,
            IEventBus eventBus)
        {
            _storage = storage;
            _spawnTimer =  spawnTimer;
            _signalBus = eventBus;
            _signalBus.Subscribe<GameInitializeEvent>(Initialize);
            _signalBus.Subscribe<GameStartEvent>(OnGameStart);
            _signalBus.Subscribe<GameStopEvent>(OnGameStop);
            _spawnTimer.OnSpawnRequested += OnSpawnRequested;
        }

        private void Initialize()
        {
            OnInitialize();
            _maxCount = GetMaxCount();
            _spawnTimer.Setup(GetSpawnInterval());
        }

        private void OnGameStart()
        {
            _spawnTimer.Start();
        }

        private void OnGameStop()
        {
            _spawnTimer.Stop();
        }

        protected virtual void OnInitialize() { }

        protected abstract int GetMaxCount();
        protected abstract float GetSpawnInterval();

        private void OnSpawnRequested()
        {
            if (_storage.Count < _maxCount)
            {
                T entity = Spawn();
                _storage.Add(entity);
            }
        }

        protected abstract T Spawn();

        public virtual void Dispose()
        {
            _maxCount = 0;
            _spawnTimer.OnSpawnRequested -= OnSpawnRequested;
            _spawnTimer.Dispose();
            _signalBus.Unsubscribe<GameInitializeEvent>(Initialize);
            _signalBus.Unsubscribe<GameStartEvent>(OnGameStart);
            _signalBus.Unsubscribe<GameStopEvent>(OnGameStop);
        }
    }
}