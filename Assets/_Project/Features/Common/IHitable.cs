using System;

namespace _Project.Features.Common
{
    public interface IHitable
    {
        event Action OnHit;
    }
}