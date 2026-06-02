using System;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Signals;
using _Project.Features.Spaceship.Health;

namespace _Project.Features.Spaceship
{
    public class SpaceshipFacade : IFacade
    {
        private readonly MovementModel _movementModel;
        private readonly IMovable _movable;
        private readonly IBouncable _bouncable;
        private readonly IRotatable _rotationController;
        private readonly BoundsChecker _boundsChecker;
        private readonly IDrawable _drawable;
        private readonly HealthController _healthController;
        private readonly ICollidable _collidable;
        private readonly ITimeService _timeService;
        private readonly ISignalBus _signalBus;


        public SpaceshipFacade(
            ITimeService timeService,
            MovementModel movementModel,
            IMovable movable,
            IBouncable bouncable,
            IRotatable rotationController,
            IDrawable drawable,
            HealthController healthController,
            ICollidable collidable,
            BoundsChecker boundsChecker,
            ISignalBus signalBus)
        {
            _timeService = timeService;
            _movementModel = movementModel;
            _movable = movable;
            _bouncable = bouncable;
            _rotationController = rotationController;
            _drawable = drawable;
            _healthController = healthController;
            _collidable = collidable;
            _boundsChecker = boundsChecker;
            _signalBus = signalBus;
            
            _timeService.OnFixedTick += OnFixedTick;
            _healthController.OnDeath += OnDeath;
            _collidable.OnCollided += OnCollided;
        }

        private void OnFixedTick()
        {
            _movable.Move(_timeService.FixedDeltaTime);
            _rotationController.Rotate();
            _boundsChecker.CheckOutOfBounds();
            _drawable.Draw();
        }

        private void OnCollided(Vector2 normal)
        {
            _bouncable.Bounce(normal);
            _healthController.ApplyDamage(1);
        }

        private void OnDeath()
        {
            _signalBus.Fire(new DespawnRequestedSignal<SpaceshipFacade>(this));
        }

        public IReadOnlyPositionable GetPositionable() => _movementModel;
        public IReadOnlyRotationable GetRotationable() => _movementModel;
        
        public IDrawable GetDrawable() => _drawable;

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _healthController.OnDeath -= OnDeath;
            _collidable.OnCollided -= OnCollided;
            _healthController.Dispose();
        }
    }
}