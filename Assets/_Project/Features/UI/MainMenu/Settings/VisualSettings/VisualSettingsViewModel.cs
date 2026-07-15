using _Project.Features.Common.Settings;
using Plugins.MVVM.Attributes;
using UniRx;

namespace _Project.Features.UI.MainMenu.Settings.VisualSettings
{
    public class VisualSettingsViewModel : BaseSettingsPageViewModel
    {
        [Data("IsSpaceshipClonesEnabled")]
        public ReactiveProperty<bool> IsSpaceshipClonesEnabled = new();
        [Data("IsAsteroidsClonesEnabled")]
        public ReactiveProperty<bool> IsAsteroidsClonesEnabled = new();
        
        private readonly SettingsModel _settingsModel;
        
        public VisualSettingsViewModel(SettingsModel settingsModel)
        {
            _settingsModel = settingsModel;
            
            IsSpaceshipClonesEnabled.Value = settingsModel.SpaceshipClonesEnabled;
            IsAsteroidsClonesEnabled.Value = settingsModel.AsteroidsClonesEnabled;
        }
        
        [Method("OnSpaceshipClonesClick")]
        public void OnSpaceshipClonesClicked()
        {
            _settingsModel.TurnSpaceshipClonesEnabled();
            IsSpaceshipClonesEnabled.Value = _settingsModel.SpaceshipClonesEnabled;
        }

        [Method("OnAsteroidsClonesClick")]
        public void OnAsteroidsClonesClicked()
        {
            _settingsModel.TurnAsteroidsClonesEnabled();
            IsAsteroidsClonesEnabled.Value = _settingsModel.AsteroidsClonesEnabled;
        }
    }
}