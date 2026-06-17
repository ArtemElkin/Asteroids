using _Project.Core.Math;

namespace _Project.Features.Common.Hit
{
    public struct HitInfo
    {
        public readonly bool fullDestroy;
        public readonly Vector2 hitPosition;


        public HitInfo(bool fullDestroy, Vector2 hitPosition)
        {
            this.fullDestroy = fullDestroy;
            this.hitPosition = hitPosition;
        }
    }
}