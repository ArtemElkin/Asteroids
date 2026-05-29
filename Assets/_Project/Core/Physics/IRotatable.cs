namespace _Project.Core.Physics
{
    public interface IRotatable : IReadOnlyRotatable
    {
        void UpdateRotationAngle(float rotationAngle);
    }
}