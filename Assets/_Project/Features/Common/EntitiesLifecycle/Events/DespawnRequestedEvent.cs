using _Project.Core.EventBus;

namespace _Project.Features.Common.EntitiesLifecycle.Events
{
    public sealed class DespawnRequestedEvent<T> : IEvent where T : IFacade
    {
        public T facade;


        public DespawnRequestedEvent(T facade)
        {
            this.facade = facade;
        }
    }
}