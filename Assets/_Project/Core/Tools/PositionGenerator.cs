using _Project.Core.Math;

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

        public CustomVector2 GenerateRandomPositionOnScreen()
        {
            var min = new CustomVector2(_screenService.LeftEdgeX, _screenService.BottomEdgeY);
            var max = new CustomVector2(_screenService.RightEdgeX, _screenService.TopEdgeY);

            return GenerateRandomPosition(min, max);
        }

        public CustomVector2 GenerateRandomPositionOutOfScreen(float offset)
        {
            var min = new CustomVector2(_screenService.LeftEdgeX - offset, _screenService.BottomEdgeY - offset);
            var max = new CustomVector2(_screenService.RightEdgeX + offset, _screenService.TopEdgeY + offset);
            return GenerateRandomPositionOnRectangle(min, max);
        }

        private CustomVector2 GenerateRandomPositionOnRectangle(CustomVector2 min, CustomVector2 max)
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
        
        private CustomVector2 GenerateRandomPosition(CustomVector2 minPos, CustomVector2 maxPos)
        {
            float newPosX = _randomService.GetRandomFloat(min: minPos.x, max: maxPos.x);
            float newPosY = _randomService.GetRandomFloat(min: minPos.y, max: maxPos.y);
            return new CustomVector2(newPosX, newPosY);
        }
    }
}