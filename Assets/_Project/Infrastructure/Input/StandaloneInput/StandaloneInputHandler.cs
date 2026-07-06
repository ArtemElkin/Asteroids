using System;
using _Project.Core.Input;
using _Project.Core.Services;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Zenject;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Input.StandaloneInput
{
    public class StandaloneInputHandler : MonoBehaviour, IMovementInputService, IFireInputService, IPauseInputService
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";
        private IScreenService _screenService;

        public event Action OnPause;
        

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                OnPause?.Invoke();
            }
        }

        [Inject]
        private void Construct(IScreenService screenService)
        {
            _screenService = screenService;
        }

        public Vector2 GetAxis()
        {
            return new Vector2(GetHorizontalAxis(), GetVerticalAxis());
        }

        public bool FireState(int buttonId) => UnityEngine.Input.GetMouseButton(buttonId);

        public Vector2 GetAimDirection(Vector2 from)
        {
            var worldTarget = _screenService.ScreenPointToWorldPoint(UnityEngine.Input.mousePosition.ToCore());
            var direction = worldTarget - from;
            if (direction.sqrMagnitude < 0.001f) return Vector2.zero;

            return direction.normalized;
        }

        private float GetHorizontalAxis() => UnityEngine.Input.GetAxis(HorizontalAxisName);

        private float GetVerticalAxis() => UnityEngine.Input.GetAxis(VerticalAxisName);
    }
}
