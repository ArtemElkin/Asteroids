using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public class MovementModel : IPositionable, IRotationable, IStunable
    {
        public Vector2 Position { get; private set; }
        public float RotationAngle { get; private set; }
        public float Speed { get; private set; }
        public Vector2 Velocity {  get; private set; }
        public Vector2 MoveDirection { get; private set; }
        public bool IsStunned { get; private set; }


        public MovementModel(InitialMovementData data)
        {
            UpdatePosition(data.initialPosition);
            UpdateSpeed(data.initialSpeed);
            UpdateMoveDirection(data.initialMoveDirection);
        }

        public void UpdatePosition(Vector2 newPosition) => Position = newPosition;
        
        public void UpdateRotationAngle(float rotationAngle) => RotationAngle = rotationAngle;

        public void UpdateVelocity(Vector2 newVelocity) => Velocity = newVelocity;
        
        public void UpdateSpeed(float speed) => Speed = speed;
        
        public void UpdateMoveDirection(Vector2 newDirection) => MoveDirection = newDirection;
        
        public void SetStunned(bool isStunned) => IsStunned = isStunned;
    }
}