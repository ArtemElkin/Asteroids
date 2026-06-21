using _Project.Core.EventBus;
using _Project.Core.Physics;
using _Project.Core.Physics.Collision;
using _Project.Core.Physics.Collision.Events;
using _Project.Core.Physics.Movement;
using _Project.Core.Render;
using _Project.Core.Services;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.EnemyAwardsService;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Common.EntitiesLifecycle.Events;
using _Project.Features.Common.Hit;
using _Project.Features.Common.Hit.Events;

namespace _Project.Features.UFO
{
    public class UFOFacade : IFacade
    {
        private const EnemyType Type = EnemyType.UFO;
        private MovementModel MovementModel { get; }
        public IDrawable Drawable { get; }
        private readonly IMovable _movable;
        private readonly IRotatable _rotatable;
        private readonly BoundsWarper _boundsWarper;
        private readonly UFOTargetFollower _targetFollower;
        private readonly BoundsChecker _boundsChecker;
        private readonly ICollidable _collidable;
        private readonly IHitable _hitable;
        private readonly ITimeService _timeService;
        private readonly IEventBus _eventBus;


        public UFOFacade(
            MovementModel movementModel,
            IDrawable drawable,
            IMovable movable,
            IRotatable rotatable,
            UFOTargetFollower targetFollower,
            BoundsChecker boundsChecker,
            BoundsWarper boundsWarper,
            ICollidable collidable,
            IHitable hitable,
            ITimeService timeService,
            IEventBus eventBus)
        {
            MovementModel = movementModel;
            Drawable = drawable;
            _movable = movable;
            _rotatable = rotatable;
            _targetFollower = targetFollower;
            _boundsChecker = boundsChecker;
            _boundsWarper = boundsWarper;
            _collidable = collidable;
            _hitable = hitable;
            _timeService = timeService;
            _eventBus = eventBus;
            
            _timeService.OnFixedTick += OnFixedTick;
            _collidable.OnCollided += OnCollided;
            _hitable.OnHit += OnHit;
            _boundsChecker.OutOfBounds += OnOutOfBounds;
        }

        private void OnFixedTick(float fixedDeltaTime)
        {
            _targetFollower.UpdateTarget();
            _movable.Move(fixedDeltaTime);
            _rotatable.Rotate();
            _boundsChecker.CheckOutOfBounds();
            Drawable.Draw(MovementModel.Position, MovementModel.RotationAngle);
        }

        private void OnCollided(CollisionData collisionData)
        {
            _eventBus.Publish(new CollisionDetectedEvent(collisionData));
        }

        private void OnOutOfBounds()
        {
            _boundsWarper.Warp(MovementModel);
        }

        private void OnHit(HitInfo hitInfo)
        {
            _eventBus.Publish(new HitEvent(hitInfo));
            _eventBus.Publish(new DespawnRequestedEvent<UFOFacade>(this));
            _eventBus.Publish(new EnemyDestroyedEvent(Type));
        }
        
        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _collidable.OnCollided -= OnCollided;
            _hitable.OnHit -= OnHit;
            _boundsChecker.OutOfBounds -= OnOutOfBounds;
            _collidable.Reset();
        }
    }
}