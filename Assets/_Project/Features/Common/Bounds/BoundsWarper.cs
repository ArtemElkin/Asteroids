using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;

namespace _Project.Features.Common.Bounds
{
    public class BoundsWarper
    {
        private readonly IScreenService _screenService;
        private readonly BoundsService _boundsService;


        public BoundsWarper(
            IScreenService screenService,
            BoundsService boundsService)
        {
            _screenService = screenService;
            _boundsService = boundsService;
        }

        public void Warp(IPositionable positionable)
        {
            var oldPos = positionable.Position;
            if (_boundsService.TryGetCrossedBounds(oldPos, out var crossedBounds))
            {
                var newPos = oldPos;
                if ((crossedBounds & BoundType.Top) != 0) newPos.y = _screenService.BottomEdgeY;
                if ((crossedBounds & BoundType.Bottom) != 0) newPos.y = _screenService.TopEdgeY;
                if ((crossedBounds & BoundType.Left) != 0) newPos.x = _screenService.RightEdgeX;
                if((crossedBounds & BoundType.Right) != 0) newPos.x = _screenService.LeftEdgeX;
                positionable.UpdatePosition(newPos);
            }
        }
    }
}