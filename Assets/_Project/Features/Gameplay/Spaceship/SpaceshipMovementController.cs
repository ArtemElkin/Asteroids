using _Project.Core.Input;
using _Project.Core.Math;
using _Project.Core.Physics;


namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipMovementController : BaseMovementController
    {
        private float _maxSpeed;
        private float _accelerationMultiplier;
        private float _inertiaMultiplier;
        private readonly IMovementInputService _movementInputService;


        public SpaceshipMovementController(
            IMovementInputService movementInputService)
        {
            _movementInputService = movementInputService;
        }

        public void Setup(
            MovementModel movementModel, 
            float maxSpeed,
            float accelerationMultiplier,
            float inertiaMultiplier)
        {
            _maxSpeed = maxSpeed;
            _accelerationMultiplier = accelerationMultiplier;
            _inertiaMultiplier = inertiaMultiplier;
            base.Setup(movementModel);
        }
        
        protected override void UpdateDirection()
        {
            var moveDirection = _movementInputService.GetAxis();
            if (moveDirection.sqrMagnitude > 1) 
            {
                moveDirection = moveDirection.normalized;
            }
            _movementModel.UpdateMoveDirection(moveDirection);
        }

        protected override void UpdateVelocity(float deltaTime)
        {
            var velocity = _movementModel.Velocity;
            velocity = _movementModel.MoveDirection == CustomVector2.zero ? 
                (CustomPhysics.ApplyInertia(velocity, _inertiaMultiplier, deltaTime)) :
                CustomPhysics.ApplyAcceleration(velocity, _accelerationMultiplier, _movementModel.MoveDirection, deltaTime);

            if (velocity.magnitude > _maxSpeed)
            {
                velocity = velocity.normalized * _maxSpeed;
            }
            _movementModel.UpdateVelocity(velocity);
        }
        
        public override void Reset()
        {
            base.Reset();
            _maxSpeed = 0f;
        }
    }
}