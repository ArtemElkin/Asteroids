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
        private readonly MovementModel _movementModel;
        private readonly IDrawable _drawable;
        private readonly ICollidable _collidable;
        private readonly IMovable _movable;
        private readonly BoundsChecker _boundsChecker;
        private readonly ITimeService _timeService;
        private readonly ISignalBus _signalBus;


        public ProjectileFacade(
            MovementModel movementModel,
            IDrawable drawable,
            ICollidable collidable,
            IMovable movable,
            BoundsChecker boundsChecker,
            ITimeService timeService,
            ISignalBus signalBus)
        {
            _movementModel = movementModel;
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

        private void OnFixedTick(float fixedDeltaTime)
        {
            _movable.Move(fixedDeltaTime);
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
        public IReadOnlyPositionable GetPositionable() => _movementModel;
        public IReadOnlyRotationable GetRotationable() => _movementModel;
        
        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _collidable.OnCollided -= OnCollided;
            _boundsChecker.OutOfBounds -= OnOutOfBounds;
        }
    }
}