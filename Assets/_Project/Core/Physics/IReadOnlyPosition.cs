using System;
using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface IReadOnlyPosition
    {
        Vector2 Position { get; }
        public event Action<Vector2> PositionChanged;
    }
}