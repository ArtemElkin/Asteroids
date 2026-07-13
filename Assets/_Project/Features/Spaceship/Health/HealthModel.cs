using System;

namespace _Project.Features.Spaceship.Health
{
    public class HealthModel : IReadOnlyHealthModel
    {
        public int MaxHp { get; }
        private int _hp;
        public int Hp
        {
            get => _hp;
            private set
            {
                _hp = value;
                OnHpChanged?.Invoke(_hp);
                if (_hp <= 0) OnDeath?.Invoke();
            }
        }
        
        public event Action<int> OnHpChanged;
        public event Action OnDeath;

        public HealthModel(int maxHp)
        {
            MaxHp = maxHp;
            Hp = MaxHp;
        }

        public void DecreaseHp()
        {
            Hp--;
        }
    }
}