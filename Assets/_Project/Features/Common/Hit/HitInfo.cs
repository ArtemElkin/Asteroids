using _Project.Core.Math;
using _Project.Core.Physics;

namespace _Project.Features.Common.Hit
{
    public readonly struct HitInfo : IHasPosition
    {
        public readonly bool fullDestroy;
        public readonly Vector2 hitPosition;
        public Vector2 Position => hitPosition;


        public HitInfo(bool fullDestroy, Vector2 hitPosition)
        {
            this.fullDestroy = fullDestroy;
            this.hitPosition = hitPosition;
        }
    }
}