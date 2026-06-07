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
        private readonly BoundsWarper _boundsWarper;
        private readonly IDrawable _drawable;
        private readonly HealthController _healthController;
        private readonly StunController _stunController;
        private readonly ICollidable _collidable;
        private readonly Weapon.Weapon _weapon;
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
            BoundsWarper boundsWarper,
            StunController stunController,
            Weapon.Weapon weapon,
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
            _boundsWarper = boundsWarper;
            _stunController = stunController;
            _weapon = weapon;
            _signalBus = signalBus;
            
            _timeService.OnFixedTick += OnFixedTick;
            _healthController.OnDeath += OnDeath;
            _collidable.OnCollided += OnCollided;
            _boundsChecker.OutOfBounds += OnOutOfBounds;
            _signalBus.Subscribe<CloneCollidedSignal<SpaceshipFacade>>(OnCloneCollided);
        }

        private void OnFixedTick(float fixedDeltaTime)
        {
            _movable.Move(fixedDeltaTime);
            _rotationController.Rotate();
            _boundsChecker.CheckOutOfBounds();
            _drawable.Draw();
        }

        private void OnCollided(Vector2 normal)
        {
            _bouncable.Bounce(normal);
            _healthController.ApplyDamage(1);
            _ = _stunController.ApplyStun(3f);
        }

        private void OnCloneCollided(CloneCollidedSignal<SpaceshipFacade> signal)
        {
            OnCollided(signal.normal);
        }

        private void OnOutOfBounds()
        {
            _boundsWarper.Warp(_movementModel);
        }

        private void OnDeath()
        {
            _signalBus.Fire(new DespawnRequestedSignal<SpaceshipFacade>(this));
        }

        public IReadOnlyPositionable GetPositionable() => _movementModel;
        public IReadOnlyRotationable GetRotationable() => _movementModel;
        public IStunable GetStunable() => _movementModel;
        
        public IDrawable GetDrawable() => _drawable;

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _healthController.OnDeath -= OnDeath;
            _collidable.OnCollided -= OnCollided;
            _signalBus.Unsubscribe<CloneCollidedSignal<SpaceshipFacade>>(OnCloneCollided);
            _healthController.Dispose();
            _weapon.Dispose();
        }
    }
}