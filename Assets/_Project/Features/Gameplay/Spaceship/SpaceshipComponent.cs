using _Project.Core.Physics;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceshipComponent : MonoBehaviour
    {
        private bool _isSetup;
        private Rigidbody2D _rb;
        private MovementModel _movementModel;
        private SpaceshipMovementController _movementController;
        private SpaceshipRotationController _rotationController;
        private BoundsChecker _boundsChecker;


        private void FixedUpdate()
        {
            if (!_isSetup) return;
            
            _movementController.UpdatePhysics(Time.fixedDeltaTime);
            _rb.MovePosition(_movementModel.Position);
            
            _rotationController.UpdatePhysics(Time.fixedDeltaTime);
            var rotation = Quaternion.Euler(0, 0, _movementModel.RotationAngle);
            _rb.MoveRotation(rotation);
            
            _boundsChecker.CheckOutOfBounds();
        }

        public void Setup(
            MovementModel movementModel,
            SpaceshipMovementController movementController,
            SpaceshipRotationController rotationController,
            BoundsChecker boundsChecker)
        {
            _movementModel = movementModel;
            _movementController = movementController;
            _rotationController = rotationController;
            _boundsChecker = boundsChecker;
            _rb = GetComponent<Rigidbody2D>();
            _rb.position = transform.position;
            _isSetup = true;
        }

        public IReadOnlyPositionable GetPositionable() => _movementModel;
        public IReadOnlyRotatable GetRotatable() => _movementModel;

        public void Reset()
        {
            _isSetup = false;
            _rb = null;
            _boundsChecker = null;
            _rotationController = null;
            _movementController = null;
            _movementModel = null;
        }
    }
}