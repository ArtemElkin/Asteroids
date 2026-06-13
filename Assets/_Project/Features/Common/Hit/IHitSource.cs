using System;

namespace _Project.Features.Common.Hit
{
    public interface IHitSource
    {
        event Action OnHit;
    }
}