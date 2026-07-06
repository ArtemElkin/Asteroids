using UnityEngine;

namespace _Project.Infrastructure.Input.MobileInput
{
    public class Joystick : MonoBehaviour
    {
        [SerializeField] private TouchZone _touchZone;
        [SerializeField] private RectTransform _handleTf;
        [SerializeField] private RectTransform _backgroundTf;
        private Camera _uiCamera;
        public Vector2 Direction { get; private set; }
        public bool IsPressed { get; private set; }


        private void Awake()
        {
            var canvas = GetComponentInParent<Canvas>();
            _uiCamera = canvas.worldCamera;
            
            if (_backgroundTf == null && _handleTf != null)
            {
                _backgroundTf = _handleTf.parent as RectTransform;
            }
        }

        private void OnEnable()
        {
            _touchZone.TouchDown += OnTouchDown;
            _touchZone.TouchUp += OnTouchUp;
            _touchZone.TouchMove += OnTouchMove;
            ResetHandle();
        }

        private void OnDisable()
        {
            _touchZone.TouchDown -= OnTouchDown;
            _touchZone.TouchUp -= OnTouchUp;
            _touchZone.TouchMove -= OnTouchMove;
        }

        private void OnTouchDown(Vector2 screenPosition)
        {
            IsPressed = true;
            UpdateHandle(screenPosition);
        }

        private void OnTouchUp(Vector2 screenPosition)
        {
            IsPressed = false;
            ResetHandle();
        }

        private void OnTouchMove(Vector2 screenPosition)
        {
            if (!IsPressed) return;

            UpdateHandle(screenPosition);
        }

        private void UpdateHandle(Vector2 screenPosition)
        {
            if (!TryGetBackgroundLocalPoint(screenPosition, out var localPoint)) return;

            var maxRadius = GetMaxRadius();
            var clampedOffset = maxRadius > 0f
                ? Vector2.ClampMagnitude(localPoint, maxRadius)
                : Vector2.zero;

            _handleTf.anchoredPosition = clampedOffset;
            Direction = maxRadius > 0f ? clampedOffset / maxRadius : Vector2.zero;
        }

        private void ResetHandle()
        {
            _handleTf.anchoredPosition = Vector2.zero;
            Direction = Vector2.zero;
        }

        private float GetMaxRadius()
        {
            var backgroundRadius = Mathf.Min(_backgroundTf.rect.width, _backgroundTf.rect.height) * 0.5f;
            return backgroundRadius;
        }

        private bool TryGetBackgroundLocalPoint(Vector2 screenPosition, out Vector2 localPoint)
        {
            return RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _backgroundTf,
                screenPosition,
                _uiCamera, 
                out localPoint);
        }
    }
}
