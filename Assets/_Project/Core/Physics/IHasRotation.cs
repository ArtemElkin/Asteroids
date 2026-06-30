using System;

namespace _Project.Core.Physics
{
    public interface IHasRotation
    {
        float RotationAngle { get; }
        event Action<float> RotationAngleChanged;
    }
}