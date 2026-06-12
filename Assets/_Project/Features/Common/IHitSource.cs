using System;

namespace _Project.Features.Common
{
    public interface IHitSource
    {
        event Action OnHit;
    }
}