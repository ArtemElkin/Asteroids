using _Project.Core.Physics;
using _Project.Features.Gameplay.Common;

namespace _Project.Features.Gameplay.Asteroid
{
    public class AsteroidMovementController : BaseMovementController
    {
        public AsteroidMovementController(MovementModel movementModel) : base(movementModel)
        {
            SetInitialVelocity();
        }

        protected override void UpdateVelocityOnMove(float deltaTime) { }
        
        private void SetInitialVelocity()
        {
            var velocity = _movementModel.Speed * _movementModel.MoveDirection;
            if (velocity.magnitude > _movementModel.Speed)
            {
                velocity = velocity.normalized * _movementModel.Speed;
            }
            _movementModel.UpdateVelocity(velocity);
        }
    }
}