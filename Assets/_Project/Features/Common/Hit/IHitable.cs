using System;

namespace _Project.Features.Common.Hit
{
    public interface IHitable
    {
        event Action<HitInfo> OnHit;
    }
}