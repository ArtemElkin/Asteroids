using _Project.Core.Physics;

namespace _Project.Features.Gameplay.Common
{
    public abstract class BaseRotationController
    {
        private bool _isSetup;
        protected MovementModel _movementModel;


        public void Setup(MovementModel movementModel)
        {
            _movementModel = movementModel;
            _isSetup = true;
        }

        public void Rotate()
        {
            if (!_isSetup) return;
            
            UpdateAngleOnRotate();
        }

        protected abstract void UpdateAngleOnRotate();

        public virtual void Reset()
        {
            _isSetup = false;
            _movementModel = null;
        }
    }
}