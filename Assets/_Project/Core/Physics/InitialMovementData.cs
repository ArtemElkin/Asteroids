using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public struct InitialMovementData
    {
        public readonly float mass;
        public readonly Vector2 initialPosition;
        public readonly Vector2 initialVelocity;


        public InitialMovementData(float mass, Vector2 initialPosition)
        {
            this.mass = mass;
            this.initialPosition = initialPosition;
            this.initialVelocity = Vector2.zero;
        }
        public InitialMovementData(float mass, Vector2 initialPosition, Vector2 initialVelocity)
        {
            this.mass = mass;
            this.initialPosition = initialPosition;
            this.initialVelocity =  initialVelocity;
        }
    }
}