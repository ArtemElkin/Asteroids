using _Project.Core.Infrastructure.Config;

namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipMovementConfig : IConfig
    {
        public float maxSpeed;
        public float accelerationMultiplier;
        public float inertiaMultiplier;
    }
}