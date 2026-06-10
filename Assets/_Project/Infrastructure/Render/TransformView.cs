using _Project.Core.Render;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Render
{
    public class TransformView : MonoBehaviour, IDrawable
    {
        public void Setup(Vector2 initialPosition, float initialRotationAngle)
        {
            transform.position = initialPosition.ToUnity();
            transform.localRotation = Quaternion.Euler(0,0,initialRotationAngle);
        }

        public void Draw(Vector2 position, float rotationAngle)
        {
            transform.position = position.ToUnity();
            transform.localRotation = Quaternion.Euler(0,0,rotationAngle);
        }

        public void Reset()
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}