using Plugins.MVVM.Attributes;
using UnityEngine.UI;

namespace _Project.Features.UI.MainMenu.Settings.VolumeSettings
{
    public class VolumeSettingsView : BaseSettingsPageView
    {
        [Data("SoundsVolumeSlider")]
        public Slider soundsVolumeSlider;
        [Setter("SoundsVolume")]
        public int SoundsVolume
        {
            set => soundsVolumeSlider.value = value;
        }
        [Data("MusicVolumeSlider")]
        public Slider musicVolumeSlider;
        [Setter("MusicVolume")]
        public int MusicVolume
        {
            set => musicVolumeSlider.value = value;
        }
    }
}