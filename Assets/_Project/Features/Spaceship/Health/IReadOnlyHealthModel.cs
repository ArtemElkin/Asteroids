using System;

namespace _Project.Features.Spaceship.Health
{
    public interface IReadOnlyHealthModel
    {
        public int MaxHp { get; }
        public int Hp { get; }
        public event Action<int> OnHpChanged;
    }
}