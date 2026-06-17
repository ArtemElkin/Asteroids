using _Project.Features.Common.EntitiesLifecycle.Events;

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