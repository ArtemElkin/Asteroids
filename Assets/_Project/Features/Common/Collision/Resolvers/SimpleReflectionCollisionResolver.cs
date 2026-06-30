using _Project.Core.Math;
using _Project.Core.Physics.Collision;

namespace _Project.Features.Common.Collision.Resolvers
{
    public class SimpleReflectionCollisionResolver : ICollisionResolver
    {
        public CollisionResolverType ResolverType => CollisionResolverType.SimpleReflection;
        public void ProcessCollision(CollisionData collisionData)
        {
            var n = collisionData.collisionNormal.normalized;
            if (n.sqrMagnitude < 0.0001f) return;
            var velocityA = collisionData.modelA.Velocity;
            var velocityB = collisionData.modelB.Velocity;
            var relNormal = Vector2.Dot(velocityA - velocityB, n);
            if (relNormal >= 0f) return;
            var velocityANew = velocityA - relNormal * n;
            var velocityBNew = velocityB + relNormal * n;
            collisionData.modelA.UpdateVelocity(velocityANew);
            collisionData.modelB.UpdateVelocity(velocityBNew);
        }
    }
}