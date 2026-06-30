using _Project.Core.Physics.Movement;

namespace _Project.Core.Physics
{
    public interface IMutableRotation : IObservableRotation
    {
        void UpdateRotationAngle(float rotationAngle);
    }
}