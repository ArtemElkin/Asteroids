using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Spaceship;


namespace _Project.Features.Gameplay.UFO
{
    public class UFOTargetFollower
    {
        private bool _isSetup;
        private bool _hasTarget;
        CustomVector2 _direction;
        private MovementModel _movementModel;
        private IReadOnlyPositionable _targetPositionable;


        public void Setup(
            MovementModel movementModel,
            Storage<SpaceshipComponent>  spaceshipStorage)
        {
            _movementModel = movementModel;
            _hasTarget = spaceshipStorage.TryGetFirst(out var spaceship);
            if (_hasTarget)
            {
                _targetPositionable = spaceship.GetPositionable();
            }
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