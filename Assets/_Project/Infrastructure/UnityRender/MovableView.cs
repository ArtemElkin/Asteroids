using _Project.Core.Physics;
using _Project.Features.Common;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.UnityRender
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovableView : MonoBehaviour, IDrawable
    {
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
            _rb.MovePosition(transform.position);
            _rb.MoveRotation(rotationAngle);
        }
        
        public void Draw(Vector2 position, float rotationAngle)
        {
            _rb.MovePosition(position.ToUnity());
            _rb.MoveRotation(rotationAngle);
        }

        public void Reset()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            _rb.position = transform.position;
            _rb.rotation = 0;
        }
    }
}