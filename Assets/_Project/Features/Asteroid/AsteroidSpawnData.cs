using _Project.Core.Physics;

namespace _Project.Features.Asteroid
{
    public struct AsteroidSpawnData
    {
        public readonly InitialMovementData initialMovementData;


        public AsteroidSpawnData(InitialMovementData initialMovementData)
        {
            this.initialMovementData = initialMovementData;
        }
    }
}