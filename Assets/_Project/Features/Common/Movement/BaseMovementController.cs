using _Project.Core.Physics.Movement;

namespace _Project.Features.Common.Movement
{
    public abstract class BaseMovementController : IMovable
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

        protected virtual void UpdateDirectionOnMove() { }

        protected virtual void UpdateVelocityOnMove(float deltaTime) { }
        
        private void UpdatePositionOnMove(float  deltaTime)
        {
            _movementModel.UpdatePosition(_movementModel.Position + _movementModel.Velocity * deltaTime);
        }
    }
}