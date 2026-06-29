using _Project.Core.Math;
using _Project.Core.Physics.Collision;

namespace _Project.Features.Common.Collision.Resolvers
{
    public class SimpleReflectionCollisionResolver : ICollisionResolver
    {
        public CollisionResolverType ResolverType => CollisionResolverType.SimpleReflection;
        public void ProcessCollision(CollisionData collisionData)
        {
            var velocityA = collisionData.modelA.Velocity;
            var velocityB = collisionData.modelB.Velocity;

            var velocityANew = Vector2.Reflect(velocityA, collisionData.collisionNormal);
            var velocityBNew = Vector2.Reflect(velocityB, collisionData.collisionNormal);
            
            collisionData.modelA.UpdateVelocity(velocityANew);
            collisionData.modelB.UpdateVelocity(velocityBNew);
        }
    }
}