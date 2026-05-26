using _Project.Core.Infrastructure.Config;
using _Project.Core.Input;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipMovementController : IInitializable, IFixedTickable
    {
        private float _maxSpeed;
        private Rigidbody2D _rb;
        private Vector2 _direction;
        private Vector2 _currentVelocity;
        private Vector2 _currentPos;
        private float _deltaTime;
        private bool _isSetup;
        private readonly SpaceshipAccelerationHandler _accelerationHandler;
        private readonly SpaceshipInertiaHandler _inertiaHandler;
        private readonly IMovementInputService _movementInputService;
        private readonly IConfigProvider _configProvider;


        public SpaceshipMovementController(
            IMovementInputService movementInputService,
            SpaceshipAccelerationHandler accelerationHandler,
            SpaceshipInertiaHandler inertiaHandler,
            IConfigProvider configProvider)
        {
            _movementInputService = movementInputService;
            _accelerationHandler = accelerationHandler;
            _inertiaHandler = inertiaHandler;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            var config = _configProvider.GetConfigFromJson<SpaceshipMovementConfig>("SpaceshipMovementConfig");
            _maxSpeed = config.maxSpeed;
        }

        public void Setup(Rigidbody2D rb)
        {
            _rb = rb;
            _currentPos = _rb.position;
            _isSetup = true;
        }

        public void Reset()
        {
            _rb = null;
            _currentPos = Vector2.zero;
            _isSetup = false;
        }

        public void FixedTick()
        {
            if (!_isSetup) return;
            
            _deltaTime = Time.fixedDeltaTime;
            UpdateDirection();
            RotateSpaceship();
            MoveSpaceship();
        }

        private void UpdateDirection()
        {
            _direction.x = _movementInputService.GetHorizontalAxis();
            _direction.y = _movementInputService.GetVerticalAxis();
    
            if (_direction.sqrMagnitude > 1) 
            {
                _direction.Normalize();
            }
        }

        private void RotateSpaceship()
        {
            float angle = Mathf.Atan2(_currentVelocity.y, _currentVelocity.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle - 90); 
            _rb.MoveRotation(rotation);
        }

        private void MoveSpaceship()
        {
            _currentVelocity = _direction == Vector2.zero ? 
                _inertiaHandler.ApplyInertia(_currentVelocity, _deltaTime) :
                _accelerationHandler.ApplyAcceleration(_currentVelocity, _direction, _deltaTime);

            if (_currentVelocity.magnitude > _maxSpeed)
            {
                _currentVelocity = _currentVelocity.normalized * _maxSpeed;
            }
            
            _currentPos += _currentVelocity * _deltaTime;
            _rb.MovePosition(_currentPos);
        }
    }
}