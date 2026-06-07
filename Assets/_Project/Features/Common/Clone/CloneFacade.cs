using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common.Bounds;
using _Project.Features.Common.Signals;

namespace _Project.Features.Common.Clone
{
    public class CloneFacade<TOriginFacade> : IFacade where TOriginFacade : IFacade
    {
        private readonly Vector2 _cloneOffset;
        private readonly MovementModel _cloneMovementModel;
        private readonly IReadOnlyPositionable _originPositionable;
        private readonly IReadOnlyRotationable _originRotationable;
        private readonly IDrawable _drawable;
        private readonly BoundsChecker _originBoundsChecker;
        private readonly ICollidable _collidable;
        private readonly ITimeService _timeService;
        private readonly ISignalBus _signalBus;


        public CloneFacade(
            ITimeService timeService,
            MovementModel cloneMovementModel,
            ICollidable collidable,
            IReadOnlyPositionable originPositionable,
            IReadOnlyRotationable originRotationable,
            BoundsChecker originBoundsChecker,
            IDrawable drawable,
            Vector2 cloneOffset,
            ISignalBus signalBus)
        {
            _originBoundsChecker = originBoundsChecker;
            _timeService = timeService;
            _collidable = collidable;
            _cloneMovementModel = cloneMovementModel;
            _originPositionable = originPositionable;
            _originRotationable = originRotationable;
            _drawable = drawable;
            _cloneOffset = cloneOffset;
            _signalBus = signalBus;
            
            _timeService.OnFixedTick += OnFixedTick;
            _collidable.OnCollided += OnCollided;
            _originBoundsChecker.EnteredGameAreaAfterSpawn += OnOriginEnteredGameAreaAfterSpawn;
        }

        private void OnOriginEnteredGameAreaAfterSpawn()
        {
            _drawable.Show();
        }

        private void OnFixedTick(float fixedDeltaTime)
        {
            _cloneMovementModel.UpdatePosition(_originPositionable.Position + _cloneOffset);
            _cloneMovementModel.UpdateRotationAngle(_originRotationable.RotationAngle);
            _originBoundsChecker.CheckOutOfBounds();
            _drawable.Draw();
        }

        private void OnCollided(Vector2 normal)
        {
            _signalBus.Fire(new CloneCollidedSignal<TOriginFacade>(normal));
        }
        
        public IDrawable GetDrawable() => _drawable;
        public IReadOnlyPositionable GetPositionable() => _cloneMovementModel;

        public IReadOnlyRotationable GetRotationable() => _cloneMovementModel;

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
            _collidable.OnCollided -= OnCollided;
        }
    }
}