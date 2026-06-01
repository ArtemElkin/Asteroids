using System;
using _Project.Core.Services;
using UnityEngine;

namespace _Project.Infrastructure.UnityServices
{
    public class UnityTimeService : MonoBehaviour, ITimeService
    {
        public float DeltaTime => Time.deltaTime;
        public float FixedDeltaTime => Time.fixedDeltaTime;
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