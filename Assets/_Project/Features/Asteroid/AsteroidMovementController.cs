using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Features.Common;

namespace _Project.Features.Asteroid
{
    public class AsteroidMovementController : BaseMovementController, IBouncable
    {
        public AsteroidMovementController(
            MovementModel movementModel) : base(movementModel)
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
        
        public void Bounce(Vector2 normal)
        {
            var velocity = _movementModel.Velocity;
            var reflectedVelocity = Vector2.Reflect(velocity, normal);
            _movementModel.UpdateVelocity(reflectedVelocity);
        }
    }
}