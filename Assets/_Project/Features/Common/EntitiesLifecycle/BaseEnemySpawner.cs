using System;
using _Project.Core.EventBus;
using _Project.Core.GameLifecycle.Events;
using _Project.Core.Tools;

namespace _Project.Features.Common.EntitiesLifecycle
{
    public abstract class BaseEnemySpawner<T> : IDisposable where T : class
    {
        private int _maxCount;
        private readonly Storage<T> _storage;
        private readonly SpawnTimer _spawnTimer;
        protected readonly IEventBus _eventBus;

        
        protected BaseEnemySpawner(
            Storage<T> storage,
            SpawnTimer spawnTimer,
            IEventBus eventBus)
        {
            _storage = storage;
            _spawnTimer =  spawnTimer;
            _eventBus = eventBus;
            _eventBus.Subscribe<GameInitializeEvent>(Initialize);
            _eventBus.Subscribe<GameStartEvent>(OnGameStart);
            _eventBus.Subscribe<GameStopEvent>(OnGameStop);
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
            _spawnTimer.OnSpawnRequested -= OnSpawnRequested;
            _spawnTimer.Dispose();
            _eventBus.Unsubscribe<GameInitializeEvent>(Initialize);
            _eventBus.Unsubscribe<GameStartEvent>(OnGameStart);
            _eventBus.Unsubscribe<GameStopEvent>(OnGameStop);
        }
    }
}