using _Project.Core.Services;
using _Project.Infrastructure.Tools;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Services
{
    public class ScreenService : IScreenService
    {
        private readonly Camera _camera;

        
        public ScreenService(Camera camera) => _camera = camera;

        public float RightEdgeX => _camera.transform.position.x + (_camera.orthographicSize * _camera.aspect);
        
        public float LeftEdgeX => _camera.transform.position.x - (_camera.orthographicSize * _camera.aspect);
        
        public float TopEdgeY => _camera.transform.position.y + _camera.orthographicSize;
        
        public float BottomEdgeY => _camera.transform.position.y - _camera.orthographicSize;
        
        public float ScreenWidth => RightEdgeX - LeftEdgeX;
        
        public float ScreenHeight => TopEdgeY - BottomEdgeY;
        
        public Vector2 ScreenPointToWorldPoint(Vector2 point) => (_camera.ScreenToWorldPoint(point.ToUnity())).ToCore();
    }
}