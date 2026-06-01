using _Project.Core.Math;

namespace _Project.Features.Gameplay.UFO
{
    public struct UFOSpawnData
    {
        public Vector2 initialPosition;
        public float initialSpeed;
        public float accelerationMultiplier;
        public float inertiaMultiplier;

        public UFOSpawnData(
            Vector2 initialPosition, 
            float initialSpeed,
            float accelerationMultiplier,
            float inertiaMultiplier)
        {
            this.initialPosition = initialPosition;
            this.initialSpeed = initialSpeed;
            this.accelerationMultiplier = accelerationMultiplier;
            this.inertiaMultiplier = inertiaMultiplier;
        }
    }
}