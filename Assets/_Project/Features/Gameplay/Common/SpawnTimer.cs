using System;
using _Project.Core.Config;
using _Project.Core.Signals;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;
// TODO осталась зависимость отUnity


namespace _Project.Features.Gameplay.Common
{
    public class SpawnTimer<T> : IInitializable, ITickable, IDisposable
    {
        private bool _isEnabled;
        private float _timeFromLastRequest;
        private float _spawnInterval;
        private readonly ISignalBus _signalBus;
        private readonly IConfigProvider _configProvider;


        public SpawnTimer(
            ISignalBus signalBus,
            IConfigProvider configProvider)
        {
            _signalBus =  signalBus;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<StartGameSignal>(Start);
            _signalBus.Subscribe<GameOverSignal>(Stop);

            var config = _configProvider.GetConfig<GameConfig>("GameConfig");
            _spawnInterval = config.spawnInterval;
        }
        
        public void Tick()
        {
            if (!_isEnabled) return;
            if (_timeFromLastRequest >= _spawnInterval)
            {
                _signalBus.Fire<SpawnRequestedSignal<T>>();
                _timeFromLastRequest = 0;
            }
            _timeFromLastRequest += Time.deltaTime;
        }

        private void Start() => _isEnabled = true;

        private void Stop() => _isEnabled = false;

        public void Dispose()
        {
            _signalBus.Unsubscribe<StartGameSignal>(Start);
            _signalBus.Unsubscribe<GameOverSignal>(Stop);
        }
    }
}