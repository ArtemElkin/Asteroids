using _Project.Core.EventBus;
using _Project.Core.GameLifecycle;
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
        private readonly UFODeathHandler _deathHandler;
        private readonly ITimeService _timeService;
        private readonly IGameStateService _gameStateService;
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
            UFODeathHandler deathHandler,
            ITimeService timeService,
            IGameStateService gameStateService,
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
            _deathHandler = deathHandler;
            _timeService = timeService;
            _gameStateService = gameStateService;
            _eventBus = eventBus;
            
            _timeService.OnFixedTick += OnFixedTick;
            _collidable.OnCollided += OnCollided;
            _hitable.OnHit += OnHit;
            _boundsChecker.OutOfBounds += OnOutOfBounds;
            _boundsChecker.EnteredGameArea += OnEnteredGameArea;
        }

        private void OnFixedTick(float fixedDeltaTime)
        {
            if (_gameStateService.CurrentState is GameState.Running)
            {
                _targetFollower.UpdateTarget();
            }
            if (_gameStateService.CurrentState is GameState.Running or GameState.GameOver)
            {
                _movable.Move(fixedDeltaTime);
                _rotatable.Rotate();
                _boundsChecker.CheckOutOfBounds();
                Drawable.Draw(MovementModel.Position, MovementModel.RotationAngle);
            }
        }

        private void OnEnteredGameArea()
        {
            _collidable.ActivateCollision();
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
            _deathHandler.HandleDeath(this, hitInfo, Type);
        }
        
        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _collidable.OnCollided -= OnCollided;
            _hitable.OnHit -= OnHit;
            _boundsChecker.OutOfBounds -= OnOutOfBounds;
            _boundsChecker.EnteredGameArea -= OnEnteredGameArea;
            _collidable.Reset();
        }
    }
}