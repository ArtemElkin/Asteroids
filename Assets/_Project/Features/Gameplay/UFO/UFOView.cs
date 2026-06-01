using _Project.Core.Physics;
using _Project.Features.Gameplay.Bounds;
using _Project.Features.Gameplay.Common;
using _Project.Infrastructure.Tools;
using UnityEngine;

namespace _Project.Features.Gameplay.UFO
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UFOView : MonoBehaviour, IDrawable
    {
        private bool _isSetup;
        private Rigidbody2D _rb;
        private MovementModel _movementModel;
        

        public void Draw()
        {
            if(!_isSetup) return;
            
            _rb.MovePosition(_movementModel.Position.ToUnity());
            var rotation =Quaternion.Euler(0, 0, _movementModel.RotationAngle);
            _rb.MoveRotation(rotation);
        }

        public void Setup(MovementModel movementModel)
        {
            _movementModel = movementModel;
            _rb = GetComponent<Rigidbody2D>();
            _isSetup = true;
        }
        
        public void Reset()
        {
            _isSetup = false;
            _rb = null;
            _movementModel = null;
        }
    }
}