using _Project.Core.Physics;
using _Project.Features.Common;
using _Project.Infrastructure.UnityServices;
using UnityEngine;

namespace _Project.Infrastructure.UnityRender
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovableView : MonoBehaviour, IDrawable
    {
        private bool _isSetup;
        private Rigidbody2D _rb;
        private MovementModel _movementModel;

        
        public void Setup(MovementModel movementModel)
        {
            _movementModel = movementModel;
            _rb = GetComponent<Rigidbody2D>();
            transform.position = movementModel.Position.ToUnity();
            _rb.position = transform.position;
            _isSetup = true;
        }

        public void Draw()
        {
            if (!_isSetup) return;
            
            _rb.MovePosition(_movementModel.Position.ToUnity());
            var rotation = Quaternion.Euler(0, 0, _movementModel.RotationAngle);
            _rb.MoveRotation(rotation);
        }

        public void Reset()
        {
            _isSetup = false;
            _rb = null;
            _movementModel = null;
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() =>  gameObject.SetActive(false);
    }
}