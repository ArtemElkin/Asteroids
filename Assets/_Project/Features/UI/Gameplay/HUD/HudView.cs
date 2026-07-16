using Plugins.MVVM.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.UI.Gameplay.HUD
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        public Image[] _healthImages;
        [Data("Score")]
        public TMP_Text _currentScoreText;
        [Data("Position")]
        public TMP_Text _positionText;
        [Data("RotationAngle")]
        public TMP_Text _rotationAngleText;
        [Data("Speed")]
        public TMP_Text _speedText;
        [Data("LaserBeams")]
        public TMP_Text _laserBeamsText;
        [Data("LaserRechargeTime")]
        public TMP_Text _laserRechargeTimeText;
        [Setter("Active")]
        public bool Active
        {
            set => _window.gameObject.SetActive(value);
        }

        public void SetHealth(int newValue)
        {
            for (int i = 0; i < _healthImages.Length; i++)
            {
                _healthImages[i].enabled = i < newValue;
            }
        }
    }
}