using System.Collections.Generic;
using _Project.Core.Factories;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Render;
using _Project.Core.Services;
using _Project.Features.Common.Bounds;

namespace _Project.Features.Common.ScreenWrapClone
{
    public class ScreenWrapCloneSet<TOriginFacade> : IScreenWrapCloneSet
    {
        private readonly List<IDrawable> _clonesDrawables;
        private readonly MovementModel _originMovementModel;
        private readonly BoundsChecker _originBoundsChecker;
        private readonly IDrawable _originDrawable;
        private readonly IScreenWrapCloneFactory<ScreenWrapCloneSpawnData, TOriginFacade> _factory;
        private readonly BoundsService _boundsService;
        private readonly IScreenService _screenService;


        public ScreenWrapCloneSet(
            MovementModel originMovementModel,
            BoundsChecker originBoundsChecker,
            IDrawable originDrawable,
            IScreenWrapCloneFactory<ScreenWrapCloneSpawnData, TOriginFacade> factory,
            BoundsService boundsService,
            IScreenService screenService)
        {
            _originMovementModel = originMovementModel;
            _originBoundsChecker = originBoundsChecker;
            _originDrawable = originDrawable;
            _factory = factory;
            _boundsService = boundsService;
            _screenService = screenService;
            _clonesDrawables = new List<IDrawable>();
        }

        public void UpdateClones()
        {
            if (!_originBoundsChecker.IsEnteredGameAreaAfterSpawn) return;
            var offsets = CalculateOffsets();
            if (_clonesDrawables.Count == 0) CreateClones(offsets);
            for (int i = 0; i < _clonesDrawables.Count; i++)
            {
                _clonesDrawables[i].Draw(_originMovementModel.Position + offsets[i], _originMovementModel.RotationAngle);
            }
        }

        private void CreateClones(Vector2[] offsets)
        {
            _clonesDrawables.Add(_factory.Create(new ScreenWrapCloneSpawnData(_originMovementModel, offsets[0], _originDrawable)));
            _clonesDrawables.Add(_factory.Create(new ScreenWrapCloneSpawnData(_originMovementModel, offsets[1], _originDrawable)));
            _clonesDrawables.Add(_factory.Create(new ScreenWrapCloneSpawnData(_originMovementModel, offsets[2], _originDrawable)));
        }

        private Vector2[] CalculateOffsets()
        {
            Vector2[] offsets = new Vector2[3];
            var sides = _boundsService.GetSides(_originMovementModel.Position);
            var width = _screenService.ScreenWidth;
            var height = _screenService.ScreenHeight;
            var oppositeSides = ~sides & BoundType.All;
            float x = 0;
            float y = 0;
            x = ((oppositeSides & BoundType.Left) != 0) ? -width :  width;
            y = ((oppositeSides & BoundType.Top) != 0) ? height :  -height;
            offsets[0] = new Vector2(x, y);
            offsets[1] = new Vector2(x, 0);
            offsets[2] = new Vector2(0, y);
            return offsets;
        }

        public void Dispose()
        {
            foreach (var clone in _clonesDrawables)
            {
                _factory.Release(clone);
            }
            _clonesDrawables.Clear();
        }
    }
}