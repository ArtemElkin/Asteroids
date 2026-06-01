using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public struct InitialMovementData
    {
        public readonly Vector2 initialPosition;
        public readonly float initialSpeed;


        public InitialMovementData(Vector2 initialPosition, float initialSpeed)
        {
            this.initialPosition = initialPosition;
            this.initialSpeed = initialSpeed;
        }
    }
}