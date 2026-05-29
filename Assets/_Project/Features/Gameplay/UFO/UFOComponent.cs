using _Project.Core.Physics;
using _Project.Features.Gameplay.Signals;
using _Project.Features.Gameplay.Spaceship;
using UnityEngine;
using Zenject;


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
        

        private void FixedUpdate()
        {
            if(!_isSetup) return;
            
            _targetFollower.UpdateTarget();
            _movementController.UpdatePhysics(Time.fixedDeltaTime);
            _rb.MovePosition(_movementModel.Position);
            
            _rotationController.UpdatePhysics();
            var rotation =Quaternion.Euler(0, 0, _movementModel.RotationAngle);
            _rb.MoveRotation(rotation);
        }

        public void Setup(
            MovementModel movementModel,
            UFOMovementController movementController,
            UFORotationController rotationController,
            UFOTargetFollower targetFollower)
        {
            _movementModel = movementModel;
            _movementController = movementController;
            _rotationController = rotationController;
            _targetFollower = targetFollower;
            _rb = GetComponent<Rigidbody2D>();
        }
        
        public void Reset()
        {
            _isSetup = false;
            _rb = null;
            _targetFollower = null;
            _rotationController = null;
            _movementController = null;
            _movementModel = null;
        }
    }
}