using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Infrastructure.Input.MobileInput
{
    public class InputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsPressed { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsPressed = false;
        }
    }
}