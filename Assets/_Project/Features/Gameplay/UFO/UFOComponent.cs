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
        private SignalBus _signalBus;


        private void OnEnable()
        {
            _signalBus.Subscribe<SpawnedSignal<SpaceshipComponent>>(OnSpaceshipSpawned);
        }

        private void FixedUpdate()
        {
            if(!_isSetup) return;
            if (_isTargetSetup)
            {
                _targetFollower.UpdateTarget();
            }
            _movementController.UpdatePhysics(Time.fixedDeltaTime);
            _rb.MovePosition(_movementModel.Position);
            
            _rotationController.UpdatePhysics();
            var rotation =Quaternion.Euler(0, 0, _movementModel.RotationAngle);
            _rb.MoveRotation(rotation);
        }

        [Inject]
        private void Construct(
            MovementModel movementModel,
            UFOMovementController movementController,
            UFORotationController rotationController,
            UFOTargetFollower targetFollower,
            SignalBus signalBus)
        {
            _movementModel = movementModel;
            _movementController = movementController;
            _rotationController = rotationController;
            _targetFollower = targetFollower;
            _signalBus =  signalBus;
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Setup(float initialSpeed)
        {
            _movementModel.Init((Vector2)transform.position, initialSpeed);
            _movementController.Setup(_movementModel);
            _rotationController.Setup(_movementModel);
            _isSetup = true;
        }
        
        private void OnSpaceshipSpawned(SpawnedSignal<SpaceshipComponent> signal)
        {
            IReadOnlyPositionable targetPositionable = signal.spawnedObj.GetPositionable();
            _targetFollower.Setup(_movementModel, targetPositionable);
            _isTargetSetup = true;
        }
        
        public void Reset()
        {
            _isSetup = false;
            _isTargetSetup = false;
            _movementController.Reset();
            _rotationController.Reset();
            _targetFollower.Reset();
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<SpawnedSignal<SpaceshipComponent>>(OnSpaceshipSpawned);
        }
    }
}