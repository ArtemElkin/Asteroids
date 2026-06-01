using _Project.Core.Math;

namespace _Project.Features.Spaceship
{
    public struct SpaceshipSpawnData
    {
        public Vector2 initialPosition;
        public SpaceshipMovementConfig movementConfig;


        public SpaceshipSpawnData(Vector2 initialPosition, SpaceshipMovementConfig movementConfig)
        {
            this.initialPosition = initialPosition;
            this.movementConfig = movementConfig;
        }
    }
}