using System;
using UnityEngine;

namespace _Project.Features.Spaceship.Health
{
    public class HealthController : IDisposable
    {
        public IReadOnlyHealthModel HealthModel => _healthModel;
        private HealthModel _healthModel;
        public bool IsAlive => _healthModel.Hp != 0;
        public event Action OnDeath;

        
        public HealthController(HealthModel healthModel)
        {
            _healthModel = healthModel;
            _healthModel.OnDeath += OnDeathHandler;
        }

        public void ApplyDamage(int damage)
        {
            _healthModel.DecreaseHp(damage);
            Debug.Log($"Damage applied. Spaceship hp: {_healthModel.Hp}");
        }
        
        private void OnDeathHandler()
        {
            OnDeath?.Invoke();
        }

        public void Dispose()
        {
            _healthModel.OnDeath -= OnDeathHandler;
        }
    }
}