using _Project.Core.EventBus;
using _Project.Core.Math;
using _Project.Core.Physics.Collision;
using _Project.Core.Services;

namespace _Project.Features.Common.Collision
{
    public class SimpleReflectionCollisionService : BaseCollisionService
    {
        public SimpleReflectionCollisionService(IEventBus eventBus, ITimeService timeService) 
            : base(eventBus, timeService) { }
        
        protected override void OnProcessCollision(CollisionData collisionData)
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