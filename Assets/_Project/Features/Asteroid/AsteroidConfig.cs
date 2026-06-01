using _Project.Core.Config;

namespace _Project.Features.Asteroid
{
    public class AsteroidConfig : IConfig
    {
        public int fragmentsCount;
        public float minSpeed;
        public float maxSpeed;
    }
}