using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IVelocityMutable : IReadOnlyVelocity
    {
        void UpdateVelocity(Vector2 newVelocity);
    }
}