namespace _Project.Features.Spaceship.Weapon
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