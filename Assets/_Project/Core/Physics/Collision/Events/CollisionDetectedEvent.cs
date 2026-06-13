using _Project.Core.EventBus;

namespace _Project.Core.Physics.Collision.Events
{
    public class CollisionDetectedEvent : IEvent
    {
        public readonly CollisionData collisionData;


        public CollisionDetectedEvent(CollisionData collisionData)
        {
            this.collisionData = collisionData;
        }
        
    }
}