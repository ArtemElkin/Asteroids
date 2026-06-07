namespace _Project.Features.Common.Signals
{
    public class CloneDespawnRequestedSignal<TOriginFacade> where TOriginFacade : IFacade
    {
        public TOriginFacade originFacade;


        public CloneDespawnRequestedSignal(TOriginFacade originFacade)
        {
            this.originFacade = originFacade;
        }
    }
}