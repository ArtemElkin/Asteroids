using System;

namespace _Project.Core.Physics
{
    public interface IReadOnlyRotation
    {
        public float RotationAngle { get; }
        public event Action<float> RotationAngleChanged;
    }
}