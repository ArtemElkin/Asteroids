using _Project.Core.Physics;

namespace _Project.Features.Asteroid
{
    public struct AsteroidSpawnData
    {
        public readonly InitialMovementData initialMovementData;
        public readonly float radius;
        public readonly int fragmentsCount;


        public AsteroidSpawnData(InitialMovementData initialMovementData, float radius, int fragmentsCount = 0)
        {
            this.initialMovementData = initialMovementData;
            this.radius = radius;
            this.fragmentsCount = fragmentsCount;
        }
    }
}