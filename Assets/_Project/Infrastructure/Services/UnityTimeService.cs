using System;
using _Project.Core.Services;
using UnityEngine;

namespace _Project.Infrastructure.Services
{
    public class UnityTimeService : MonoBehaviour, ITimeService
    {
        public float DeltaTime => UnityEngine.Time.deltaTime;
        public float FixedDeltaTime => UnityEngine.Time.fixedDeltaTime;
        public event Action OnTick;
        public event Action OnFixedTick;

        
        private void Update()
        {
            OnTick?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedTick?.Invoke();
        }
    }
}