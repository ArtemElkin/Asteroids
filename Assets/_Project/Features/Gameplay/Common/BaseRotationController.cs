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

        public void UpdatePhysics(float deltaTime)
        {
            if (!_isSetup) return;
            
            Rotate();
        }

        protected abstract void Rotate();

        public virtual void Reset()
        {
            _isSetup = false;
            _movementModel = null;
        }
    }
}