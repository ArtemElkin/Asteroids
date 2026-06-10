namespace _Project.Features.Common.Event
{
    public class DespawnRequestedEvent<T> where T : IFacade
    {
        public IFacade facade;


        public DespawnRequestedEvent(IFacade facade)
        {
            this.facade = facade;
        }
    }
}