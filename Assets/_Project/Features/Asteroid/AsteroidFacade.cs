using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Signals;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.Asteroid
{
    public class AsteroidFacade : IFacade
    {
        private readonly MovementModel _movementModel;
        private readonly IMovable _movable;
        private readonly IBouncable _bouncable;
        private readonly BoundsChecker _boundsChecker;
        private readonly BoundsWarper  _boundsWarper;
        private readonly IDrawable _drawable;
        private readonly ICollidable _collidable;
        private readonly IHitable _hitable;
        private readonly AsteroidDestructor _destructor;
        private readonly ITimeService _timeService;
        private readonly ISignalBus _signalBus;
        
        
        public AsteroidFacade(
            MovementModel movementModel,
            IMovable movable,
            IBouncable bouncable,
            BoundsChecker boundsChecker,
            BoundsWarper boundsWarper,
            IDrawable drawable,
            ICollidable collidable,
            IHitable hitable,
            AsteroidDestructor destructor,
            ITimeService timeService,
            ISignalBus signalBus)
        {
            _movementModel = movementModel;
            _movable = movable;
            _bouncable = bouncable;
            _boundsChecker = boundsChecker;
            _boundsWarper = boundsWarper;
            _drawable = drawable;
            _collidable = collidable;
            _hitable = hitable;
            _destructor = destructor;
            _timeService = timeService;
            _signalBus = signalBus;
            
            _timeService.OnFixedTick += OnFixedTick;
            _collidable.OnCollided += OnCollided;
            _hitable.OnHit += Destruct;
            _boundsChecker.OutOfBounds += OnOutOfBounds;
            _signalBus.Subscribe<CloneCollidedSignal<AsteroidFacade>>(OnCloneCollided);
        }

        private void OnCloneCollided(CloneCollidedSignal<AsteroidFacade> signal)
        {
            OnCollided(signal.normal);
        }

        public IDrawable GetDrawable() => _drawable;
        public IReadOnlyPositionable GetPositionable() => _movementModel;
        public IReadOnlyRotationable GetRotationable() => _movementModel;

        private void OnFixedTick(float fixedDeltaTime)
        {
            _movable.Move(fixedDeltaTime);
            _boundsChecker.CheckOutOfBounds();
            _drawable.Draw();
        }

        private void OnCollided(Vector2 normal)
        {
            if (_boundsChecker.IsEnteredGameAreaAfterSpawn)
            {
                _bouncable.Bounce(normal);
            }
        }

        private void OnOutOfBounds()
        {
            _boundsWarper.Warp(_movementModel);
        }

        private void Destruct()
        {
            _destructor.Destruct(this);
        }

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _collidable.OnCollided -= OnCollided;
            _hitable.OnHit -= Destruct;
            _signalBus.Unsubscribe<CloneCollidedSignal<AsteroidFacade>>(OnCloneCollided);
        }
    }
}