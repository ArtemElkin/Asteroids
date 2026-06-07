using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public struct InitialMovementData
    {
        public readonly Vector2 initialPosition;
        public readonly Vector2 initialVelocity;


        public InitialMovementData(Vector2 initialPosition)
        {
            this.initialPosition = initialPosition;
            this.initialVelocity = Vector2.zero;
        }
        public InitialMovementData(Vector2 initialPosition, Vector2 initialVelocity)
        {
            this.initialPosition = initialPosition;
            this.initialVelocity =  initialVelocity;
        }
    }
}