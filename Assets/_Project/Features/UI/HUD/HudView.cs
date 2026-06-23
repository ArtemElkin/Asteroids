using Plugins.MVVM.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.UI.HUD
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] public Image[] _hpImages;
        [Data("Score")]
        [SerializeField] public TMP_Text _currentScoreText;
        [Data("Position")]
        [SerializeField] public TMP_Text _positionText;
        [Data("RotationAngle")]
        [SerializeField] public TMP_Text _rotationAngleText;
        [Data("Speed")]
        [SerializeField] public TMP_Text _speedText;
        [Data("LaserBeams")]
        [SerializeField] public TMP_Text _laserBeamsText;
        [Data("LaserRechargeTime")]
        [SerializeField] public TMP_Text _laserRechargeTimeText;


        public void SetHp(int newValue)
        {
            for (int i = 0; i < _hpImages.Length; i++)
            {
                _hpImages[i].enabled = i < newValue;
            }
        }
    }
}