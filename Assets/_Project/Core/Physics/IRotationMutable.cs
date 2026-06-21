namespace _Project.Core.Physics
{
    public interface IRotationMutable : IReadOnlyRotation
    {
        void UpdateRotationAngle(float rotationAngle);
    }
}