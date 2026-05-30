using _Project.Core.Physics;

namespace _Project.Features.Gameplay.Bounds
{
    public class BoundsChecker
    {
        private bool _isSetup;
        private bool _isEnteredGameAreaAfterSpawn;
        private IWarpable _warpable;
        private IReadOnlyPositionable _positionable;
        private readonly BoundsService _boundsService;
        private readonly BoundsWarper _boundsWarper;


        public BoundsChecker(
            BoundsService boundsService,
            BoundsWarper boundsWarper)
        {
            _boundsService = boundsService;
            _boundsWarper = boundsWarper;
        }

        public void Setup(IReadOnlyPositionable  positionable, IWarpable warpable)
        {
            _positionable = positionable;
            _warpable = warpable;
            _isSetup = true;
        }

        public void CheckOutOfBounds()
        {
            if (!_isSetup) return;
            
            if (_boundsService.IsOutOfBounds(_positionable.Position) && _isEnteredGameAreaAfterSpawn)
            {
                _boundsWarper.Warp(_warpable, _positionable.Position);
            }
            else if (!_isEnteredGameAreaAfterSpawn && !_boundsService.IsOutOfBounds(_positionable.Position))
            {
                _isEnteredGameAreaAfterSpawn = true;
            }
        }

        public void Reset()
        {
            _isSetup = false;
            _positionable = null;
            _warpable = null;
        }
    }
}