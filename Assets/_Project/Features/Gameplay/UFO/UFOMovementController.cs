using _Project.Core.Math;
using _Project.Core.Physics;


namespace _Project.Features.Gameplay.UFO
{
    public class UFOMovementController : IWarpable
    {
        private CustomVector2 _velocity;
        private MovementModel _movementModel;
        private bool _isSetup;
        

        public void Setup(MovementModel movementModel)
        {
            _movementModel = movementModel;
            _isSetup = true;
        }

        public void UpdatePhysics(float deltaTime)
        {
            if (!_isSetup) return;
            
            UpdateVelocity();
            MoveUFO(deltaTime);
        }

        public void Warp(CustomVector2 position)
        {
            _movementModel.UpdatePosition(position);
        }

        private void UpdateVelocity()
        {
            _velocity = _movementModel.MoveDirection * _movementModel.Speed; 

            if (_velocity.sqrMagnitude > 1)
            {
                _velocity = _velocity.normalized * _movementModel.Speed;
            }
            _movementModel.UpdateVelocity(_velocity);
        }
        
        private void MoveUFO(float deltaTime)
        {
            _movementModel.UpdatePosition(_movementModel.Velocity * deltaTime);
        }
        
        public void Reset()
        {
            _movementModel.UpdatePosition(CustomVector2.zero);
            _isSetup = false;
        }
    }
}