using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IPositionMutable : IReadOnlyPosition
    {
        void UpdatePosition(Vector2 newPosition);
    }
}