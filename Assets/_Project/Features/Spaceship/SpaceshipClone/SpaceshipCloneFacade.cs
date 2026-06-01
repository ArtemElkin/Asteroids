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
        private readonly IReadOnlyRotatable _mainSpaceshipRotatable;
        private readonly IDrawable _cloneView;
        private readonly ITimeService _timeService;


        public SpaceshipCloneFacade(
            ITimeService timeService,
            MovementModel cloneMovementModel,
            IReadOnlyPositionable mainSpaceshipPositionable,
            IReadOnlyRotatable mainSpaceshipRotatable,
            IDrawable cloneView,
            Vector2 cloneOffset)
        {
            _timeService = timeService;
            _cloneMovementModel = cloneMovementModel;
            _mainSpaceshipPositionable = mainSpaceshipPositionable;
            _mainSpaceshipRotatable = mainSpaceshipRotatable;
            _cloneView = cloneView;
            _cloneOffset = cloneOffset;
            
            _timeService.OnFixedTick += OnFixedTick;
        }

        private void OnFixedTick()
        {
            _cloneMovementModel.UpdatePosition(_mainSpaceshipPositionable.Position + _cloneOffset);
            _cloneMovementModel.UpdateRotationAngle(_mainSpaceshipRotatable.RotationAngle);
            _cloneView.Draw();
        }
        
        public IDrawable GetDrawable() => _cloneView;

        public void Dispose()
        {
            _timeService.OnFixedTick -= OnFixedTick;
        }
    }
}