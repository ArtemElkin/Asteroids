using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IMutableVelocity : IObservableVelocity
    {
        void UpdateVelocity(Vector2 newVelocity);
    }
}