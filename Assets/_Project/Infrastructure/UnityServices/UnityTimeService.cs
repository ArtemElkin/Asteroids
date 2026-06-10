using System;
using _Project.Core.Services;
using UnityEngine;

namespace _Project.Infrastructure.UnityServices
{
    public class UnityTimeService : MonoBehaviour, ITimeService
    {
        [SerializeField] private float _timeScale = 1f;
        public event Action<float> OnTick;
        public event Action<float> OnFixedTick;

        
        private void Update()
        {
            Time.timeScale = _timeScale;
            OnTick?.Invoke(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            OnFixedTick?.Invoke(Time.fixedDeltaTime);
        }
    }
}