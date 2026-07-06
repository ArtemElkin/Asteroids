using System;
using _Project.Core.Input;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Input.MobileInput
{
    public class MobileInputHandler : MonoBehaviour, IMovementInputService, IFireInputService, IPauseInputService
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private InputButton _fireProjectileButton;
        [SerializeField] private InputButton _fireLaserButton;
        [SerializeField] private Joystick _movementJoystick;
        [SerializeField] private Joystick _lookJoystick;
        private Vector2 _lastLookDirection;

        public event Action OnPause;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
        }

        private void Update()
        {
            if (!_lookJoystick.IsPressed) return;

            var direction = _lookJoystick.Direction;
            if (direction.sqrMagnitude < 0.001f) return;

            _lastLookDirection = new Vector2(direction.x, direction.y).normalized;
        }

        public Vector2 GetAxis()
        {
            var direction = _movementJoystick.Direction;
            return new Vector2(direction.x, direction.y);
        }

        public bool FireState(int buttonId) =>
            buttonId == 0
                ? _fireProjectileButton.IsPressed
                : _fireLaserButton.IsPressed;

        public Vector2 GetAimDirection(Vector2 from) => _lastLookDirection;

        private void OnPauseButtonClicked()
        {
            OnPause?.Invoke();
        }
    }
}
