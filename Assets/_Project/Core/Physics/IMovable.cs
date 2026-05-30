using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IMovable
    {
        public float Speed { get; }
        public Vector2 Velocity {  get; }
        public Vector2 MoveDirection { get; }
        public void UpdateSpeed(float newSpeed);
        public void UpdateVelocity(Vector2 newVelocity);
        public void UpdateMoveDirection(Vector2 newDirection);
    }
}