using System;

namespace _Project.Features.Spaceship.Health
{
    public interface IReadOnlyHealthModel
    {
        public int MaxHealth { get; }
        public int Health { get; }
        public event Action<int> OnHealthChanged;
    }
}