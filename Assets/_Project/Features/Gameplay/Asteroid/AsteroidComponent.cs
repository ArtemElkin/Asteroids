using _Project.Core.Physics;
using UnityEngine;


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

        public void Setup(
            MovementModel movementModel,
            AsteroidMovementController movementController)
        {
            _movementModel = movementModel;
            _movementController = movementController;
            _rb = GetComponent<Rigidbody2D>();
            _rb.position = transform.position;
            _isSetup = true;
        }

        public void Reset()
        {
            _isSetup = false;
            _rb = null;
            _movementController = null;
            _movementModel = null;
        }
    }
}