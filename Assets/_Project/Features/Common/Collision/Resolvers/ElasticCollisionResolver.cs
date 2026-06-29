using _Project.Core.Physics;
using _Project.Core.Physics.Collision;

namespace _Project.Features.Common.Collision.Resolvers
{
    public class ElasticCollisionResolver : ICollisionResolver
    {
        public CollisionResolverType ResolverType => CollisionResolverType.Elastic;
        public void ProcessCollision(CollisionData collisionData)
        {
            var massA = collisionData.modelA.Mass;
            var massB = collisionData.modelB.Mass;
            var collisionNormal = collisionData.collisionNormal;
            var velocityA = collisionData.modelA.Velocity;
            var velocityB = collisionData.modelB.Velocity;

            var velocityANew = Physics.CalculateCollisionVelocity(velocityA, velocityB, massA, massB, collisionNormal);
            var velocityBNew = Physics.CalculateCollisionVelocity(velocityB, velocityA, massB, massA, -collisionNormal);
            
            collisionData.modelA.UpdateVelocity(velocityANew);
            collisionData.modelB.UpdateVelocity(velocityBNew);
        }
    }
}