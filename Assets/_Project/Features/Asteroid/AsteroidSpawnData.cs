using _Project.Core.Physics;
using _Project.Features.Asteroid.Config;

namespace _Project.Features.Asteroid
{
    public struct AsteroidSpawnData
    {
        public readonly InitialMovementData initialMovementData;
        public readonly AsteroidConfig config;


        public AsteroidSpawnData(InitialMovementData initialMovementData, AsteroidConfig config)
        {
            this.initialMovementData = initialMovementData;
            this.config = config;
        }
    }
}