using System;
using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IReadOnlyVelocity
    {
        Vector2 Velocity {  get; }
        public event Action<Vector2> VelocityChanged;
    }
}