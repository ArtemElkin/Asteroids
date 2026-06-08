using _Project.Core.Config;

namespace _Project.Features.Asteroid.Config
{
    public class AsteroidConfig : IConfig
    {
        public int fragmentsCount;
        public float radius;
        public float fragmentRadius;
        public bool hasClones;
        public AsteroidMovementConfig movementConfig;
    }
}