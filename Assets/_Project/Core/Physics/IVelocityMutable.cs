using _Project.Core.Math;

namespace _Project.Core.Physics.Movement
{
    public interface IVelocityMutable : IReadOnlyVelocity
    {
        void UpdateVelocity(Vector2 newVelocity);
    }
}