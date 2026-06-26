using System;
using UnityEngine;

namespace _Project.Core.Services
{
    public class Timer : IDisposable
    {
        public event Action Elapsed;
        public event Action<float> OnTimeLeftChanged;
        private bool _loop;
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
                Elapsed?.Invoke();
                if (_loop)
                {
                    Start(_duration, _loop);
                }
                else
                {
                    _elapsedTime = 0;
                    _isEnabled = false;
                }
            }
            OnTimeLeftChanged?.Invoke(_duration -  _elapsedTime);;
        }

        public void Start(float duration, bool loop = false)
        {
            _duration = duration;
            _loop = loop;
            _elapsedTime = 0;
            _isEnabled = true;
        }

        public void Stop()
        {
            _isEnabled = false;
            _elapsedTime = 0;
            _duration = 0;
        }
        
        public void Pause() => _isEnabled = false;
        public void Resume() => _isEnabled = true;

        public void Dispose()
        {
            _timeService.OnTick -= OnTick;
        }
    }
}