using _Project.Core.Math;
using _Project.Core.Physics;

namespace _Project.Features.Spaceship
{
    public struct SpaceshipSpawnData
    {
        public InitialMovementData initialMovementData;
        public SpaceshipMovementConfig movementConfig;
        public int initialHp;


        public SpaceshipSpawnData(InitialMovementData initialMovementData, SpaceshipMovementConfig movementConfig, int initialHp)
        {
            this.initialMovementData =  initialMovementData;
            this.movementConfig = movementConfig;
            this.initialHp = initialHp;
        }
    }
}