using _Project.Core.Audio;
using _Project.Features.Common.Settings;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Audio
{
    public class AudioService : MonoBehaviour, IAudioService<AudioClip>
    {
        [SerializeField] private AudioSource _soundsAudioSource;
        [SerializeField] private AudioSource _musicAudioSource;
        private SettingsModel _settingsModel;


        [Inject]
        private void Construct(SettingsModel settingsModel)
        {
            _settingsModel = settingsModel;
            _settingsModel.OnSoundsVolumeChanged += OnSoundsVolumeChanged;
            _settingsModel.OnMusicVolumeChanged += OnMusicVolumeChanged;
            OnSoundsVolumeChanged(_settingsModel.SoundsVolume);
            OnMusicVolumeChanged(_settingsModel.MusicVolume);
        }
        
        public void PlaySound(AudioClip clip)
        {
            _soundsAudioSource.PlayOneShot(clip);
        }

        public void PauseSound()
        {
            _soundsAudioSource.Pause();
        }

        public void ResumeSound()
        {
            _soundsAudioSource.UnPause();
        }

        public void StopAllSounds()
        {
            _soundsAudioSource.Stop();
        }

        private void OnSoundsVolumeChanged(int value)
        {
            _soundsAudioSource.volume = NormalizeVolume(value);
        }

        private void OnMusicVolumeChanged(int value)
        {
            _musicAudioSource.volume = NormalizeVolume(value);
        }

        private static float NormalizeVolume(int value)
        {
            return Mathf.Clamp01((float)value / SettingsSave.MaxVolumeLevel);
        }

        private void OnDestroy()
        {
            _settingsModel.OnSoundsVolumeChanged -= OnSoundsVolumeChanged;
            _settingsModel.OnMusicVolumeChanged -= OnMusicVolumeChanged;
        }
    }
}