using System.Collections.Generic;
using _Project.Core.Infrastructure.Config;
using _Project.Core.Input;
using _Project.Features.Gameplay.Bounds;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipMovementController : IInitializable, IFixedTickable, IWarpable
    {
        private float _maxSpeed;
        private Rigidbody2D _rb;
        private Dictionary<Rigidbody2D, Vector2> _clonesRigidbodiesOffsets;
        private Vector2 _moveDirection;
        private Vector2 _currentVelocity;
        private Vector2 _currentPos;
        private float _deltaTime;
        private bool _isSetup;
        private readonly SpaceshipAccelerationApplier _accelerationApplier;
        private readonly SpaceshipInertiaApplier _inertiaApplier;
        private readonly IMovementInputService _movementInputService;
        private readonly IConfigProvider _configProvider;
        private readonly BoundsService _boundsService;
        private readonly SignalBus _signalBus;


        public SpaceshipMovementController(
            IMovementInputService movementInputService,
            SpaceshipAccelerationApplier accelerationApplier,
            SpaceshipInertiaApplier inertiaApplier,
            BoundsService boundsService,
            IConfigProvider configProvider,
            SignalBus signalBus)
        {
            _movementInputService = movementInputService;
            _accelerationApplier = accelerationApplier;
            _inertiaApplier = inertiaApplier;
            _boundsService = boundsService;
            _configProvider = configProvider;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            var config = _configProvider.GetConfigFromJson<SpaceshipMovementConfig>("SpaceshipMovementConfig");
            _maxSpeed = config.maxSpeed;
            
            _clonesRigidbodiesOffsets = new Dictionary<Rigidbody2D, Vector2>();
        }

        public void Setup(Rigidbody2D rb, Vector2 initialPosition)
        {
            _rb = rb;
            _currentPos = initialPosition;
            _isSetup = true;
        }

        public void AddClone(Rigidbody2D clone, Vector2 offset)
        {
            _clonesRigidbodiesOffsets[clone] = offset;
        }

        public void Reset()
        {
            _rb = null;
            _clonesRigidbodiesOffsets.Clear();
            _currentPos = Vector2.zero;
            _isSetup = false;
        }

        public void FixedTick()
        {
            if (!_isSetup) return;
            
            _deltaTime = Time.fixedDeltaTime;
            
            UpdateDirection();
            UpdateVelocity();
            // RotateSpaceship();
            MoveSpaceship();
            
            if (_boundsService.IsOutOfBounds(_currentPos))
            {
                _signalBus.Fire(new OutOfBoundsSignal(this));
            }
        }

        public void Warp(Vector3 position)
        {
            _currentPos = position;
        }

        public Vector2 GetLastPosition()
        {
            return _currentPos;
        }

        private void UpdateDirection()
        {
            _moveDirection.x = _movementInputService.GetHorizontalAxis();
            _moveDirection.y = _movementInputService.GetVerticalAxis();
        
            if (_moveDirection.sqrMagnitude > 1) 
            {
                _moveDirection.Normalize();
            }
        }

        // private void RotateSpaceship()
        // {
        //     if (_currentVelocity.sqrMagnitude < 0.001f) return;
        //     
        //     // float angle = Mathf.Atan2(_currentVelocity.y, _currentVelocity.x) * Mathf.Rad2Deg;
        //     var lookPoint = _movementInputService.GetPo
        //     float angle = Quaternion.LookRotation()
        //     Quaternion rotation = Quaternion.Euler(0, 0, angle - 90); 
        //     _rb.MoveRotation(rotation);
        //     foreach (var clone in _clonesRigidbodiesOffsets.Keys)
        //     {
        //         clone.MoveRotation(rotation);
        //     }
        // }

        private void UpdateVelocity()
        {
            _currentVelocity = _moveDirection == Vector2.zero ? 
                _inertiaApplier.ApplyInertia(_currentVelocity, _deltaTime) :
                _accelerationApplier.ApplyAcceleration(_currentVelocity, _moveDirection, _deltaTime);

            if (_currentVelocity.magnitude > _maxSpeed)
            {
                _currentVelocity = _currentVelocity.normalized * _maxSpeed;
            }
        }
        
        private void MoveSpaceship()
        {
            _currentPos += _currentVelocity * _deltaTime;
            _rb.MovePosition(_currentPos);

            foreach (var kv in _clonesRigidbodiesOffsets)
            {
                kv.Key.MovePosition(_currentPos + kv.Value);
            }
        }
    }
}