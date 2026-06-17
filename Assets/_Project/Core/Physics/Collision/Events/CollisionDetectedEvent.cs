using _Project.Core.EventBus;

namespace _Project.Core.Physics.Collision.Events
{
    public sealed class CollisionDetectedEvent : IEvent
    {
        public readonly CollisionData collisionData;


        public CollisionDetectedEvent(CollisionData collisionData)
        {
            this.collisionData = collisionData;
        }
    }
}