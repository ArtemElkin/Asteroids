using _Project.Core.Physics.Collision;

namespace _Project.Features.Common.Collision
{
    public interface ICollisionResolver
    {
        CollisionResolverType ResolverType { get; }
        void ProcessCollision(CollisionData collisionData);
    }
}