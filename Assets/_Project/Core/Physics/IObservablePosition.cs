using System;
using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IObservablePosition : IHasPosition
    {
        event Action<Vector2> PositionChanged;
    }
}