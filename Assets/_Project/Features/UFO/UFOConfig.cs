using _Project.Core.Config;

namespace _Project.Features.UFO
{
    public class UFOConfig : IConfig
    {
        public float minSpeed;
        public float maxSpeed;
        public float accelerationMultiplier;
        public float inertiaMultiplier;
    }
}