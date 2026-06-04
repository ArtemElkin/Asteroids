using System;
using _Project.Core.Physics;

namespace _Project.Features.Common.Bounds
{
    public class BoundsChecker
    {
        public event Action OutOfBounds;
        private bool _isEnteredGameAreaAfterSpawn;
        private readonly IWarpable _warpable;
        private readonly IReadOnlyPositionable _positionable;
        private readonly BoundsService _boundsService;
        private readonly BoundsWarper _boundsWarper;


        public BoundsChecker(
            BoundsService boundsService,
            BoundsWarper boundsWarper,
            IReadOnlyPositionable positionable,
            IWarpable warpable)
        {
            _boundsService = boundsService;
            _boundsWarper = boundsWarper;
            _positionable = positionable;
            _warpable = warpable;
        }

        public void CheckOutOfBounds()
        {
            if (_boundsService.IsOutOfBounds(_positionable.Position) && _isEnteredGameAreaAfterSpawn)
            {
                _boundsWarper.Warp(_warpable, _positionable.Position);
                OutOfBounds?.Invoke();
            }
            else if (!_isEnteredGameAreaAfterSpawn && !_boundsService.IsOutOfBounds(_positionable.Position))
            {
                _isEnteredGameAreaAfterSpawn = true;
            }
        }
    }
}