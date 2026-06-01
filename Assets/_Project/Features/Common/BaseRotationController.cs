using _Project.Core.Physics;

namespace _Project.Features.Common
{
    public abstract class BaseRotationController
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