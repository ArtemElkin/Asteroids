using System;
using _Project.Features.Spaceship.Weapon;

namespace _Project.Features.Common
{
    public interface IHitable
    {
        event Action<HitInfo> OnHit;
    }
}