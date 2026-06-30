using System;
using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IObservableVelocity : IHasVelocity
    {
        event Action<Vector2> VelocityChanged;
    }
}