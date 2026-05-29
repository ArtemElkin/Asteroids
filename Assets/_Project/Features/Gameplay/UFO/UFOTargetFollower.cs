using _Project.Core.Math;
using _Project.Core.Physics;


namespace _Project.Features.Gameplay.UFO
{
    public class UFOTargetFollower
    {
        private bool _isSetup;
        CustomVector2 _direction;
        private MovementModel _movementModel;
        private IReadOnlyPositionable _targetPositionable;


        public void Setup(
            MovementModel movementModel,
            IReadOnlyPositionable targetPositionable)
        {
            _movementModel = movementModel;
            _targetPositionable = targetPositionable;
            _isSetup = true;
        }

        public void UpdateTarget()
        {
            if (!_isSetup) return;
            
            _direction = _targetPositionable.Position - _movementModel.Position;
            if (_direction.sqrMagnitude > 1) 
            {
                _direction = _direction.normalized;
            }
            _movementModel.UpdateMoveDirection(_direction);
        }

        public void Reset()
        {
            _isSetup = false;
            _direction = CustomVector2.zero;
            _movementModel = null;
            _targetPositionable = null;
        }
    }
}