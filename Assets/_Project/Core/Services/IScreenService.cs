using _Project.Core.Math;

namespace _Project.Core.Services
{
    public interface IScreenService
    {
        float RightEdgeX { get; }
        
        float LeftEdgeX { get; }
        
        float TopEdgeY { get; }
        
        float BottomEdgeY { get; }
        
        float ScreenWidth { get; }
        
        float ScreenHeight { get; }

        Vector2 ScreenPointToWorldPoint(Vector2 point);
    }
}