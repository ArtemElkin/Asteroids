
using _Project.Core.Math;

namespace _Project.Features.Gameplay.Asteroid
{
    public struct AsteroidSpawnData
    {
        public readonly Vector2 initialPosition;
        public readonly float initialSpeed;


        public AsteroidSpawnData(Vector2 initialPosition, float initialSpeed)
        {
            this.initialPosition = initialPosition;
            this.initialSpeed = initialSpeed;
        }
    }
}