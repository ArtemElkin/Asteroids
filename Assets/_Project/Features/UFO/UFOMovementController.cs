using _Project.Core.Physics;
using _Project.Core.Physics.Movement;
using _Project.Features.Common;
using _Project.Features.Common.Movement;

namespace _Project.Features.UFO
{
    public class UFOMovementController : BaseMovementController
    {
        private float _speed;
        
        public UFOMovementController(MovementModel movementModel, float initialSpeed) : base(movementModel)
        {
            _speed = initialSpeed;
        }

        protected override void UpdateVelocityOnMove(float deltaTime)
        {
            var newVelocity = _movementModel.MoveDirection * _speed;
            _movementModel.UpdateVelocity(newVelocity);
        }
    }
}