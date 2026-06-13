using _Project.Core.EventBus;

namespace _Project.Features.Common.EntitiesLifecycle.Events
{
    public class DespawnRequestedEvent<T> : IEvent where T : IFacade
    {
        public IFacade facade;


        public DespawnRequestedEvent(IFacade facade)
        {
            this.facade = facade;
        }
    }
}