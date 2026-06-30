using System;

namespace _Project.Core.Physics
{
    public interface IObservableRotation : IHasRotation
    {
        event Action<float> RotationAngleChanged;
    }
}