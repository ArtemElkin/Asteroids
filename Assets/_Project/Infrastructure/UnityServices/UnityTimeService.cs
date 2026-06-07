using System;
using _Project.Core.Services;
using UnityEngine;

namespace _Project.Infrastructure.UnityServices
{
    public class UnityTimeService : MonoBehaviour, ITimeService
    {
        public event Action<float> OnTick;
        public event Action<float> OnFixedTick;

        
        private void Update()
        {
            OnTick?.Invoke(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            OnFixedTick?.Invoke(Time.fixedDeltaTime);
        }
    }
}