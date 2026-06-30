using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IMutablePosition : IObservablePosition
    {
        void UpdatePosition(Vector2 newPosition);
    }
}