using _Project.Core.Math;


namespace _Project.Core.Physics
{
    public interface IMovable
    {
        public float Speed { get; }
        public CustomVector2 Velocity {  get; }
        public CustomVector2 MoveDirection { get; }
        public void UpdateSpeed(float newSpeed);
        public void UpdateVelocity(CustomVector2 newVelocity);
        public void UpdateMoveDirection(CustomVector2 newDirection);
    }
}