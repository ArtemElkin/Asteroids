using _Project.Core.Physics;

namespace _Project.Features.Common.Event
{
    public class CollisionDetectedEvent
    {
        public readonly CollisionData collisionData;


        public CollisionDetectedEvent(CollisionData collisionData)
        {
            this.collisionData = collisionData;
        }
        
    }
}