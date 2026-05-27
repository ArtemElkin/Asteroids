using UnityEngine;


namespace _Project.Core.Tools
{
    public class ScreenBoundsService
    {
        private readonly Camera _camera;
        
        
        public ScreenBoundsService(Camera camera) => _camera = camera;

        public float RightEdgeX => _camera.transform.position.x + (_camera.orthographicSize * _camera.aspect);
        
        public float LeftEdgeX => _camera.transform.position.x - (_camera.orthographicSize * _camera.aspect);
        
        public float TopEdgeY => _camera.transform.position.y + _camera.orthographicSize;
        
        public float BottomEdgeY => _camera.transform.position.y - _camera.orthographicSize;
        
        public float ScreenWidth => RightEdgeX - LeftEdgeX;
        
        public float ScreenHeight => TopEdgeY - BottomEdgeY;
    }
}