using _Project.Core.Physics;
using _Project.Core.Physics.Movement;

namespace _Project.Features.UFO
{
    public struct UFOSpawnData
    {
        public InitialMovementData initialMovementData;
        public float speed;
        public float accelerationMultiplier;
        public float inertiaMultiplier;

        public UFOSpawnData(
            InitialMovementData initialMovementData,
            float speed,
            float accelerationMultiplier,
            float inertiaMultiplier)
        {
            this.initialMovementData = initialMovementData;
            this.speed = speed;
            this.accelerationMultiplier = accelerationMultiplier;
            this.inertiaMultiplier = inertiaMultiplier;
        }
    }
}