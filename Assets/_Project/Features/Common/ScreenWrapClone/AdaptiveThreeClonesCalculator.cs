using _Project.Core.Math;
using _Project.Core.Physics.Movement;
using _Project.Core.Services;
using _Project.Features.Common.Bounds;

namespace _Project.Features.Common.ScreenWrapClone
{
    public class AdaptiveThreeClonesCalculator : IScreenWrapCloneOffsetCalculator
    {
        private readonly BoundsService _boundsService;
        private readonly IScreenService _screenService;
        
        
        public AdaptiveThreeClonesCalculator(
            BoundsService boundsService,
            IScreenService screenService)
        {
            _boundsService = boundsService;
            _screenService = screenService;
        }
        
        public Vector2[] CalculateOffsets(MovementModel originMovementModel)
        {
            Vector2[] offsets = new Vector2[3];
            var sides = _boundsService.GetSides(originMovementModel.Position);
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
    }
}