using System;
using _Project.Core.Tools;
using UnityEngine;
using Zenject;


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

        public float GetHorizontalAxis()
        {
            return UnityEngine.Input.GetAxis(HorizontalAxisName);
        }

        public float GetVerticalAxis()
        {
            return UnityEngine.Input.GetAxis(VerticalAxisName);
        }

        public Vector2 GetScreenPointerPosition()
        {
            return UnityEngine.Input.mousePosition;
        }
    }
}
