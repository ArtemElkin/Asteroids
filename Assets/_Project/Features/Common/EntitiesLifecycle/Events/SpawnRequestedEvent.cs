using _Project.Core.Physics.Movement;

namespace _Project.Features.Common.EntitiesLifecycle.Events
{
    public sealed class SpawnRequestedEvent<T> : ISpawnEvent<InitialMovementData> where T : IFacade
    {
        public InitialMovementData SpawnData { get; }
        

        public SpawnRequestedEvent(InitialMovementData initialMovementData)
        {
            SpawnData = initialMovementData;
        }
    }
}