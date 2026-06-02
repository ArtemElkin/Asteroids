using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Signals;

namespace _Project.Features.UFO
{
    public class UFOFacade : IFacade
    {
        private readonly IMovable _movable;
        private readonly IRotatable _rotatable;
        private readonly IBouncable _bouncable;
        private readonly UFOTargetFollower _targetFollower;
        private readonly BoundsChecker _boundsChecker;
        private readonly IDrawable _drawable;
        private readonly ICollidable _collidable;
        private readonly IHitable _hitable;
        private readonly ITimeService _timeService;
        private readonly ISignalBus _signalBus;


        public UFOFacade(
            IMovable movable,
            IRotatable rotatable,
            IBouncable bouncable,
            UFOTargetFollower targetFollower,
            BoundsChecker boundsChecker,
            IDrawable drawable,
            ICollidable collidable,
            IHitable hitable,
            ITimeService timeService,
            ISignalBus signalBus)
        {
            _movable = movable;
            _rotatable = rotatable;
            _bouncable = bouncable;
            _targetFollower = targetFollower;
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

        private void OnFixedTick()
        {
            _targetFollower.UpdateTarget();
            _movable.Move(_timeService.FixedDeltaTime);
            _rotatable.Rotate();
            _boundsChecker.CheckOutOfBounds();
            _drawable.Draw();
        }

        private void OnCollided(Vector2 normal)
        {
            _bouncable.Bounce(normal);
        }

        private void Destruct()
        {
            _signalBus.Fire(new DespawnRequestedSignal<UFOFacade>(this));
        }
        
        public IDrawable GetDrawable() => _drawable;

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _collidable.OnCollided -= OnCollided;
            _hitable.OnHit -= Destruct;
        }
    }
}