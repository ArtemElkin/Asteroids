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
        private readonly IMovable _movable;
        private readonly IBouncable _bouncable;
        private readonly BoundsChecker _boundsChecker;
        private readonly IDrawable _drawable;
        private readonly ICollidable _collidable;
        private readonly IHitable _hitable;
        private readonly ITimeService _timeService;
        private readonly ISignalBus _signalBus;
        

        public AsteroidFacade(
            IMovable movable,
            IBouncable bouncable,
            BoundsChecker boundsChecker,
            IDrawable drawable,
            ICollidable collidable,
            IHitable hitable,
            ITimeService timeService,
            ISignalBus signalBus)
        {
            _movable = movable;
            _bouncable = bouncable;
            _boundsChecker = boundsChecker;
            _drawable = drawable;
            _collidable = collidable;
            _hitable = hitable;
            _timeService = timeService;
            _signalBus = signalBus;
            
            _timeService.OnFixedTick += OnFixedTick;
            _collidable.OnCollided += OnCollided;
            _hitable.OnHit += Destruct;
        }
        
        public IDrawable GetDrawable() => _drawable;

        private void OnFixedTick()
        {
            _movable.Move(_timeService.FixedDeltaTime);
            _boundsChecker.CheckOutOfBounds();
            _drawable.Draw();
        }

        private void OnCollided(Vector2 normal)
        {
            _bouncable.Bounce(normal);
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