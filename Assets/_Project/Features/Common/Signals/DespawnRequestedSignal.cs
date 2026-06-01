namespace _Project.Features.Common.Signals
{
    public class DespawnRequestedSignal<T> where T : IFacade
    {
        public IFacade facade;


        public DespawnRequestedSignal(IFacade facade)
        {
            this.facade = facade;
        }
    }
}