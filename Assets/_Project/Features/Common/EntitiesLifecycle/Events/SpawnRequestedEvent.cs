using _Project.Core.EventBus;
using _Project.Core.Physics.Movement;

namespace _Project.Features.Common.EntitiesLifecycle.Events
{
    public sealed class SpawnRequestedEvent<T> : ISpawnEvent<InitialMovementData> where T : IFacade
    {
        public readonly InitialMovementData initialMovementData;
        public InitialMovementData SpawnData => initialMovementData;



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