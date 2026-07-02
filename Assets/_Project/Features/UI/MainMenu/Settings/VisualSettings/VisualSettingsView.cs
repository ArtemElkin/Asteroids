using Plugins.MVVM.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.UI.MainMenu.Settings.VisualSettings
{
    public class VisualSettingsView : BaseSettingsPageView
    {
        [Data("OnSpaceshipClonesClick")]
        public Button onSpaceshipClonesButton;
        [SerializeField] private Image _spaceshipClonesCheckbox;
            
        [Data("OnAsteroidsClonesClick")]
        public Button onAsteroidsClonesButton;
        [SerializeField] private Image _asteroidsClonesCheckbox;
        
        [SerializeField] private Sprite _selectedSprite;
        [SerializeField] private Sprite _unselectedSprite;
        
        [Setter("IsSpaceshipClonesEnabled")]
        public bool IsSpaceshipClonesEnabled
        {
            set => _spaceshipClonesCheckbox.sprite = value ? _selectedSprite : _unselectedSprite;
        }
        
        [Setter("IsAsteroidsClonesEnabled")]
        public bool IsAsteroidsClonesEnabled
        {
            set => _asteroidsClonesCheckbox.sprite = value ? _selectedSprite : _unselectedSprite;
        }
    }
}