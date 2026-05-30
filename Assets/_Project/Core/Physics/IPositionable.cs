using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IPositionable : IReadOnlyPositionable
    {
        void UpdatePosition(Vector2 newPosition);
    }
}