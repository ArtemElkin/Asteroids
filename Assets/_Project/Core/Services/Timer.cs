using System;

namespace _Project.Core.Services
{
    public class Timer : IDisposable
    {
        public event Action Elapsed;
        private bool _isEnabled;
        private float _elapsedTime;
        private float _duration;
        private readonly ITimeService _timeService;
        

        public Timer(ITimeService timeService)
        {
            _timeService = timeService;
            _timeService.OnTick += OnTick;
        }

        private void OnTick(float deltaTime)
        {
            if (!_isEnabled) return;
            _elapsedTime += deltaTime;
            if (_elapsedTime >= _duration)
            {
                _elapsedTime = 0;
                _isEnabled = false;
                Elapsed?.Invoke();
            }
        }

        public void Start(float duration)
        {
            _duration = duration;
            _elapsedTime = 0;
            _isEnabled = true;
        }

        public void Pause() => _isEnabled = false;
        public void Continue() => _isEnabled = true;

        public void Stop()
        {
            _isEnabled = false;
            _elapsedTime = 0;
            _duration = 0;
        }

        public void Dispose()
        {
            _timeService.OnTick -= OnTick;
        }
    }
}