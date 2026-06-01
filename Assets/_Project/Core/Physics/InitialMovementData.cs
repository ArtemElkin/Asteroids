using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public struct InitialMovementData
    {
        public readonly Vector2 initialPosition;
        public readonly float initialSpeed;
        public readonly Vector2 initialMoveDirection;

        public InitialMovementData(Vector2 initialPosition, float initialSpeed)
        {
            this.initialPosition = initialPosition;
            this.initialSpeed = initialSpeed;
            initialMoveDirection = Vector2.zero;
        }
        
        public InitialMovementData(Vector2 initialPosition, float initialSpeed, Vector2 initialMoveDirection) : this(initialPosition, initialSpeed)
        {
            this.initialMoveDirection = initialMoveDirection;
        }
    }
}