using _Project.Core.Infrastructure.Config;


namespace _Project.Features.Gameplay.Asteroid
{
    public class AsteroidConfig : IConfig
    {
        public int fragmentsCount;
        public float minSpeed;
        public float maxSpeed;
    }
}