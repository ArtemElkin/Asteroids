using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public class MovementModel : IPositionable, IRotatable, IMovable
    {
        public CustomVector2 Position { get; private set; }
        public float RotationAngle { get; private set; }
        public float Speed { get; private set; }
        public CustomVector2 Velocity {  get; private set; }
        public CustomVector2 MoveDirection { get; private set; }


        public virtual void Init(CustomVector2 initialPosition, float initialSpeed)
        {
            UpdatePosition(initialPosition);
            UpdateSpeed(initialSpeed);
        }

        public void UpdatePosition(CustomVector2 newPosition) => Position = newPosition;
        
        public void UpdateRotationAngle(float rotationAngle) => RotationAngle = rotationAngle;

        public void UpdateVelocity(CustomVector2 newVelocity) => Velocity = newVelocity;
        
        public void UpdateSpeed(float speed) => Speed = speed;
        
        public void UpdateMoveDirection(CustomVector2 newDirection) => MoveDirection = newDirection;
    }
}