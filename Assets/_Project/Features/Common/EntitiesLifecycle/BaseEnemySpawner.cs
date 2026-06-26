using System;
using _Project.Core.EventBus;
using _Project.Core.GameLifecycle;
using _Project.Core.Services;
using _Project.Core.Tools;

namespace _Project.Features.Common.EntitiesLifecycle
{
    public abstract class BaseEnemySpawner<T> : IDisposable where T : class
    {
        protected abstract int MaxCount { get; }
        protected abstract float SpawnInterval { get; }
        protected readonly Storage<T> _storage;
        private readonly Timer _spawnTimer;
        private readonly IGameStateService  _gameStateService;

        
        protected BaseEnemySpawner(
            Storage<T> storage,
            Timer spawnTimer,
            IGameStateService gameStateService)
        {
            _storage = storage;
            _spawnTimer =  spawnTimer;
            _gameStateService = gameStateService;
            _gameStateService.OnGameStateChanged += OnGameStateChanged;
            _spawnTimer.Elapsed += OnSpawnRequested;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Initialize:
                    _spawnTimer.Start(SpawnInterval, true);
                    break;
                case GameState.Paused:
                    _spawnTimer.Pause();
                    break;
                case GameState.Running:
                    _spawnTimer.Resume();
                    break;
                case GameState.GameOver:
                    _spawnTimer.Stop();
                    break;
            }
        }

        private void OnSpawnRequested()
        {
            if (_storage.Count < MaxCount)
            {
                T entity = Spawn();
                _storage.Add(entity);
            }
        }

        protected abstract T Spawn();

        public virtual void Dispose()
        {
            _gameStateService.OnGameStateChanged -= OnGameStateChanged;
            _spawnTimer.Elapsed -= OnSpawnRequested;
            _spawnTimer.Dispose();
        }
    }
}