using System;

namespace _Project.Features.Spaceship.Health
{
    public class HealthModel
    {
        public int MaxHp { get; }
        private int _hp;
        public int Hp
        {
            get => _hp;
            set
            {
                _hp = value;
                OnHealthChanged?.Invoke();
                if (_hp <= 0) OnDeath?.Invoke();
            }
        }
        
        public event Action OnHealthChanged;
        public event Action OnDeath;

        public HealthModel(int maxHp)
        {
            MaxHp = maxHp;
            Hp = MaxHp;
        }

        public void DecreaseHp(int amount)
        {
            Hp -= amount;
        }
    }
}