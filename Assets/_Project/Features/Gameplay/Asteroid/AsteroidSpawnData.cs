
using _Project.Core.Math;

namespace _Project.Features.Gameplay.Asteroid
{
    public struct AsteroidSpawnData
    {
        public readonly Vector2 initialPosition;
        public readonly float initialSpeed;
        public readonly Vector2 initialDirection;


        public AsteroidSpawnData(Vector2 initialPosition, float initialSpeed, Vector2 initialDirection)
        {
            this.initialPosition = initialPosition;
            this.initialSpeed = initialSpeed;
            this.initialDirection = initialDirection;
        }
    }
}