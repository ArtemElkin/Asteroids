using System;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;

namespace _Project.Features.Spaceship
{
    public class SpaceshipFacade : IDisposable
    {
        private readonly MovementModel _movementModel;
        private readonly SpaceshipMovementController _movementController;
        private readonly SpaceshipRotationController _rotationController;
        private readonly BoundsChecker _boundsChecker;
        private readonly IDrawable _spaceshipView;
        private readonly ITimeService _timeService;


        public SpaceshipFacade(
            ITimeService timeService,
            MovementModel movementModel,
            SpaceshipMovementController movementController,
            SpaceshipRotationController rotationController,
            IDrawable spaceshipView,
            BoundsChecker boundsChecker)
        {
            _timeService = timeService;
            _movementModel = movementModel;
            _movementController = movementController;
            _rotationController = rotationController;
            _spaceshipView = spaceshipView;
            _boundsChecker = boundsChecker;
            
            _timeService.OnFixedTick += OnFixedTick;
        }

        private void OnFixedTick()
        {
            _movementController.Move(_timeService.FixedDeltaTime);
            _rotationController.Rotate();
            _boundsChecker.CheckOutOfBounds();
            _spaceshipView.Draw();
        }

        public IReadOnlyPositionable GetPositionable() => _movementModel;
        public IReadOnlyRotatable GetRotatable() => _movementModel;

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
        }
    }
}