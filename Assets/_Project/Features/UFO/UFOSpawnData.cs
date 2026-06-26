using _Project.Core.Physics.Movement;

namespace _Project.Features.UFO
{
    public struct UFOSpawnData
    {
        public readonly InitialMovementData initialMovementData;
        public readonly float speed;

        public UFOSpawnData(
            InitialMovementData initialMovementData,
            float speed)
        {
            this.initialMovementData = initialMovementData;
            this.speed = speed;
        }
    }
}