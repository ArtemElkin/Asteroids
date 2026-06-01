using System;
using _Project.Core.Services;
using _Project.Features.Gameplay.Bounds;
using _Project.Features.Gameplay.Common;

namespace _Project.Features.Gameplay.UFO
{
    public class UFOFacade : IDisposable
    {
        private readonly UFOMovementController _movementController;
        private readonly UFORotationController _rotationController;
        private readonly UFOTargetFollower _targetFollower;
        private readonly BoundsChecker _boundsChecker;
        private readonly IDrawable _view;
        private readonly ITimeService _timeService;


        public UFOFacade(
            UFOMovementController movementController,
            UFORotationController rotationController,
            UFOTargetFollower targetFollower,
            BoundsChecker boundsChecker,
            IDrawable view,
            ITimeService timeService)
        {
            _movementController = movementController;
            _rotationController = rotationController;
            _targetFollower = targetFollower;
            _boundsChecker = boundsChecker;
            _view = view;
            _timeService = timeService;
            
            _timeService.OnFixedTick += OnFixedTick;
        }

        private void OnFixedTick()
        {
            _targetFollower.UpdateTarget();
            _movementController.Move(_timeService.FixedDeltaTime);
            _rotationController.Rotate();
            _boundsChecker.CheckOutOfBounds();
            _view.Draw();
        }

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
        }
    }
}