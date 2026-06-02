using System;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Features.Common;

namespace _Project.Features.Spaceship.SpaceshipClone
{
    public class SpaceshipCloneFacade : IFacade
    {
        private readonly Vector2 _cloneOffset;
        private readonly MovementModel _cloneMovementModel;
        private readonly IReadOnlyPositionable _mainSpaceshipPositionable;
        private readonly IReadOnlyRotationable _mainSpaceshipRotationable;
        private readonly IDrawable _cloneView;
        private readonly ITimeService _timeService;


        public SpaceshipCloneFacade(
            ITimeService timeService,
            MovementModel cloneMovementModel,
            IReadOnlyPositionable mainSpaceshipPositionable,
            IReadOnlyRotationable mainSpaceshipRotationable,
            IDrawable cloneView,
            Vector2 cloneOffset)
        {
            _timeService = timeService;
            _cloneMovementModel = cloneMovementModel;
            _mainSpaceshipPositionable = mainSpaceshipPositionable;
            _mainSpaceshipRotationable = mainSpaceshipRotationable;
            _cloneView = cloneView;
            _cloneOffset = cloneOffset;
            
            _timeService.OnFixedTick += OnFixedTick;
        }

        private void OnFixedTick()
        {
            _cloneMovementModel.UpdatePosition(_mainSpaceshipPositionable.Position + _cloneOffset);
            _cloneMovementModel.UpdateRotationAngle(_mainSpaceshipRotationable.RotationAngle);
            _cloneView.Draw();
        }
        
        public IDrawable GetDrawable() => _cloneView;

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
        }
    }
}