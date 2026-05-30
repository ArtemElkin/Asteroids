using _Project.Core.Physics;
using _Project.Infrastructure.Tools;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;


namespace _Project.Features.Gameplay.Spaceship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceshipCloneComponent : MonoBehaviour
    {
        public bool _isSetup;
        private Vector2 _cloneOffset;
        private Rigidbody2D _rb;
        private IReadOnlyRotatable _mainSpaceshipRotatable;
        private IReadOnlyPositionable _mainSpaceshipPositionable;


        private void FixedUpdate()
        {
            if (!_isSetup) return;
            
            _rb.MovePosition((_mainSpaceshipPositionable.Position + _cloneOffset).ToUnity());
            
            var rotation = Quaternion.Euler(0,0,_mainSpaceshipRotatable.RotationAngle);
            _rb.MoveRotation(rotation);
        }

        public void Setup(
            Vector2 offset,
            IReadOnlyPositionable mainSpaceshipPositionable,
            IReadOnlyRotatable mainSpaceshipRotatable)
        {
            _cloneOffset = offset;
            _mainSpaceshipPositionable = mainSpaceshipPositionable;
            _mainSpaceshipRotatable = mainSpaceshipRotatable;
            _rb = GetComponent<Rigidbody2D>();
            _isSetup = true;
        }

        public void Reset()
        {
            _isSetup = false;
            _rb = null;
            _mainSpaceshipRotatable = null;
            _mainSpaceshipPositionable = null;
            _cloneOffset = Vector2.zero;
        }
    }
}