using _Project.Core.EventBus;
using _Project.Core.Physics;
using _Project.Core.Physics.Collision;
using _Project.Core.Services;

namespace _Project.Features.Common.Collision
{
    public class ElasticCollisionService : BaseCollisionService
    {
        public ElasticCollisionService(IEventBus eventBus, ITimeService timeService) :
            base(eventBus, timeService) { }

        protected override void OnProcessCollision(CollisionData collisionData)
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