using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Infrastructure.Input.MobileInput
{
    public class TouchZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public event Action<Vector2> TouchDown;
        public event Action<Vector2> TouchUp;
        public event Action<Vector2> TouchMove;

        public void OnPointerDown(PointerEventData eventData)
        {
            TouchDown?.Invoke(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TouchUp?.Invoke(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            TouchMove?.Invoke(eventData.position);
        }
    }
}
