using System;
using _Project.Core.Signals;
using _Project.Core.Tools;

namespace _Project.Features.Common
{
    public abstract class BaseSpawner<T> : IDisposable where T : class
    {
        private int _maxCount;
        private readonly Storage<T> _storage;
        private readonly SpawnTimer _spawnTimer;
        private readonly ISignalBus _signalBus;

        protected BaseSpawner(
            Storage<T> storage,
            SpawnTimer spawnTimer,
            ISignalBus signalBus)
        {
            _storage = storage;
            _spawnTimer =  spawnTimer;
            _signalBus = signalBus;
            _signalBus.Subscribe<GameInitializeSignal>(Initialize);
            _spawnTimer.OnSpawnRequested += OnSpawnRequested;
        }

        private void Initialize()
        {
            OnInitialize();
            _maxCount = GetMaxCount();
            _spawnTimer.Setup(GetSpawnInterval());
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

        public void Dispose()
        {
            _storage.Clear();
            _maxCount = 0;
            _spawnTimer.OnSpawnRequested -= OnSpawnRequested;
            _signalBus.Unsubscribe<GameInitializeSignal>(Initialize);
        }
    }
}