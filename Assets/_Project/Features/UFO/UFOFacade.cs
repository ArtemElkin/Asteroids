using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Signals;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Features.UFO
{
    public class UFOFacade : IFacade
    {
        public MovementModel MovementModel { get; }
        private readonly IMovable _movable;
        private readonly IRotatable _rotatable;
        private readonly UFOTargetFollower _targetFollower;
        private readonly BoundsChecker _boundsChecker;
        private readonly BoundsWarper _boundsWarper;
        private readonly IDrawable _drawable;
        private readonly ICollidable _collidable;
        private readonly IHitable _hitable;
        private readonly ITimeService _timeService;
        private readonly ISignalBus _signalBus;


        public UFOFacade(
            MovementModel movementModel,
            IMovable movable,
            IRotatable rotatable,
            UFOTargetFollower targetFollower,
            BoundsChecker boundsChecker,
            BoundsWarper boundsWarper,
            IDrawable drawable,
            ICollidable collidable,
            IHitable hitable,
            ITimeService timeService,
            ISignalBus signalBus)
        {
            MovementModel = movementModel;
            _movable = movable;
            _rotatable = rotatable;
            _targetFollower = targetFollower;
            _boundsChecker = boundsChecker;
            _boundsWarper = boundsWarper;
            _drawable = drawable;
            _collidable = collidable;
            _hitable = hitable;
            _timeService = timeService;
            _signalBus = signalBus;
            
            _timeService.OnFixedTick += OnFixedTick;
            _collidable.OnCollided += OnCollided;
            _hitable.OnHit += Destruct;
            _boundsChecker.OutOfBounds += OnOutOfBounds;
        }

        private void OnFixedTick(float fixedDeltaTime)
        {
            _targetFollower.UpdateTarget();
            _movable.Move(fixedDeltaTime);
            _rotatable.Rotate();
            _boundsChecker.CheckOutOfBounds();
            _drawable.Draw(MovementModel.Position, MovementModel.RotationAngle);
        }

        private void OnCollided(ICollidable other, Vector2 collisionNormal)
        {
            var collisionData = new CollisionData(MovementModel, other.MovementModel, collisionNormal);
            _signalBus.Fire(new CollisionDetectedSignal(collisionData));
        }

        private void OnOutOfBounds()
        {
            _boundsWarper.Warp(MovementModel);
        }

        private void Destruct()
        {
            _signalBus.Fire(new DespawnRequestedSignal<UFOFacade>(this));
        }
        
        public IDrawable GetDrawable() => _drawable;

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _collidable.OnCollided -= OnCollided;
            _hitable.OnHit -= Destruct;
        }
    }
}