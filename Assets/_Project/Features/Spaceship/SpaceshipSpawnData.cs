using _Project.Core.Physics;

namespace _Project.Features.Spaceship
{
    public struct SpaceshipSpawnData
    {
        public InitialMovementData initialMovementData;
        public SpaceshipConfig config;
        public int initialHp;


        public SpaceshipSpawnData(InitialMovementData initialMovementData, SpaceshipConfig config, int initialHp)
        {
            this.initialMovementData =  initialMovementData;
            this.config = config;
            this.initialHp = initialHp;
        }
    }
}