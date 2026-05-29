using _Project.Core.Physics;
using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Asteroid
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AsteroidComponent : MonoBehaviour
    {
        private bool _isSetup;
        private Rigidbody2D _rb;
        private MovementModel _movementModel;
        private AsteroidMovementController _movementController;


        private void FixedUpdate()
        {
            if (!_isSetup) return;
            
            _movementController.UpdatePhysics(Time.fixedDeltaTime);
            _rb.MovePosition(_movementModel.Position);
        }

        [Inject]
        private void Construct(
            MovementModel movementModel,
            AsteroidMovementController movementController)
        {
            _movementModel = movementModel;
            _movementController = movementController;
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Setup(Vector2 initialDirection, float initialSpeed)
        {
            _movementModel.Init((Vector2)transform.position, initialSpeed);
            _movementModel.UpdateMoveDirection(initialDirection);
            _rb.position = transform.position;
            _movementController.Setup(_movementModel);
            _isSetup = true;
        }

        public void Reset()
        {
            _isSetup = false;
            _movementController.Reset();
        }
    }
}