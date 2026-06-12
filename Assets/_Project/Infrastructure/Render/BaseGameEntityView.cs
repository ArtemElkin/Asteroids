using _Project.Core.Render;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Render
{
    public abstract class BaseGameEntityView : MonoBehaviour, IDrawable
    {
        public abstract void Setup(Vector2 initialPosition, float initialRotationAngle);

        public abstract void Draw(Vector2 position, float rotationAngle);

        public abstract void Reset();
    }
}