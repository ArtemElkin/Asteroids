using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Signals;

namespace _Project.Features.Common.Clone
{
    public class CloneFacade<TOriginFacade> : IFacade where TOriginFacade : IFacade
    {
        private readonly Vector2 _cloneOffset;
        public MovementModel MovementModel { get; }
        private readonly IDrawable _drawable;
        private readonly BoundsChecker _originBoundsChecker;
        private readonly ICollidable _collidable;
        private readonly ITimeService _timeService;
        private readonly ISignalBus _signalBus;


        public CloneFacade(
            ITimeService timeService,
            MovementModel originMovementModel,
            ICollidable collidable,
            BoundsChecker originBoundsChecker,
            IDrawable drawable,
            Vector2 cloneOffset,
            ISignalBus signalBus)
        {
            _originBoundsChecker = originBoundsChecker;
            _timeService = timeService;
            _collidable = collidable;
            MovementModel = originMovementModel;
            _drawable = drawable;
            _cloneOffset = cloneOffset;
            _signalBus = signalBus;
            
            _timeService.OnFixedTick += OnFixedTick;
            _collidable.OnCollided += OnCollided;
        }

        private void OnFixedTick(float fixedDeltaTime)
        {
            _originBoundsChecker.CheckOutOfBounds();
            if (_originBoundsChecker.IsEnteredGameAreaAfterSpawn)
            {
                _drawable.Draw(MovementModel.Position + _cloneOffset, MovementModel.RotationAngle);
            }
        }

        private void OnCollided(ICollidable other, Vector2 collisionNormal)
        {
            var collisionData = new CollisionData(MovementModel, other.MovementModel, collisionNormal);
            _signalBus.Fire(new CollisionDetectedSignal(collisionData));
        }
        
        public IDrawable GetDrawable() => _drawable;

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _collidable.OnCollided -= OnCollided;
        }
    }
}