using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Asteroid
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AsteroidComponent : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private AsteroidMovementController _movementController;


        private void FixedUpdate()
        {
            _movementController.UpdatePhysics(Time.fixedDeltaTime);
        }

        [Inject]
        private void Construct(
            AsteroidMovementController movementController)
        {
            _rb = GetComponent<Rigidbody2D>();
            _movementController = movementController;
        }

        public void Setup(Vector2 initialDirection, float initialSpeed)
        {
            _rb.position = transform.position;
            _movementController.Setup(_rb, transform.position, initialDirection, initialSpeed);
        }

        public void Reset()
        {
            _movementController.Reset();
        }
    }
}