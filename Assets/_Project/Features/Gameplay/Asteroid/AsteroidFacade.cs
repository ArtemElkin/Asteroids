using System;
using _Project.Core.Services;
using _Project.Features.Gameplay.Bounds;
using _Project.Features.Gameplay.Common;

namespace _Project.Features.Gameplay.Asteroid
{
    public class AsteroidFacade : IDisposable
    {
        private readonly AsteroidMovementController _movementController;
        private readonly BoundsChecker _boundsChecker;
        private readonly IDrawable _asteroidView;
        private readonly ITimeService _timeService;


        public AsteroidFacade(
            AsteroidMovementController movementController,
            BoundsChecker boundsChecker,
            IDrawable asteroidView,
            ITimeService timeService)
        {
            _movementController = movementController;
            _boundsChecker = boundsChecker;
            _asteroidView = asteroidView;
            _timeService = timeService;
            _timeService.OnFixedTick += OnFixedTick;
        }

        private void OnFixedTick()
        {
            _movementController.Move(_timeService.FixedDeltaTime);
            _boundsChecker.CheckOutOfBounds();
            _asteroidView.Draw();
        }

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
        }
    }
}