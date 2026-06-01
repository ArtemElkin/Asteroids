using System;
using _Project.Core.Services;
using _Project.Core.Signals;

namespace _Project.Features.Gameplay.Common
{
    public class SpawnTimer : IDisposable
    {
        public event Action OnSpawnRequested;
        private bool _isEnabled;
        private float _timeFromLastRequest;
        private float _spawnInterval;
        private readonly ISignalBus _signalBus;
        private readonly ITimeService _timeService;


        public SpawnTimer(
            ITimeService timeService,
            ISignalBus signalBus)
        {
            _timeService = timeService;
            _signalBus =  signalBus;
            _signalBus.Subscribe<StartGameSignal>(Start);
            _signalBus.Subscribe<StopGameSignal>(Stop);
            _timeService.OnTick += OnTick;
        }

        public void Setup(float spawnInterval)
        {
            _spawnInterval = spawnInterval;
        }
        
        private void OnTick()
        {
            if (!_isEnabled) return;
            if (_timeFromLastRequest >= _spawnInterval)
            {
                OnSpawnRequested?.Invoke();
                _timeFromLastRequest = 0;
            }
            _timeFromLastRequest += _timeService.DeltaTime;
        }

        private void Start() => _isEnabled = true;

        private void Stop() => _isEnabled = false;

        public void Dispose()
        {
            _signalBus.Unsubscribe<StartGameSignal>(Start);
            _signalBus.Unsubscribe<StopGameSignal>(Stop);
            _timeService.OnTick -= OnTick;
        }
    }
}