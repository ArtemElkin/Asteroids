using _Project.Core.Infrastructure.Config;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Bounds;
using _Project.Features.Gameplay.Signals;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Asteroid
{
    public class AsteroidMovementController : IWarpable
    {
        private bool _isEnteredGameAreaAfterSpawn;
        private Rigidbody2D _rb;
        private Vector2 _currentVelocity;
        private Vector2 _currentPos;
        private bool _isSetup;
        private readonly BoundsService _boundsService;
        private readonly SignalBus _signalBus;


        public AsteroidMovementController(
            BoundsService boundsService,
            SignalBus signalBus)
        {
            _boundsService = boundsService;
            _signalBus = signalBus;
        }


        public void Setup(Rigidbody2D rb, Vector2 initialPosition, Vector2 initialDirection, float initialSpeed)
        {
            _rb = rb;
            
            _currentPos = initialPosition;
            SetVelocity(initialSpeed, initialDirection);
            
            _isEnteredGameAreaAfterSpawn = false;
            _isSetup = true;
        }

        public void Reset()
        {
            _rb = null;
            _currentPos = Vector2.zero;
            _currentVelocity = Vector2.zero;
            _isSetup = false;
        }

        public void UpdatePhysics(float deltaTime)
        {
            if (!_isSetup) return;
            
            MoveAsteroid(deltaTime);
            
            if (_boundsService.IsOutOfBounds(_currentPos) && _isEnteredGameAreaAfterSpawn)
            {
                _signalBus.Fire(new OutOfBoundsSignal(this));
            }
            else if (!_isEnteredGameAreaAfterSpawn && !_boundsService.IsOutOfBounds(_currentPos))
            {
                _isEnteredGameAreaAfterSpawn = true;
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

        private void SetVelocity(float speed, Vector2 direction)
        {
            _currentVelocity = speed * direction;
            if (_currentVelocity.magnitude > speed)
            {
                _currentVelocity = _currentVelocity.normalized * speed;
            }
        }
        
        private void MoveAsteroid(float deltaTime)
        {
            _currentPos += _currentVelocity * deltaTime;
            _rb.MovePosition(_currentPos);
        }
    }
}