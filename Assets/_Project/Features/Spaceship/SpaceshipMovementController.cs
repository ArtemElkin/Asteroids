using _Project.Core.Input;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Features.Gameplay.Common;

namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipMovementController : BaseMovementController
    {
        private readonly SpaceshipMovementConfig _movementConfig;
        private readonly IMovementInputService _movementInputService;


        public SpaceshipMovementController(
            MovementModel movementModel,
            IMovementInputService movementInputService,
            SpaceshipMovementConfig movementConfig
            ) : base (movementModel)
        {
            _movementInputService = movementInputService;
            _movementConfig = movementConfig;
        }
        
        protected override void UpdateDirectionOnMove()
        {
            var moveDirection = _movementInputService.GetAxis();
            if (moveDirection.sqrMagnitude > 1) 
            {
                moveDirection = moveDirection.normalized;
            }
            _movementModel.UpdateMoveDirection(moveDirection);
        }

        protected override void UpdateVelocityOnMove(float deltaTime)
        {
            var velocity = _movementModel.Velocity;
            velocity = _movementModel.MoveDirection == Vector2.zero ? 
                (Physics.ApplyInertia(velocity, _movementConfig.inertiaMultiplier, deltaTime)) :
                Physics.ApplyAcceleration(velocity, _movementConfig.accelerationMultiplier, _movementModel.MoveDirection, deltaTime);

            if (velocity.magnitude > _movementConfig.maxSpeed)
            {
                velocity = velocity.normalized * _movementConfig.maxSpeed;
            }
            _movementModel.UpdateVelocity(velocity);
        }
    }
}