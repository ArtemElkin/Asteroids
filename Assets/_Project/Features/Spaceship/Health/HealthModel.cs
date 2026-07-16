using System;

namespace _Project.Features.Spaceship.Health
{
    public class HealthModel : IReadOnlyHealthModel
    {
        public int MaxHealth { get; }
        private int _health;
        public int Health
        {
            get => _health;
            private set
            {
                _health = value;
                OnHealthChanged?.Invoke(_health);
                if (_health <= 0) OnDeath?.Invoke();
            }
        }
        
        public event Action<int> OnHealthChanged;
        public event Action OnDeath;

        public HealthModel(int maxHealth)
        {
            MaxHealth = maxHealth;
            Health = MaxHealth;
        }

        public void DecreaseHealth()
        {
            Health--;
        }
    }
}