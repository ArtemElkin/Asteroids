namespace _Project.Features.Common.Hit
{
    public struct HitInfo
    {
        public readonly bool fullDestroy;


        public HitInfo(bool fullDestroy)
        {
            this.fullDestroy = fullDestroy;
        }
    }
}