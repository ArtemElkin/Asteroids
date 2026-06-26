using System;
using _Project.Core.Input;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Input
{
    public class StandaloneInputHandler : MonoBehaviour, IMovementInputService, IFireInputService, IPauseInputService
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";
        public bool FireState(int buttonId) => UnityEngine.Input.GetMouseButton(buttonId);
        public event Action OnPause;



        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                OnPause?.Invoke();
            }
        }

        public Vector2 GetAxis()
        {
            return new  Vector2(GetHorizontalAxis(), GetVerticalAxis());
        }

        public Vector2 GetScreenPointerPosition()
        {
            return UnityEngine.Input.mousePosition.ToCore();
        }

        private float GetHorizontalAxis()
        {
            return UnityEngine.Input.GetAxis(HorizontalAxisName);
        }

        private float GetVerticalAxis()
        {
            return UnityEngine.Input.GetAxis(VerticalAxisName);
        }
    }
}
