using _Project.Core.Physics.Movement;
using _Project.Features.Asteroid.Config;

namespace _Project.Features.Asteroid
{
    public struct AsteroidSpawnData
    {
        public readonly InitialMovementData initialMovementData;
        public readonly float radius;
        public readonly int fragmentsCount;
        public readonly bool hasClones;
        public readonly AsteroidConfig config;


        public AsteroidSpawnData(
            InitialMovementData initialMovementData,
            float radius, int fragmentsCount, 
            bool hasClones,
            AsteroidConfig config)
        {
            this.initialMovementData = initialMovementData;
            this.radius = radius;
            this.fragmentsCount = fragmentsCount;
            this.hasClones = hasClones;
            this.config = config;
        }
    }
}