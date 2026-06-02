using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Signals;

namespace _Project.Features.UFO
{
    public class UFOFacade : IFacade
    {
        private readonly UFOMovementController _movementController;
        private readonly UFORotationController _rotationController;
        private readonly UFOTargetFollower _targetFollower;
        private readonly BoundsChecker _boundsChecker;
        private readonly IDrawable _view;
        private readonly ICollidable _collidable;
        private readonly IHitable _hitable;
        private readonly ITimeService _timeService;
        private readonly ISignalBus _signalBus;


        public UFOFacade(
            UFOMovementController movementController,
            UFORotationController rotationController,
            UFOTargetFollower targetFollower,
            BoundsChecker boundsChecker,
            IDrawable view,
            ICollidable collidable,
            IHitable hitable,
            ITimeService timeService,
            ISignalBus signalBus)
        {
            _movementController = movementController;
            _rotationController = rotationController;
            _targetFollower = targetFollower;
            _boundsChecker = boundsChecker;
            _view = view;
            _collidable = collidable;
            _hitable = hitable;
            _timeService = timeService;
            _signalBus = signalBus;
            
            _timeService.OnFixedTick += OnFixedTick;
            _collidable.OnCollided += OnCollided;
            _hitable.OnHit += Destruct;
        }

        private void OnFixedTick()
        {
            _targetFollower.UpdateTarget();
            _movementController.Move(_timeService.FixedDeltaTime);
            _rotationController.Rotate();
            _boundsChecker.CheckOutOfBounds();
            _view.Draw();
        }

        private void OnCollided(Vector2 normal)
        {
            _movementController.Bounce(normal);
        }

        private void Destruct()
        {
            _signalBus.Fire(new DespawnRequestedSignal<UFOFacade>(this));
        }
        
        public IDrawable GetDrawable() => _view;

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _collidable.OnCollided -= OnCollided;
            _hitable.OnHit -= Destruct;
        }
    }
}