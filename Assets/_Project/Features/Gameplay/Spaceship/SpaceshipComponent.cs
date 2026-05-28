using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceshipComponent : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private SpaceshipMovementController _movementController;
        private SpaceshipRotationController _rotationController;


        [Inject]
        private void Construct(
            SpaceshipMovementController movementController,
            SpaceshipRotationController rotationController)
        {
            _movementController = movementController;
            _rotationController = rotationController;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _rb.position = transform.position;
            _movementController.Setup(_rb, transform.position);
            _rotationController.Setup(_rb);
        }

        private void OnDisable()
        {
            _movementController.Reset();
        }
    }
}