using _Project.Core.Math;
using _Project.Core.Physics;

namespace _Project.Features.Gameplay.Common
{
    public abstract class BaseMovementController : IWarpable
    {
        private bool _isSetup;
        protected MovementModel _movementModel;
        

        public void Setup(MovementModel movementModel)
        {
            _movementModel = movementModel;
            OnSetup();
            _isSetup = true;
        }
        
        protected virtual void OnSetup() { }

        public void Move(float deltaTime)
        {
            if (!_isSetup) return;
            
            UpdateDirectionOnMove();
            UpdateVelocityOnMove(deltaTime);
            UpdatePositionOnMove(deltaTime);
        }

        public void Warp(Vector2 position)
        {
            _movementModel.UpdatePosition(position);
        }

        protected virtual void UpdateDirectionOnMove() { }

        protected abstract void UpdateVelocityOnMove(float  deltaTime);
        
        private void UpdatePositionOnMove(float  deltaTime)
        {
            _movementModel.UpdatePosition(_movementModel.Position + _movementModel.Velocity * deltaTime);
        }
        
        public virtual void Reset()
        {
            _isSetup = false;
            _movementModel = null;
        }
    }
}