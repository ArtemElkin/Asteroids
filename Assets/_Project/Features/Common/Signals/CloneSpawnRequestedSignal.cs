using _Project.Core.Physics;

namespace _Project.Features.Common.Signals
{
    public class CloneSpawnRequestedSignal<TOriginFacade> where TOriginFacade : IFacade
    {
        public TOriginFacade originFacade;


        public CloneSpawnRequestedSignal(TOriginFacade originFacade)
        {
            this.originFacade = originFacade;
        }
    }
}