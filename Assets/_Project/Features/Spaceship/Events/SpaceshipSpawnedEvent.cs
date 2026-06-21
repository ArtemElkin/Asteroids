using _Project.Core.EventBus;

namespace _Project.Features.Spaceship.Events
{
    public class SpaceshipSpawnedEvent : IEvent
    {
        public SpaceshipReadOnlyInfo Info { get; }


        public SpaceshipSpawnedEvent(SpaceshipReadOnlyInfo info)
        {
            Info = info;
        }
    }
}