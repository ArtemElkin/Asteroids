using _Project.Core.Config;

namespace _Project.Features.Asteroid
{
    public class AsteroidConfig : IConfig
    {
        public float mass;
        public int fragmentsCount;
        public float minSpeed;
        public float maxSpeed;
        public float minFragmentSpeed;
        public float maxFragmentSpeed;
        public float radius;
        public float fragmentRadius;
        public float maxfragmentMoveDirectionAgleOffsetFromAsteroid;
    }
}