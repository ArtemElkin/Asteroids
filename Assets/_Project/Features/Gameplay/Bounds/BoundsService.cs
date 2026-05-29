using _Project.Core.Math;
using _Project.Core.Tools;
using Zenject;


namespace _Project.Features.Gameplay.Bounds
{
    public class BoundsService : IInitializable
    {
        private float _leftBoundX;
        private float _rightBoundX;
        private float _topBoundY;
        private float _bottomBoundY;
        private readonly ScreenService _screenService;
        

        public BoundsService(
            ScreenService screenService)
        {
            _screenService = screenService;
        }

        public void Initialize()
        {
            _leftBoundX = _screenService.LeftEdgeX;
            _rightBoundX = _screenService.RightEdgeX;
            _topBoundY = _screenService.TopEdgeY;
            _bottomBoundY = _screenService.BottomEdgeY;
        }

        public bool IsOutOfBounds(CustomVector2 pos)
        {
            return pos.y > _topBoundY ||
                   pos.y < _bottomBoundY ||
                   pos.x > _rightBoundX ||
                   pos.x < _leftBoundX;
        }

        public bool TryGetCrossedBounds(CustomVector2 pos, out BoundType crossedBounds)
        {
            crossedBounds = BoundType.None;
            if (pos.y > _topBoundY) crossedBounds |= BoundType.Top;
            if (pos.y < _bottomBoundY) crossedBounds |= BoundType.Bottom;
            if (pos.x > _rightBoundX) crossedBounds |= BoundType.Right;
            if (pos.x < _leftBoundX) crossedBounds |= BoundType.Left;
            return crossedBounds != BoundType.None;
        }
    }
}