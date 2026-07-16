using _Project.Core.Math;
using _Project.Core.Physics.Movement;

namespace _Project.Features.Common.ScreenWrapClone
{
    public interface IScreenWrapCloneOffsetCalculator
    {
        Vector2[] CalculateOffsets(MovementModel originMovementModel);
    }
}