using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Spaceship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceshipComponent : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private SpaceshipMovementController _movementController;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        [Inject]
        private void Construct(SpaceshipMovementController movementController)
        {
            _movementController = movementController;
        }

        private void OnEnable()
        {
            _movementController.Setup(_rb);
        }

        private void OnDisable()
        {
            _movementController.Reset();
        }

    }
}