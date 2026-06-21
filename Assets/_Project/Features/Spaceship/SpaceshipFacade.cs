using System.Collections.Generic;
using _Project.Core.EventBus;
using _Project.Core.GameLifecycle.Events;
using _Project.Core.Physics;
using _Project.Core.Physics.Collision;
using _Project.Core.Physics.Collision.Events;
using _Project.Core.Physics.Movement;
using _Project.Core.Render;
using _Project.Core.Services;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Common.EntitiesLifecycle.Events;
using _Project.Features.Common.ScreenWrapClone;
using _Project.Features.Spaceship.Events;
using _Project.Features.Spaceship.Health;
using _Project.Features.Spaceship.Stun;
using _Project.Features.Spaceship.Weapon;
using _Project.Features.Spaceship.Weapon.LaserWeapon;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.Spaceship
{
    public class SpaceshipFacade : IFacade
    {
        public MovementModel MovementModel { get; }
        public IDrawable Drawable { get; }
        private readonly IMovable _movable;
        private readonly IRotatable _rotatable;
        private readonly BoundsChecker _boundsChecker;
        private readonly BoundsWarper _boundsWarper;
        private readonly HealthController _healthController;
        private readonly StunController _stunController;
        private readonly ICollidable _collidable;
        private readonly Dictionary<WeaponType, IWeapon> _weapons;
        private readonly IScreenWrapCloneSet _screenWrapCloneSet;
        private readonly ITimeService _timeService;
        private readonly IEventBus _eventBus;


        public SpaceshipFacade(
            MovementModel movementModel,
            IDrawable drawable,
            IMovable movable,
            IRotatable rotatable,
            BoundsChecker boundsChecker,
            BoundsWarper boundsWarper,
            HealthController healthController,
            StunController stunController,
            ICollidable collidable,
            Dictionary<WeaponType, IWeapon> weapons,
            IScreenWrapCloneSet screenWrapCloneSet,
            ITimeService timeService,
            IEventBus eventBus)
        {
            MovementModel = movementModel;
            Drawable = drawable;
            _movable = movable;
            _rotatable = rotatable;
            _boundsChecker = boundsChecker;
            _boundsWarper = boundsWarper;
            _healthController = healthController;
            _stunController = stunController;
            _collidable = collidable;
            _weapons = weapons;
            _screenWrapCloneSet = screenWrapCloneSet;
            _timeService = timeService;
            _eventBus = eventBus;
            
            _timeService.OnFixedTick += OnFixedTick;
            _healthController.OnDeath += OnDeath;
            _collidable.OnCollided += OnCollided;
            _boundsChecker.OutOfBounds += OnOutOfBounds;

            var laserWeaponState = _weapons[WeaponType.LaserWeapon] as IReadOnlyLaserWeaponState;
            var spaceshipInfo = new SpaceshipReadOnlyInfo(MovementModel, MovementModel, MovementModel, _healthController.HealthModel, laserWeaponState);
            _eventBus.Publish(new SpaceshipSpawnedEvent(spaceshipInfo));
        }

        private void OnFixedTick(float fixedDeltaTime)
        {
            _movable.Move(fixedDeltaTime);
            _rotatable.Rotate();
            _boundsChecker.CheckOutOfBounds();
            Drawable.Draw(MovementModel.Position, MovementModel.RotationAngle);
            _screenWrapCloneSet.UpdateClones();
        }

        private void OnCollided(CollisionData collisionData)
        {
            _healthController.ApplyDamage(1);
            _eventBus.Publish(new CollisionDetectedEvent(collisionData));
            _ = _stunController.ApplyStun(3f);
        }

        private void OnOutOfBounds()
        {
            _boundsWarper.Warp(MovementModel);
        }

        private void OnDeath()
        {
            _eventBus.Publish(new DespawnRequestedEvent<SpaceshipFacade>(this));
            _eventBus.Publish<GameStopEvent>();
        }

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _healthController.OnDeath -= OnDeath;
            _collidable.OnCollided -= OnCollided;
            _boundsChecker.OutOfBounds -= OnOutOfBounds;
            _collidable.Reset();
            _healthController.Dispose();
            _screenWrapCloneSet.Dispose();
            foreach (var weapon in _weapons.Values)
            {
                weapon.Dispose();
            }
        }
    }
}