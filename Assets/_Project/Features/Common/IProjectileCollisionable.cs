using System;

namespace _Project.Features.Common
{
    public interface IProjectileCollisionable
    {
        event Action OnProjectileCollisioned;
    }
}