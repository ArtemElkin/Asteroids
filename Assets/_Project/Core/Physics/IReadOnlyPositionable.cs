using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IReadOnlyPositionable
    {
        CustomVector2 Position { get; }
        
    }
}