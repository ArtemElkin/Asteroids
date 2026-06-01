using _Project.Core.Math;

namespace _Project.Features.Spaceship
{
    public struct SpaceshipSpawnData
    {
        public Vector2 initialPosition;
        public SpaceshipMovementConfig movementConfig;
        public int initialHp;


        public SpaceshipSpawnData(Vector2 initialPosition, SpaceshipMovementConfig movementConfig, int initialHp)
        {
            this.initialPosition = initialPosition;
            this.movementConfig = movementConfig;
            this.initialHp = initialHp;
        }
    }
}