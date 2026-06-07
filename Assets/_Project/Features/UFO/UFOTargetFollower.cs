using _Project.Core.Physics;
using _Project.Core.Tools;
using _Project.Features.Spaceship;

namespace _Project.Features.UFO
{
    public class UFOTargetFollower
    {
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
            if (TryGetTarget(out var targetPositionable, out var targetStunable))
            {
                if (targetStunable.IsStunned) return;
                
                var direction = targetPositionable.Position - _movementModel.Position;
                if (direction.sqrMagnitude > 1) 
                {
                    direction = direction.normalized;
                }
                _movementModel.UpdateMoveDirection(direction);
            }
        }

        private bool TryGetTarget(out IReadOnlyPositionable targetPositionable, out IReadOnlyStunable targetStunable)
        {
            targetPositionable = null;
            targetStunable = null;
            var hasTarget = _spaceshipStorage.TryGetFirst(out var spaceship);
            if (hasTarget)
            {
                targetPositionable = spaceship.MovementModel;
                targetStunable = spaceship.MovementModel;
                return true;
            }
            return false;
        }
    }
}