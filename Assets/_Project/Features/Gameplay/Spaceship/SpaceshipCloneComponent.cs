using _Project.Core.Math;
using _Project.Core.Physics;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceshipCloneComponent : MonoBehaviour
    {
        public bool _isSetup;
        private CustomVector2 _cloneOffset;
        private Rigidbody2D _rb;
        private IReadOnlyRotatable _mainSpaceshipRotatableModel;
        private IReadOnlyPositionable _mainSpaceshipPositionableModel;


        private void FixedUpdate()
        {
            if (!_isSetup) return;
            
            _rb.MovePosition(_mainSpaceshipPositionableModel.Position + _cloneOffset);
            
            var rotation = Quaternion.Euler(0,0,_mainSpaceshipRotatableModel.RotationAngle);
            _rb.MoveRotation(rotation);
        }

        [Inject]
        private void Construct()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Setup(
            CustomVector2 offset,
            IReadOnlyPositionable mainSpaceshipPositionableModel,
            IReadOnlyRotatable mainSpaceshipRotatableModel)
        {
            _cloneOffset = offset;
            _mainSpaceshipPositionableModel = mainSpaceshipPositionableModel;
            _mainSpaceshipRotatableModel = mainSpaceshipRotatableModel;
            _isSetup = true;
        }

        public void Reset()
        {
            _isSetup = false;
            _cloneOffset = CustomVector2.zero;
            _mainSpaceshipPositionableModel = null;
            _mainSpaceshipRotatableModel = null;
        }
    }
}