using System;

namespace _Project.Features.Common.Bounds
{
    [Flags]
    public enum BoundType
    {
        None = 0,
        Top = 1 << 0,
        Bottom = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3,
        All = Top | Bottom | Left | Right
    }
}