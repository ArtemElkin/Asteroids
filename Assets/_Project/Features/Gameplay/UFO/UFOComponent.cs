using _Project.Core.Physics;
using _Project.Features.Gameplay.Common;
using _Project.Infrastructure.Tools;
using UnityEngine;

namespace _Project.Features.Gameplay.UFO
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UFOComponent : MonoBehaviour
    {
        private bool _isSetup;
        private bool _isTargetSetup;
        private Rigidbody2D _rb;
        private MovementModel _movementModel;
        private UFOMovementController _movementController;
        private UFORotationController _rotationController;
        private UFOTargetFollower _targetFollower;
        private BoundsChecker _boundsChecker;
        

        private void FixedUpdate()
        {
            if(!_isSetup) return;
            
            _targetFollower.UpdateTarget();
            
            _movementController.UpdatePhysics(Time.fixedDeltaTime);
            _rb.MovePosition(_movementModel.Position.ToUnity());
            
            _rotationController.UpdatePhysics();
            var rotation =Quaternion.Euler(0, 0, _movementModel.RotationAngle);
            _rb.MoveRotation(rotation);
            
            _boundsChecker.CheckOutOfBounds();
        }

        public void Setup(
            MovementModel movementModel,
            UFOMovementController movementController,
            UFORotationController rotationController,
            UFOTargetFollower targetFollower,
            BoundsChecker boundsChecker)
        {
            _movementModel = movementModel;
            _movementController = movementController;
            _rotationController = rotationController;
            _targetFollower = targetFollower;
            _boundsChecker = boundsChecker;
            _rb = GetComponent<Rigidbody2D>();
            _isSetup = true;
        }
        
        public void Reset()
        {
            _isSetup = false;
            _rb = null;
            _boundsChecker = null;
            _targetFollower = null;
            _rotationController = null;
            _movementController = null;
            _movementModel = null;
        }
    }
}