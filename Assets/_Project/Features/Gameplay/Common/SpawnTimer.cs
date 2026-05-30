using System;
using _Project.Core.Config;
using _Project.Core.Services;
using _Project.Core.Signals;

namespace _Project.Features.Gameplay.Common
{
    public class SpawnTimer<T> : IDisposable
    {
        public event Action OnSpawnRequested;
        private bool _isEnabled;
        private float _timeFromLastRequest;
        private float _spawnInterval;
        private readonly ISignalBus _signalBus;
        private readonly ITimeService _timeService;
        private readonly IConfigProvider _configProvider;


        public SpawnTimer(
            IConfigProvider configProvider,
            ITimeService timeService,
            ISignalBus signalBus)
        {
            _configProvider = configProvider;
            _timeService = timeService;
            _signalBus =  signalBus;
            _signalBus.Subscribe<InitializeGameSignal>(Initialize);
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
            _timeFromLastRequest += _timeService.DeltaTime;
        }

        private void Start() => _isEnabled = true;

        private void Stop() => _isEnabled = false;

        public void Dispose()
        {
            _signalBus.Unsubscribe<InitializeGameSignal>(Initialize);
            _signalBus.Unsubscribe<StartGameSignal>(Start);
            _signalBus.Unsubscribe<StopGameSignal>(Stop);
        }
    }
}