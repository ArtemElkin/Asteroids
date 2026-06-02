namespace _Project.Core.Physics
{
    public interface IRotationable : IReadOnlyRotationable
    {
        void UpdateRotationAngle(float rotationAngle);
    }
}