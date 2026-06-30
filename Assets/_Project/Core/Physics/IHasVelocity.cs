using System;
using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IHasVelocity
    {
        Vector2 Velocity {  get; }
    }
}