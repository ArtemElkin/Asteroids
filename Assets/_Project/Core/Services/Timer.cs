using System;

namespace _Project.Core.Services
{
    public class Timer : IDisposable
    {
        public event Action Elapsed;
        public event Action<float> OnTimeLeftChanged;
        private bool _loop;
        public bool Enabled { get; private set; }
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
            if (!Enabled) return;
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
                    Enabled = false;
                }
            }
            OnTimeLeftChanged?.Invoke(_duration -  _elapsedTime);;
        }

        public void Start(float duration, bool loop = false)
        {
            _duration = duration;
            _loop = loop;
            _elapsedTime = 0;
            Enabled = true;
        }

        public void Stop()
        {
            Enabled = false;
            _elapsedTime = 0;
            _duration = 0;
        }
        
        public void Pause() => Enabled = false;
        public void Resume() => Enabled = true;

        public void Dispose()
        {
            _timeService.OnTick -= OnTick;
        }
    }
}