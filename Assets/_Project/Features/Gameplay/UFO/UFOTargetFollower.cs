using _Project.Core.Physics;
using _Project.Core.Tools;
using _Project.Features.Gameplay.Spaceship;


namespace _Project.Features.Gameplay.UFO
{
    public class UFOTargetFollower
    {
        private bool _isSetup;
        private bool _hasTarget;
        private MovementModel _movementModel;
        private IReadOnlyPositionable _targetPositionable;
        private Storage<SpaceshipComponent> _spaceshipStorage;


        public void Setup(
            MovementModel movementModel,
            Storage<SpaceshipComponent>  spaceshipStorage)
        {
            _movementModel = movementModel;
            _spaceshipStorage = spaceshipStorage;
            TryGetTarget();
            _isSetup = true;
        }

        public void UpdateTarget()
        {
            if (!_isSetup) return;
            if (!_hasTarget)
            {
                TryGetTarget();
                if (!_hasTarget) return;
            }
            var direction = _targetPositionable.Position - _movementModel.Position;
            if (direction.sqrMagnitude > 1) 
            {
                direction = direction.normalized;
            }
            _movementModel.UpdateMoveDirection(direction);
        }

        private void TryGetTarget()
        {
            _hasTarget = _spaceshipStorage.TryGetFirst(out var spaceship);
            if (_hasTarget)
            {
                _targetPositionable = spaceship.GetPositionable();
            }
        }

        public void Reset()
        {
            _isSetup = false;
            _movementModel = null;
            _targetPositionable = null;
        }
    }
}