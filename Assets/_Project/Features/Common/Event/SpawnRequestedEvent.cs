using _Project.Core.Physics;

namespace _Project.Features.Common.Event
{
    public class SpawnRequestedEvent<T> where T : IFacade
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