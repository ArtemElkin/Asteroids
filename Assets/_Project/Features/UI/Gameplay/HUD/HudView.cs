using Plugins.MVVM.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.UI.Gameplay.HUD
{
    public class HudView : MonoBehaviour
    {
        public Image[] _hpImages;
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


        public void SetHp(int newValue)
        {
            for (int i = 0; i < _hpImages.Length; i++)
            {
                _hpImages[i].enabled = i < newValue;
            }
        }
    }
}