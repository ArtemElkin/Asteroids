using System;
using _Project.Core.Signals;
using _Project.Features.Gameplay.Signals;
// TODO осталась зависимость отUnity
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay
{
    public class SpawnTimer<T> : IInitializable, ITickable, IDisposable
    {
        private bool _isEnabled;
        private float _timeFromLastRequest;
        private readonly float _spawnInterval = 3f;
        private readonly SignalBus _signalBus;


        public SpawnTimer(
            SignalBus signalBus)
        {
            _signalBus =  signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameStartedSignal>(Start);
            _signalBus.Subscribe<GameOverSignal>(Stop);
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
            _signalBus.Unsubscribe<GameStartedSignal>(Start);
            _signalBus.Unsubscribe<GameOverSignal>(Stop);
        }
    }
}