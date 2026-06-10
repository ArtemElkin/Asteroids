using _Project.Core.Render;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Render
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovableView : MonoBehaviour, IDrawable
    {
        private bool _isSetup;
        private Rigidbody2D _rb;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            _rb.position = transform.position;
            _rb.MoveRotation(transform.localRotation);
        }

        public void Setup(Vector2 position, float rotationAngle)
        {
            transform.position = position.ToUnity();
            _rb.position = transform.position;
            _rb.rotation = rotationAngle;
            _isSetup = true;
        }
        
        public void Draw(Vector2 position, float rotationAngle)
        {
            if (!_isSetup) return;
            _rb.MovePosition(position.ToUnity());
            _rb.MoveRotation(rotationAngle);
        }

        public void Reset()
        {
            _isSetup = false;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            _rb.position = transform.position;
            _rb.rotation = 0;
        }
    }
}