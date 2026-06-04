using _Project.Core.Physics;

namespace _Project.Features.Common.Signals
{
    public class SpawnRequestedSignal<T> where T : IFacade
    {
        public readonly InitialMovementData initialMovementData;

        public SpawnRequestedSignal()
        {
            initialMovementData = new InitialMovementData();
        }

        public SpawnRequestedSignal(InitialMovementData initialMovementData)
        {
            this.initialMovementData = initialMovementData;
        }
    }
}