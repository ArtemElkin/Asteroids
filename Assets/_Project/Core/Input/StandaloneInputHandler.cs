using System;
using _Project.Core.Math;
using UnityEngine;


namespace _Project.Core.Input
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

        public CustomVector2 GetAxis()
        {
            return new  CustomVector2(GetHorizontalAxis(), GetVerticalAxis());
        }

        public CustomVector2 GetScreenPointerPosition()
        {
            return (Vector2)UnityEngine.Input.mousePosition;
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
