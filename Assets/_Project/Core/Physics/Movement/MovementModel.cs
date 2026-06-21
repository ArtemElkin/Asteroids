using System;
using _Project.Core.Math;

namespace _Project.Core.Physics.Movement
{
    public class MovementModel : IPositionMutable, IRotationMutable, IVelocityMutable, IStunnable
    {
        public float Mass { get; private set; }
        public Vector2 Position { get; private set; }
        public float RotationAngle { get; private set; }
        public Vector2 Velocity {  get; private set; }
        public Vector2 MoveDirection { get; private set; }
        public bool IsStunned { get; private set; }
        
        public event Action<Vector2> PositionChanged;
        public event Action<float> RotationAngleChanged;
        public event Action<Vector2> VelocityChanged;


        public MovementModel(InitialMovementData data)
        {
            Mass = data.mass;
            Position = data.initialPosition;
            Velocity = data.initialVelocity;
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            Position = newPosition;
            PositionChanged?.Invoke(newPosition);
        }

        public void UpdateRotationAngle(float rotationAngle)
        {
            RotationAngle = rotationAngle;
            RotationAngleChanged?.Invoke(rotationAngle);
        }

        public void UpdateVelocity(Vector2 newVelocity)
        {
            Velocity = newVelocity;
            VelocityChanged?.Invoke(newVelocity);
        }
        
        public void UpdateMoveDirection(Vector2 newDirection) => MoveDirection = newDirection;
        
        public void SetStunned(bool isStunned) => IsStunned = isStunned;
    }
}