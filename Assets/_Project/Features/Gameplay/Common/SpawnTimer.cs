using System;
using _Project.Core.Config;
using _Project.Core.Signals;
using _Project.Features.Gameplay.Signals;
// TODO осталась зависимость отUnity
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Common
{
    public class SpawnTimer<T> : IInitializable, ITickable, IDisposable
    {
        public event Action OnSpawnRequested;
        private bool _isEnabled;
        private float _timeFromLastRequest;
        private float _spawnInterval;
        private readonly ISignalBus _signalBus;
        private readonly IConfigProvider _configProvider;


        public SpawnTimer(
            IConfigProvider configProvider,
            ISignalBus signalBus)
        {
            _configProvider = configProvider;
            _signalBus =  signalBus;
            _signalBus.Subscribe<StartGameSignal>(Start);
            _signalBus.Subscribe<StopGameSignal>(Stop);
        }

        public void Initialize()
        {
            var config = _configProvider.GetConfig<GameConfig>("GameConfig");
            _spawnInterval = config.spawnInterval;
        }
        
        public void Tick()
        {
            if (!_isEnabled) return;
            if (_timeFromLastRequest >= _spawnInterval)
            {
                OnSpawnRequested?.Invoke();
                _timeFromLastRequest = 0;
            }
            _timeFromLastRequest += Time.deltaTime;
        }

        private void Start() => _isEnabled = true;

        private void Stop() => _isEnabled = false;

        public void Dispose()
        {
            _signalBus.Unsubscribe<StartGameSignal>(Start);
            _signalBus.Unsubscribe<StopGameSignal>(Stop);
        }
    }
}