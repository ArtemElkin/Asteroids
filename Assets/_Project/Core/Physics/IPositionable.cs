using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IPositionable : IReadOnlyPositionable
    {
        void UpdatePosition(CustomVector2 newPosition);
    }
}