namespace _Project.Core.Physics
{
    public interface IMutableRotation : IObservableRotation
    {
        void UpdateRotationAngle(float rotationAngle);
    }
}