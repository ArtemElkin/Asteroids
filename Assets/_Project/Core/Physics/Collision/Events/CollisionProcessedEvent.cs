using _Project.Features.Common.EntitiesLifecycle.Events;

namespace _Project.Core.Physics.Collision.Events
{
    public sealed class CollisionProcessedEvent : ISpawnEvent<CollisionData>
    {
        public readonly CollisionData collisionData;
        public CollisionData SpawnData => collisionData;



        public CollisionProcessedEvent(CollisionData collisionData)
        {
            this.collisionData = collisionData;
        }
    }
}