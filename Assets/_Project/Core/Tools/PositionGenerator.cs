using UnityEngine;


namespace _Project.Core.Tools
{
    public class PositionGenerator
    {
        private readonly RandomService _randomService;
        private readonly ScreenService _screenService;

        
        public PositionGenerator(
            ScreenService screenService,
            RandomService randomService)
        {
            _randomService = randomService;
            _screenService = screenService;
        }

        public Vector2 GenerateRandomPositionOnScreen()
        {
            var min = new Vector2(_screenService.LeftEdgeX, _screenService.BottomEdgeY);
            var max = new Vector2(_screenService.RightEdgeX, _screenService.TopEdgeY);

            return GenerateRandomPosition(min, max);
        }

        public Vector2 GenerateRandomPositionOutOfScreen(float offset)
        {
            var min = new Vector2(_screenService.LeftEdgeX - offset, _screenService.BottomEdgeY - offset);
            var max = new Vector2(_screenService.RightEdgeX + offset, _screenService.TopEdgeY + offset);
            return GenerateRandomPositionOnRectangle(min, max);
        }

        private Vector2 GenerateRandomPositionOnRectangle(Vector2 min, Vector2 max)
        {
            var pos = GenerateRandomPosition(min, max);
            int side = _randomService.GetRandomNonNegativeInt(4);
            switch (side)
            {
                case 0:
                    pos.x = min.x;
                    break;
                case 1:
                    pos.y = max.y;
                    break;
                case 2:
                    pos.x = max.x;
                    break;
                case 3:
                    pos.y = min.y;
                    break;
            }
            return pos;
        }
        
        private Vector2 GenerateRandomPosition(Vector2 minPos, Vector2 maxPos)
        {
            float newPosX = _randomService.GetRandomFloat(min: minPos.x, max: maxPos.x);
            float newPosY = _randomService.GetRandomFloat(min: minPos.y, max: maxPos.y);
            return new Vector2(newPosX, newPosY);
        }
    }
}