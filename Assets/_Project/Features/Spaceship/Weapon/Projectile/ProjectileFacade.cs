using _Project.Core.EventBus;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Render;
using _Project.Core.Services;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Event;

namespace _Project.Features.Spaceship.Weapon.Projectile
{
    public class ProjectileFacade : IFacade
    {
        public MovementModel MovementModel { get; }
        public IDrawable Drawable { get; }
        private readonly ICollidable _collidable;
        private readonly IMovable _movable;
        private readonly BoundsChecker _boundsChecker;
        private readonly ITimeService _timeService;
        private readonly IEventBus _eventBus;


        public ProjectileFacade(
            MovementModel movementModel,
            IDrawable drawable,
            ICollidable collidable,
            IMovable movable,
            BoundsChecker boundsChecker,
            ITimeService timeService,
            IEventBus eventBus)
        {
            MovementModel = movementModel;
            Drawable = drawable;
            _collidable = collidable;
            _movable = movable;
            _boundsChecker = boundsChecker;
            _timeService = timeService;
            _eventBus = eventBus;
            _timeService.OnFixedTick += OnFixedTick;
            _collidable.OnCollided += OnCollided;
            _boundsChecker.OutOfBounds += OnOutOfBounds;
        }

        private void OnFixedTick(float fixedDeltaTime)
        {
            _movable.Move(fixedDeltaTime);
            _boundsChecker.CheckOutOfBounds();
            Drawable.Draw(MovementModel.Position, MovementModel.RotationAngle);
        }

        private void OnCollided(ICollidable other, Vector2 collisionNormal)
        {
            _eventBus.Publish(new DespawnRequestedEvent<ProjectileFacade>(this));
        }

        private void OnOutOfBounds()
        {
            _eventBus.Publish(new DespawnRequestedEvent<ProjectileFacade>(this));
        }
        
        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _collidable.OnCollided -= OnCollided;
            _boundsChecker.OutOfBounds -= OnOutOfBounds;
        }
    }
}