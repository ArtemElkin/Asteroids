using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.UI.HUD
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private Image[] _hpImages;
        [SerializeField] private TextMeshProUGUI _currentScoreText;
        [SerializeField] private TextMeshProUGUI _positionText;
        [SerializeField] private TextMeshProUGUI _rotationAngleText;
        [SerializeField] private TextMeshProUGUI _speedText;
        [SerializeField] private TextMeshProUGUI _laserBeamsText;
        [SerializeField] private TextMeshProUGUI _laserRechargeTimeText;


        public void SetHp(int newValue)
        {
            for (int i = 0; i < _hpImages.Length; i++)
            {
                _hpImages[i].enabled = i < newValue;
            }
        }

        public void SetCurrentScore(string newValue)
        {
            _currentScoreText.text = newValue;
        }

        public void SetPosition(string newValue)
        {
            _positionText.text = newValue;
        }

        public void SetRotationAngle(string newValue)
        {
            _rotationAngleText.text = newValue;
        }

        public void SetSpeed(string newValue)
        {
            _speedText.text = newValue;
        }

        public void SetLaserBeams(string newValue)
        {
            _laserBeamsText.text = newValue;
        }

        public void SetLaserRechargeTime(string newValue)
        {
            _laserRechargeTimeText.text = newValue;
        }
    }
}