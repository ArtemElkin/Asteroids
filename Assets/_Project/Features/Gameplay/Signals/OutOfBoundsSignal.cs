using _Project.Core.Math;
using _Project.Core.Physics;

namespace _Project.Features.Gameplay.Signals
{
    public class OutOfBoundsSignal
    {
        public IWarpable warpable;
        public Vector2 position;


        public OutOfBoundsSignal(IWarpable warpable, Vector2 position)
        {
            this.warpable = warpable;
            this.position = position;
        }
    }
}