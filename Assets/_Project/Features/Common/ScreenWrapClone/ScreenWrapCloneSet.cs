using System.Collections.Generic;
using _Project.Core.Factories;
using _Project.Core.Math;
using _Project.Core.Physics.Movement;
using _Project.Core.Render;
using _Project.Core.Services;
using _Project.Features.Common.Bounds;

namespace _Project.Features.Common.ScreenWrapClone
{
    public class ScreenWrapCloneSet<TOriginFacade> : IScreenWrapCloneSet
    {
        public IReadOnlyCollection<IDrawable> ClonesDrawables => _clonesDrawables;
        private readonly List<IDrawable> _clonesDrawables;
        private readonly MovementModel _originMovementModel;
        private readonly BoundsChecker _originBoundsChecker;
        private readonly IDrawable _originDrawable;
        private readonly IScreenWrapCloneOffsetCalculator _offsetCalculator;
        private readonly IScreenWrapCloneFactory<ScreenWrapCloneSpawnData, TOriginFacade> _factory;


        public ScreenWrapCloneSet(
            MovementModel originMovementModel,
            BoundsChecker originBoundsChecker,
            IDrawable originDrawable,
            IScreenWrapCloneOffsetCalculator offsetCalculator,
            IScreenWrapCloneFactory<ScreenWrapCloneSpawnData, TOriginFacade> factory)
        {
            _originMovementModel = originMovementModel;
            _originBoundsChecker = originBoundsChecker;
            _originDrawable = originDrawable;
            _offsetCalculator = offsetCalculator;
            _factory = factory;
            _clonesDrawables = new List<IDrawable>();
        }

        public void UpdateClones()
        {
            if (!_originBoundsChecker.IsEnteredGameAreaAfterSpawn) return;
            var offsets = _offsetCalculator.CalculateOffsets(_originMovementModel);
            if (_clonesDrawables.Count == 0) CreateClones();
            for (int i = 0; i < _clonesDrawables.Count; i++)
            {
                _clonesDrawables[i].Draw(_originMovementModel.Position + offsets[i], _originMovementModel.RotationAngle);
            }
        }

        public void CreateClones()
        {
            var offsets = _offsetCalculator.CalculateOffsets(_originMovementModel);
            foreach (var offset in offsets)
            {
                _clonesDrawables.Add(_factory.Create(new ScreenWrapCloneSpawnData(_originMovementModel, offset, _originDrawable)));
            }
        }

        public void Dispose()
        {
            foreach (var drawable in _clonesDrawables)
            {
                _factory.Release(drawable);
            }
        }
    }
}