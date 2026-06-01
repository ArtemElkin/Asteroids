using _Project.Core.Config;

namespace _Project.Features.Spaceship
{
    public class SpaceshipMovementConfig : IConfig
    {
        public float maxSpeed;
        public float accelerationMultiplier;
        public float inertiaMultiplier;
    }
}