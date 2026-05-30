using System;
using _Project.Core.Input;
using _Project.Infrastructure.Tools;
using UnityEngine;
using Vector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.Input
{
    public class StandaloneInputHandler : MonoBehaviour, IMovementInputService, IFireInputService
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";
        public event Action<bool> FireStateChanged;
        
        
        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                FireStateChanged?.Invoke(true);
            else if (UnityEngine.Input.GetMouseButtonUp(0))
                FireStateChanged?.Invoke(false);
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
