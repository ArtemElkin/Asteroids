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
        public MovementModel MovementModel { get; }
        private readonly IMovable _movable;
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
            MovementModel = movementModel;
            _movable = movable;
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
        }

        private void OnFixedTick(float fixedDeltaTime)
        {
            _movable.Move(fixedDeltaTime);
            _rotationController.Rotate();
            _boundsChecker.CheckOutOfBounds();
            _drawable.Draw(MovementModel.Position, MovementModel.RotationAngle);
        }

        private void OnCollided(ICollidable other, Vector2 collisionNormal)
        {
            var collisionData = new CollisionData(MovementModel, other.MovementModel, collisionNormal);
            _signalBus.Fire(new CollisionDetectedSignal(collisionData));
            _healthController.ApplyDamage(1);
            _ = _stunController.ApplyStun(3f);
        }

        private void OnOutOfBounds()
        {
            _boundsWarper.Warp(MovementModel);
        }

        private void OnDeath()
        {
            _signalBus.Fire(new DespawnRequestedSignal<SpaceshipFacade>(this));
        }

        public IReadOnlyPositionable GetPositionable() => MovementModel;
        public IReadOnlyRotationable GetRotationable() => MovementModel;
        public IStunable GetStunable() => MovementModel;
        
        public IDrawable GetDrawable() => _drawable;
        
        public float GetMass() => MovementModel.Mass;

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _healthController.OnDeath -= OnDeath;
            _collidable.OnCollided -= OnCollided;
            _healthController.Dispose();
            _weapon.Dispose();
        }
    }
}