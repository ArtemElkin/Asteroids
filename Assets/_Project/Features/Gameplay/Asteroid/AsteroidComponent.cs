using _Project.Core.Physics;
using _Project.Features.Gameplay.Bounds;
using _Project.Features.Gameplay.Common;
using _Project.Infrastructure.Tools;
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
        private BoundsChecker _boundsChecker;


        private void FixedUpdate()
        {
            if (!_isSetup) return;
            
            _movementController.Move(Time.fixedDeltaTime);
            _rb.MovePosition(_movementModel.Position.ToUnity());
            _boundsChecker.CheckOutOfBounds();
        }

        public void Setup(
            MovementModel movementModel,
            AsteroidMovementController movementController,
            BoundsChecker boundsChecker)
        {
            _movementModel = movementModel;
            _movementController = movementController;
            _boundsChecker = boundsChecker;
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