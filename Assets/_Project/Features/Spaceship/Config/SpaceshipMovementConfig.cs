using _Project.Core.Config;

namespace _Project.Features.Spaceship.Config
{
    public class SpaceshipMovementConfig : IConfig
    {
        public float mass;
        public float maxSpeed;
        public float thrust;
        public float friction;
    }
}