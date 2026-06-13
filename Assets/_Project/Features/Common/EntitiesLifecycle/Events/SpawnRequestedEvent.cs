using _Project.Core.EventBus;
using _Project.Core.Physics.Movement;

namespace _Project.Features.Common.EntitiesLifecycle.Events
{
    public class SpawnRequestedEvent<T> : IEvent where T : IFacade
    {
        public readonly InitialMovementData initialMovementData;

        
        public SpawnRequestedEvent()
        {
            initialMovementData = new InitialMovementData();
        }

        public SpawnRequestedEvent(InitialMovementData initialMovementData)
        {
            this.initialMovementData = initialMovementData;
        }
    }
}