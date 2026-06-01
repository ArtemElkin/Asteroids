using _Project.Core.Physics;
using _Project.Core.Tools;
using _Project.Features.Spaceship;

namespace _Project.Features.Gameplay.UFO
{
    public class UFOTargetFollower
    {
        private bool _hasTarget;
        private IReadOnlyPositionable _targetPositionable;
        private readonly MovementModel _movementModel;
        private readonly Storage<SpaceshipFacade> _spaceshipStorage;


        public UFOTargetFollower(
            MovementModel movementModel,
            Storage<SpaceshipFacade> spaceshipStorage)
        {
            _movementModel = movementModel;
            _spaceshipStorage = spaceshipStorage;
        }

        public void UpdateTarget()
        {
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
    }
}