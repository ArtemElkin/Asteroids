using System;
using _Project.Core.Services;
using _Project.Core.Signals;

namespace _Project.Features.Common
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
            _signalBus.Subscribe<GameStartSignal>(Start);
            _signalBus.Subscribe<GameStopSignal>(Stop);
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

        public void Start() => _isEnabled = true;

        private void Stop() => _isEnabled = false;

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStartSignal>(Start);
            _signalBus.Unsubscribe<GameStopSignal>(Stop);
            _timeService.OnTick -= OnTick;
        }
    }
}