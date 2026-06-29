using Plugins.MVVM.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.UI.Settings.GameSettings
{
    public class GameSettingsView : BaseSettingsPageView
    {
        [Data("OnElasticClick")]
        public Button _elasticOptionButton;
        [SerializeField] private Image _elasticOptionImage;
        [Data("OnSimpleReflectionClick")]
        public Button _simpleReflectionOptionButton;
        [SerializeField] private Image _simpleReflectionOptionImage;

        [SerializeField] private Sprite _selectedSprite;
        [SerializeField] private Sprite _unselectedSprite;

        [Setter("IsElasticSelected")]
        public bool IsElasticSelected
        {
            set => _elasticOptionImage.sprite = value ? _selectedSprite : _unselectedSprite;
        }
        [Setter("IsSimpleReflectionSelected")]
        public bool IsSimpleReflectionSelected
        {
            set => _simpleReflectionOptionImage.sprite = value ? _selectedSprite : _unselectedSprite;
        }
    }
}