using _Project.Core.Infrastructure.Config;
using _Project.Core.Input;
using _Project.Core.Math;
using _Project.Core.Physics;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipMovementController : IInitializable, IWarpable
    {
        private float _maxSpeed;
        private bool _isSetup;
        private MovementModel _movementModel;
        private readonly SpaceshipAccelerationApplier _accelerationApplier;
        private readonly SpaceshipInertiaApplier _inertiaApplier;
        private readonly IMovementInputService _movementInputService;
        private readonly IConfigProvider _configProvider;


        public SpaceshipMovementController(
            IMovementInputService movementInputService,
            SpaceshipAccelerationApplier accelerationApplier,
            SpaceshipInertiaApplier inertiaApplier,
            IConfigProvider configProvider)
        {
            _movementInputService = movementInputService;
            _accelerationApplier = accelerationApplier;
            _inertiaApplier = inertiaApplier;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            var config = _configProvider.GetConfigFromJson<SpaceshipMovementConfig>("SpaceshipMovementConfig");
            _maxSpeed = config.maxSpeed;
        }

        public void Setup(MovementModel movementModel, float maxSpeed)
        {
            _movementModel = movementModel;
            _maxSpeed = maxSpeed;
            _isSetup = true;
        }

        public void Reset()
        {
            _isSetup = false;
            _movementModel = null;
            _maxSpeed = 0f;
        }

        public void UpdatePhysics(float deltaTime)
        {
            if (!_isSetup) return;
            
            UpdateDirection();
            UpdateVelocity(deltaTime);
            MoveSpaceship(deltaTime);
            
        }

        public void Warp(CustomVector2 position)
        {
            _movementModel.UpdatePosition(position);
        }

        private void UpdateDirection()
        {
            var x = _movementInputService.GetHorizontalAxis();
            var y = _movementInputService.GetVerticalAxis();
            var direction = new CustomVector2(x, y);
            if (direction.sqrMagnitude > 1) 
            {
                direction = direction.normalized;
            }
            _movementModel.UpdateMoveDirection(direction);
        }

        private void UpdateVelocity(float deltaTime)
        {
            var velocity = _movementModel.MoveDirection == CustomVector2.zero ? 
                (_inertiaApplier.ApplyInertia(_movementModel.Velocity, deltaTime)) :
                _accelerationApplier.ApplyAcceleration(_movementModel.Velocity, _movementModel.MoveDirection, deltaTime);

            if (velocity.magnitude > _maxSpeed)
            {
                _movementModel.UpdateVelocity(velocity.normalized * _maxSpeed);
            }
        }
        
        private void MoveSpaceship(float  deltaTime)
        {
            _movementModel.UpdatePosition(_movementModel.Velocity * deltaTime);
        }
    }
}