using _Project.Core.Tools;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Bounds
{
    public class BoundsService : IInitializable
    {
        private float _leftBoundX;
        private float _rightBoundX;
        private float _topBoundY;
        private float _bottomBoundY;
        private readonly ScreenBoundsService _screenBoundsService;
        

        public BoundsService(
            ScreenBoundsService screenBoundsService)
        {
            _screenBoundsService = screenBoundsService;
        }

        public void Initialize()
        {
            _leftBoundX = _screenBoundsService.LeftEdgeX;
            _rightBoundX = _screenBoundsService.RightEdgeX;
            _topBoundY = _screenBoundsService.TopEdgeY;
            _bottomBoundY = _screenBoundsService.BottomEdgeY;
        }

        public bool IsOutOfBounds(Vector2 pos)
        {
            return pos.y > _topBoundY ||
                   pos.y < _bottomBoundY ||
                   pos.x > _rightBoundX ||
                   pos.x < _leftBoundX;
        }

        public bool TryGetCrossedBounds(Vector2 pos, out BoundType crossedBounds)
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