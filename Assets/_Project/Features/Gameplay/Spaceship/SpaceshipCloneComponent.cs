using UnityEngine;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceshipCloneComponent : MonoBehaviour
    {
        private Vector2 _cloneOffset;
        private Rigidbody2D _rb;
        private SpaceshipMovementController _movementController;


        [Inject]
        private void Construct(SpaceshipMovementController movementController)
        {
            _movementController = movementController;
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Setup(Vector2 offset)
        {
            _cloneOffset = offset;
            _movementController.AddClone(_rb, _cloneOffset);
        }
    }
}