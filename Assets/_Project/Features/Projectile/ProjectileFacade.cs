using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Signals;

namespace _Project.Features.Projectile
{
    public class ProjectileFacade : IFacade
    {
        private readonly IDrawable _drawable;
        private readonly ICollidable _collidable;
        private readonly IMovable _movable;
        private readonly BoundsChecker _boundsChecker;
        private readonly ITimeService _timeService;
        private readonly ISignalBus _signalBus;


        public ProjectileFacade(
            IDrawable drawable,
            ICollidable collidable,
            IMovable movable,
            BoundsChecker boundsChecker,
            ITimeService timeService,
            ISignalBus signalBus)
        {
            _drawable = drawable;
            _collidable = collidable;
            _movable = movable;
            _boundsChecker = boundsChecker;
            _timeService = timeService;
            _signalBus = signalBus;
            _timeService.OnFixedTick += OnFixedTick;
            _collidable.OnCollided += OnCollided;
            _boundsChecker.OutOfBounds += OnOutOfBounds;
        }

        private void OnFixedTick()
        {
            _movable.Move(_timeService.FixedDeltaTime);
            _boundsChecker.CheckOutOfBounds();
            _drawable.Draw();
        }

        private void OnCollided(Vector2 normal)
        {
            _signalBus.Fire(new DespawnRequestedSignal<ProjectileFacade>(this));
        }

        private void OnOutOfBounds()
        {
            _signalBus.Fire(new DespawnRequestedSignal<ProjectileFacade>(this));
        }
        
        public IDrawable GetDrawable() => _drawable;
        
        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _collidable.OnCollided -= OnCollided;
            _boundsChecker.OutOfBounds -= OnOutOfBounds;
        }
    }
}