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

        [Inject]
        private void Construct(
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
        }

        public void Setup(float maxSpeed)
        {
            _movementModel.Init((Vector2)transform.position, 0);
            _movementController.Setup(_movementModel, maxSpeed);
            _rotationController.Setup(_movementModel);
            _boundsChecker.Setup(_movementModel, _movementController);
            _isSetup = true;
        }

        public IReadOnlyPositionable GetPositionable() => _movementModel;
        public IReadOnlyRotatable GetRotatable() => _movementModel;

        public void Reset()
        {
            _isSetup = false;
            _movementController.Reset();
            _rotationController.Reset();
            _boundsChecker.Reset();
        }
    }
}