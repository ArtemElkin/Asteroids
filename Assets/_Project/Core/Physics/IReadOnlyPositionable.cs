using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IReadOnlyPositionable
    {
        Vector2 Position { get; }
    }
}