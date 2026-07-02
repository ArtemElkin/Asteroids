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
        private readonly SettingsSaveController _settingsSaveController;
        
        public VisualSettingsViewModel(SettingsModel settingsModel, SettingsSaveController settingsSaveController)
        {
            _settingsModel = settingsModel;
            _settingsSaveController = settingsSaveController;
            
            IsSpaceshipClonesEnabled.Value = settingsModel.SpaceshipClonesEnabled;
            IsAsteroidsClonesEnabled.Value = settingsModel.AsteroidsClonesEnabled;
        }
        
        [Method("OnSpaceshipClonesClick")]
        public void OnSpaceshipClonesClicked()
        {
            _settingsModel.TurnSpaceshipClonesEnabled();
            _settingsSaveController.Save();
            IsSpaceshipClonesEnabled.Value = _settingsModel.SpaceshipClonesEnabled;
        }

        [Method("OnAsteroidsClonesClick")]
        public void OnAsteroidsClonesClicked()
        {
            _settingsModel.TurnAsteroidsClonesEnabled();
            _settingsSaveController.Save();
            IsAsteroidsClonesEnabled.Value = _settingsModel.AsteroidsClonesEnabled;
        }
    }
}