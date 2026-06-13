using _Project.Core.Physics;
using _Project.Core.Physics.Movement;

namespace _Project.Features.Common.Movement
{
    public abstract class BaseRotationController : IRotatable
    {
        protected MovementModel _movementModel;


        protected BaseRotationController(
            MovementModel movementModel)
        {
            _movementModel = movementModel;
        }

        public void Rotate()
        {
            UpdateAngleOnRotate();
        }

        protected abstract void UpdateAngleOnRotate();
    }
}