using _Project.Core.Input;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Features.Common;

namespace _Project.Features.Spaceship
{
    public class SpaceshipMovementController : BaseMovementController
    {
        private readonly SpaceshipMovementConfig _movementConfig;
        private readonly IMovementInputService _movementInputService;


        public SpaceshipMovementController(
            MovementModel movementModel,
            IMovementInputService movementInputService,
            SpaceshipMovementConfig movementConfig) : base (movementModel)
        {
            _movementInputService = movementInputService;
            _movementConfig = movementConfig;
        }
        
        protected override void UpdateDirectionOnMove()
        {
            if (_movementModel.IsStunned) return;
            
            var input = _movementInputService.GetAxis();
            if (input.sqrMagnitude > 0.001f)
            {
                _movementModel.UpdateMoveDirection(input.normalized);
            }
        }

        protected override void UpdateVelocityOnMove(float deltaTime)
        {
            var input = _movementInputService.GetAxis();
            if (input.sqrMagnitude > 0.001f && !_movementModel.IsStunned)
            {
                var currentVelocity = _movementModel.Velocity;
                var targetVelocity = Physics.ApplyAcceleration(currentVelocity, _movementModel.MoveDirection, _movementConfig.accelerationMultiplier, deltaTime);
                if (targetVelocity.magnitude > _movementConfig.maxSpeed)
                {
                    targetVelocity = targetVelocity.normalized * _movementConfig.maxSpeed;
                }
                var newVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, 15 * deltaTime);
                _movementModel.UpdateVelocity(newVelocity);
            }
            else
            {
                var velocity = Physics.ApplyInertia(_movementModel.Velocity, _movementConfig.inertiaMultiplier, deltaTime);
                _movementModel.UpdateVelocity(velocity);
            }
        }
    }
}