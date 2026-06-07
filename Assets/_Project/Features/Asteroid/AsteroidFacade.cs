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
        public MovementModel MovementModel { get; }
        private readonly IMovable _movable;
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
            BoundsChecker boundsChecker,
            BoundsWarper boundsWarper,
            IDrawable drawable,
            ICollidable collidable,
            IHitable hitable,
            AsteroidDestructor destructor,
            ITimeService timeService,
            ISignalBus signalBus)
        {
            MovementModel = movementModel;
            _movable = movable;
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
        }

        public IDrawable GetDrawable() => _drawable;

        private void OnFixedTick(float fixedDeltaTime)
        {
            _movable.Move(fixedDeltaTime);
            _boundsChecker.CheckOutOfBounds();
            _drawable.Draw(MovementModel.Position, MovementModel.RotationAngle);
        }

        private void OnCollided(ICollidable other, Vector2 collisionNormal)
        {
            if (_boundsChecker.IsEnteredGameAreaAfterSpawn)
            {
                var collisionData = new CollisionData(MovementModel, other.MovementModel, collisionNormal);
                _signalBus.Fire(new CollisionDetectedSignal(collisionData));
            }
        }

        private void OnOutOfBounds()
        {
            _boundsWarper.Warp(MovementModel);
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
        }
    }
}