using _Project.Core.Math;
using _Project.Core.Physics;

namespace _Project.Features.Common
{
    public class BaseMovementController : IMovable, IWarpable
    {
        protected readonly MovementModel _movementModel;


        protected BaseMovementController(MovementModel movementModel)
        {
            _movementModel = movementModel;
        }

        public void Move(float deltaTime)
        {
            UpdateDirectionOnMove();
            UpdateSpeedOnMove(deltaTime);
            UpdateVelocityOnMove();
            UpdatePositionOnMove(deltaTime);
        }

        public void Warp(Vector2 position)
        {
            _movementModel.UpdatePosition(position);
        }

        protected virtual void UpdateDirectionOnMove() { }
        protected virtual void UpdateSpeedOnMove(float deltaTime) { }

        private void UpdateVelocityOnMove()
        {
            var velocity = _movementModel.MoveDirection * _movementModel.Speed;
            _movementModel.UpdateVelocity(velocity);
        }
        
        private void UpdatePositionOnMove(float  deltaTime)
        {
            _movementModel.UpdatePosition(_movementModel.Position + _movementModel.Velocity * deltaTime);
        }
    }
}