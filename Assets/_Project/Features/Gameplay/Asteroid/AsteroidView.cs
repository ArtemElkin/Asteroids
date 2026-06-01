using _Project.Core.Physics;
using _Project.Features.Gameplay.Common;
using _Project.Infrastructure.Tools;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Asteroid
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AsteroidView : MonoBehaviour, IDrawable
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
        }

        public void Reset()
        {
            _isSetup = false;
            _rb = null;
            _movementModel = null;
        }
    }
}