using _Project.Core.Audio;
using _Project.Features.Common.Settings;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioService : MonoBehaviour, IAudioService<AudioClip>
    {
        private AudioSource _audioSource;
        private SettingsModel _settingsModel;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        [Inject]
        private void Construct(SettingsModel settingsModel)
        {
            _settingsModel = settingsModel;
        }
        
        public void Play(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        public void Pause()
        {
            _audioSource.Pause();
        }

        public void Resume()
        {
            _audioSource.UnPause();
        }

        public void StopAll()
        {
            _audioSource.Stop();
        }
    }
}