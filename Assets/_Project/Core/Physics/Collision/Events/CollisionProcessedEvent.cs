using _Project.Core.EventBus;

namespace _Project.Core.Physics.Collision.Events
{
    public sealed class CollisionProcessedEvent : ISpawnEvent<CollisionData>
    {
        public CollisionData SpawnData { get; }
        

        public CollisionProcessedEvent(CollisionData collisionData)
        {
            SpawnData = collisionData;
        }
    }
}