using System;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Signals;

namespace _Project.Features.Asteroid
{
    public class AsteroidFacade : IFacade
    {
        private readonly AsteroidMovementController _movementController;
        private readonly BoundsChecker _boundsChecker;
        private readonly IDrawable _asteroidView;
        private readonly ICollidable _collidable;
        private readonly IHitable _hitable;
        private readonly ITimeService _timeService;
        private readonly ISignalBus _signalBus;
        

        public AsteroidFacade(
            AsteroidMovementController movementController,
            BoundsChecker boundsChecker,
            IDrawable asteroidView,
            ICollidable collidable,
            IHitable hitable,
            ITimeService timeService,
            ISignalBus signalBus)
        {
            _movementController = movementController;
            _boundsChecker = boundsChecker;
            _asteroidView = asteroidView;
            _collidable = collidable;
            _hitable = hitable;
            _timeService = timeService;
            _signalBus = signalBus;
            
            _timeService.OnFixedTick += OnFixedTick;
            _collidable.OnCollided += OnCollided;
            _hitable.OnHit += Destruct;
        }
        
        public IDrawable GetDrawable() => _asteroidView;

        private void OnFixedTick()
        {
            _movementController.Move(_timeService.FixedDeltaTime);
            _boundsChecker.CheckOutOfBounds();
            _asteroidView.Draw();
        }

        private void OnCollided(Vector2 normal)
        {
            _movementController.Bounce(normal);
        }

        private void Destruct()
        {
            _signalBus.Fire(new DespawnRequestedSignal<AsteroidFacade>(this));
        }

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _collidable.OnCollided -= OnCollided;
            _hitable.OnHit -= Destruct;
        }
    }
}