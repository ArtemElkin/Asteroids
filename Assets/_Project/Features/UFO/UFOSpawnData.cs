using _Project.Core.Physics.Movement;

namespace _Project.Features.UFO
{
    public struct UFOSpawnData
    {
        public InitialMovementData initialMovementData;
        public float speed;

        public UFOSpawnData(
            InitialMovementData initialMovementData,
            float speed)
        {
            this.initialMovementData = initialMovementData;
            this.speed = speed;
        }
    }
}