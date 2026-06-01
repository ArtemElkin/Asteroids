using System;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Asteroid.Signals;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;

namespace _Project.Features.Asteroid
{
    public class AsteroidFacade : IDisposable
    {
        private readonly AsteroidMovementController _movementController;
        private readonly BoundsChecker _boundsChecker;
        private readonly IProjectileCollisionable _projectileCollisionable;
        private readonly IDrawable _asteroidView;
        private readonly ITimeService _timeService;
        private readonly ISignalBus _signalBus;
        

        public AsteroidFacade(
            AsteroidMovementController movementController,
            BoundsChecker boundsChecker,
            IProjectileCollisionable projectileCollisionable,
            IDrawable asteroidView,
            ITimeService timeService,
            ISignalBus signalBus)
        {
            _movementController = movementController;
            _boundsChecker = boundsChecker;
            _projectileCollisionable = projectileCollisionable;
            _asteroidView = asteroidView;
            _timeService = timeService;
            _signalBus = signalBus;
            
            _timeService.OnFixedTick += OnFixedTick;
            _projectileCollisionable.OnProjectileCollisioned += Destruct;
        }
        
        public IDrawable GetDrawable() => _asteroidView;

        private void OnFixedTick()
        {
            _movementController.Move(_timeService.FixedDeltaTime);
            _boundsChecker.CheckOutOfBounds();
            _asteroidView.Draw();
        }

        private void Destruct()
        {
            _signalBus.Fire(new DespawnRequestedSignal(this));
        }

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _projectileCollisionable.OnProjectileCollisioned -= Destruct;
        }
    }
}