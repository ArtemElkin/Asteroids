using _Project.Features.Common.Settings;
using Plugins.MVVM.Attributes;
using UniRx;

namespace _Project.Features.UI.MainMenu.Settings.VolumeSettings
{
    public class VolumeSettingsViewModel : BaseSettingsPageViewModel
    {
        [Data("SoundsVolume")]
        public ReactiveProperty<int> SoundsVolume = new();
        [Data("MusicVolume")]
        public ReactiveProperty<int> MusicVolume = new();
        private readonly SettingsModel _settingsModel;


        public VolumeSettingsViewModel(
            SettingsModel settingsModel)
        {
            _settingsModel = settingsModel;
            SoundsVolume.Value = _settingsModel.SoundsVolume;
            MusicVolume.Value = _settingsModel.MusicVolume;
        }
        
        [Method("SoundsVolumeSlider")]
        public void OnSoundsVolumeChanged(float value)
        {
            var intValue = (int)value;
            SoundsVolume.Value = intValue;
            _settingsModel.SetSoundsVolume(intValue);
        }
        [Method("MusicVolumeSlider")]
        public void OnMusicVolumeChanged(float value)
        {
            var intValue = (int)value;
            MusicVolume.Value = intValue;
            _settingsModel.SetMusicVolume(intValue);
        }
    }
}