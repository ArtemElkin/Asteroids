using _Project.Core.EventBus;
using _Project.Core.GameLifecycle;
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
using _Project.Features.Common.ScreenWrapClone;

namespace _Project.Features.Asteroid
{
    public class AsteroidFacade : IFacade
    {
        private readonly EnemyType _type;
        public IDrawable Drawable { get; }
        private MovementModel MovementModel { get; }
        private readonly IMovable _movable;
        private readonly BoundsChecker _boundsChecker;
        private readonly BoundsWarper  _boundsWarper;
        private readonly ICollidable _collidable;
        private readonly IHitable _hitable;
        private readonly AsteroidDeathHandler _deathHandler;
        private readonly IScreenWrapCloneSet _screenWrapCloneSet;
        private readonly ITimeService _timeService;
        private readonly IGameStateService _gameStateService;
        private readonly IEventBus _eventBus;


        public AsteroidFacade(
            EnemyType type,
            MovementModel movementModel,
            IDrawable drawable,
            IMovable movable,
            BoundsChecker boundsChecker,
            BoundsWarper boundsWarper,
            ICollidable collidable,
            IHitable hitable,
            AsteroidDeathHandler deathHandler,
            IScreenWrapCloneSet screenWrapCloneSet,
            ITimeService timeService,
            IGameStateService gameStateService,
            IEventBus eventBus)
        {
            _type = type;
            MovementModel = movementModel;
            Drawable = drawable;
            _movable = movable;
            _boundsChecker = boundsChecker;
            _boundsWarper = boundsWarper;
            _collidable = collidable;
            _hitable = hitable;
            _deathHandler =  deathHandler;
            _screenWrapCloneSet = screenWrapCloneSet;
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
            if (_gameStateService.CurrentState is GameState.Running or GameState.GameOver)
            {
                _movable.Move(fixedDeltaTime);
                _boundsChecker.CheckOutOfBounds();
                Drawable.Draw(MovementModel.Position, MovementModel.RotationAngle);
                _screenWrapCloneSet.UpdateClones();
            }
        }

        private void OnCollided(CollisionData collisionData)
        {
            if (_boundsChecker.IsEnteredGameAreaAfterSpawn)
            {
                _eventBus.Publish(new CollisionDetectedEvent(collisionData));
            }
        }

        private void OnOutOfBounds()
        {
            _boundsWarper.Warp(MovementModel);
        }

        private void OnEnteredGameArea()
        {
            _collidable.ActivateCollision();
        }

        private void OnHit(HitInfo hitInfo)
        {
            _deathHandler.HandleDeath(this, hitInfo, _type);
        }

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _screenWrapCloneSet.Dispose();
            _collidable.OnCollided -= OnCollided;
            _hitable.OnHit -= OnHit;
            _boundsChecker.OutOfBounds -= OnOutOfBounds;
            _boundsChecker.EnteredGameArea -= OnEnteredGameArea;
            _collidable.Reset();
        }
    }
}