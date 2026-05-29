using _Project.Core.Math;
using _Project.Core.Physics;


namespace _Project.Features.Gameplay.Signals
{
    public class OutOfBoundsSignal
    {
        public IWarpable warpable;
        public CustomVector2 position;


        public OutOfBoundsSignal(IWarpable warpable, CustomVector2 position)
        {
            this.warpable = warpable;
            this.position = position;
        }
    }
}