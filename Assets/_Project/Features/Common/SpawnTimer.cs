using System;
using _Project.Core.Services;

namespace _Project.Features.Common
{
    public class SpawnTimer : IDisposable
    {
        public event Action OnSpawnRequested;
        private float _spawnInterval;
        private readonly Timer _timer;


        public SpawnTimer(
            Timer timer)
        {
            _timer = timer;
            _timer.Elapsed += OnTimerElapsed;
        }

        public void Setup(float spawnInterval)
        {
            _spawnInterval = spawnInterval;
        }

        private void OnTimerElapsed()
        {
            OnSpawnRequested?.Invoke();
            _timer.Start(_spawnInterval);
        }

        public void Start()
        {
            OnSpawnRequested?.Invoke();
            _timer.Start(_spawnInterval);
        }

        public void Stop() => _timer.Stop();

        public void Dispose()
        {
            _timer.Elapsed -= OnTimerElapsed;
        }
    }
}