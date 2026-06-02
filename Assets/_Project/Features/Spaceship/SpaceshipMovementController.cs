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
            var moveDirection = _movementInputService.GetAxis().normalized;
            _movementModel.UpdateMoveDirection(moveDirection);
        }

        protected override void UpdateSpeedOnMove(float deltaTime)
        {
            var speed = _movementModel.Speed;
            speed = _movementInputService.GetAxis().sqrMagnitude < 0.001f ? 
                Physics.ApplyInertia(speed, _movementConfig.inertiaMultiplier, deltaTime) :
                Physics.ApplyAcceleration(speed, _movementConfig.accelerationMultiplier, deltaTime);
            if (speed > _movementConfig.maxSpeed)  speed = _movementConfig.maxSpeed;
            _movementModel.UpdateSpeed(speed);
        }
    }
}