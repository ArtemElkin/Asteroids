using _Project.Core.Math;
using _Project.Core.Physics;


namespace _Project.Features.Gameplay.Asteroid
{
    public class AsteroidMovementController : IWarpable
    {
        private MovementModel _movementModel;
        private bool _isSetup;


        public void Setup(MovementModel movementModel)
        {
            _movementModel = movementModel;
            SetVelocity();
            
            _isSetup = true;
        }

        public void Reset()
        {
            _movementModel = null;
            _isSetup = false;
        }

        public void UpdatePhysics(float deltaTime)
        {
            if (!_isSetup) return;
            
            MoveAsteroid(deltaTime);
        }

        public void Warp(CustomVector2 position)
        {
            _movementModel.UpdatePosition(position);
        }

        private void SetVelocity()
        {
            var velocity = _movementModel.Speed * _movementModel.MoveDirection;
            if (velocity.magnitude > _movementModel.Speed)
            {
                velocity = velocity.normalized * _movementModel.Speed;
            }
            _movementModel.UpdateVelocity(velocity);
        }
        
        private void MoveAsteroid(float deltaTime)
        {
            _movementModel.UpdatePosition(_movementModel.Velocity * deltaTime);
        }
    }
}