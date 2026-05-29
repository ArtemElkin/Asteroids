using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Features.Gameplay.Spaceship;


namespace _Project.Features.Gameplay.UFO
{
    public class UFOMovementController : IWarpable
    {
        private CustomVector2 _velocity;
        private MovementModel _movementModel;
        private bool _isSetup;
        private readonly SpaceshipAccelerationApplier _accelerationApplier;
        private readonly SpaceshipInertiaApplier _inertiaApplier;


        public UFOMovementController(
            SpaceshipAccelerationApplier accelerationApplier,
            SpaceshipInertiaApplier inertiaApplier)
        {
            _accelerationApplier =  accelerationApplier;
            _inertiaApplier = inertiaApplier;
        }

        public void Setup(MovementModel movementModel)
        {
            _movementModel = movementModel;
            _isSetup = true;
        }

        public void UpdatePhysics(float deltaTime)
        {
            if (!_isSetup) return;
            
            UpdateVelocity(deltaTime);
            MoveUFO(deltaTime);
        }

        public void Warp(CustomVector2 position)
        {
            _movementModel.UpdatePosition(position);
        }

        private void UpdateVelocity(float deltaTime)
        {
            _velocity = _movementModel.Velocity; 
            _velocity = _movementModel.MoveDirection == CustomVector2.zero ? 
                (_inertiaApplier.ApplyInertia(_velocity, deltaTime)) :
                _accelerationApplier.ApplyAcceleration(_velocity, _movementModel.MoveDirection, deltaTime);
            if (_velocity.sqrMagnitude > 1)
            {
                _velocity = _velocity.normalized * _movementModel.Speed;
            }
            _movementModel.UpdateVelocity(_velocity);
        }
        
        private void MoveUFO(float deltaTime)
        {
            _movementModel.UpdatePosition(_movementModel.Position + _movementModel.Velocity * deltaTime);
        }
        
        public void Reset()
        {
            _velocity = CustomVector2.zero;
            _isSetup = false;
        }
    }
}