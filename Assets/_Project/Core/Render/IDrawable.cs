using _Project.Core.Math;

namespace _Project.Core.Render
{
    public interface IDrawable
    {
        void Setup(Vector2 initialPosition, float initialRotationAngle);
        void Draw(Vector2 position, float rotationAngle);
        void Reset();
    }
}