using System;
using _Project.Core.Math;
using _Project.Core.Services;
using _Project.Core.Signals;

namespace _Project.Features.Gameplay.Bounds
{
    public class BoundsService
    {
        private float LeftBoundX => _screenService.LeftEdgeX;
        private float RightBoundX => _screenService.RightEdgeX;
        private float TopBoundY => _screenService.TopEdgeY;
        private float BottomBoundY => _screenService.BottomEdgeY;
        private readonly IScreenService _screenService;
        private readonly ISignalBus _signalBus;
        

        public BoundsService(IScreenService screenService) => _screenService = screenService;
        

        public bool IsOutOfBounds(Vector2 pos)
        {
            return pos.y > TopBoundY ||
                   pos.y < BottomBoundY ||
                   pos.x > RightBoundX ||
                   pos.x < LeftBoundX;
        }

        public bool TryGetCrossedBounds(Vector2 pos, out BoundType crossedBounds)
        {
            crossedBounds = BoundType.None;
            if (pos.y > TopBoundY) crossedBounds |= BoundType.Top;
            if (pos.y < BottomBoundY) crossedBounds |= BoundType.Bottom;
            if (pos.x > RightBoundX) crossedBounds |= BoundType.Right;
            if (pos.x < LeftBoundX) crossedBounds |= BoundType.Left;
            return crossedBounds != BoundType.None;
        }
    }
}