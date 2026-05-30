using _Project.Features.Gameplay.Common;

namespace _Project.Features.Gameplay.Asteroid
{
    public class AsteroidMovementController : BaseMovementController
    {
        protected override void OnSetup()
        {
            SetInitialVelocity();
        }

        protected override void UpdateVelocity(float deltaTime) { }
        
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