namespace _Project.Features.Gameplay.Signals
{
    public class OutOfBoundsSignal
    {
        public IWarpable warpable;


        public OutOfBoundsSignal(IWarpable warpable)
        {
            this.warpable = warpable;
        }
    }
}