using _Project.Core.Math;
using _Project.Core.Physics;

namespace _Project.Features.Common
{
    public abstract class BaseMovementController : IMovable, IWarpable
    {
        protected readonly MovementModel _movementModel;


        protected BaseMovementController(MovementModel movementModel)
        {
            _movementModel = movementModel;
        }

        public void Move(float deltaTime)
        {
            UpdateDirectionOnMove();
            UpdateVelocityOnMove(deltaTime);
            UpdatePositionOnMove(deltaTime);
        }

        public void Warp(Vector2 position)
        {
            _movementModel.UpdatePosition(position);
        }

        protected virtual void UpdateDirectionOnMove() { }

        protected virtual void UpdateVelocityOnMove(float deltaTime) { }
        
        private void UpdatePositionOnMove(float  deltaTime)
        {
            _movementModel.UpdatePosition(_movementModel.Position + _movementModel.Velocity * deltaTime);
        }
    }
}