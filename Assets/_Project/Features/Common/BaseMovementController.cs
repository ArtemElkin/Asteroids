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
            var initialVelocity =  _movementModel.MoveDirection * _movementModel.Speed;
            _movementModel.UpdateVelocity(initialVelocity);
        }

        public void Move(float deltaTime)
        {
            UpdateDirectionOnMove();
            UpdateSpeedOnMove(deltaTime);
            UpdateVelocityOnMove(deltaTime);
            UpdatePositionOnMove(deltaTime);
        }

        public void Warp(Vector2 position)
        {
            _movementModel.UpdatePosition(position);
        }

        protected virtual void UpdateDirectionOnMove() { }
        protected virtual void UpdateSpeedOnMove(float deltaTime) { }

        protected virtual void UpdateVelocityOnMove(float deltaTime)
        {
            // var previousVelocity = _movementModel.Velocity;
            var newVelocity = _movementModel.Velocity.normalized * _movementModel.Speed;
            // var velocity = Vector2.Lerp(previousVelocity, newVelocity, deltaTime);
            _movementModel.UpdateVelocity(newVelocity);
        }
        
        private void UpdatePositionOnMove(float  deltaTime)
        {
            _movementModel.UpdatePosition(_movementModel.Position + _movementModel.Velocity * deltaTime);
        }
    }
}