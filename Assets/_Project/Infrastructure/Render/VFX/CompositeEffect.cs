using _Project.Core.Render.VFX;
using UnityEngine;

namespace _Project.Infrastructure.Render.VFX
{
    [RequireComponent(typeof(ParticleSystem))]
    [RequireComponent(typeof(AudioSource))]
    public class CompositeEffect : MonoBehaviour, IEffect
    {
        [SerializeField] private AudioClip _clip;
        public bool IsPaused { get; private set; }
        protected ParticleSystem _particleSystem;
        private AudioSource _audioSource;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            if (IsPaused) IsPaused = false;
            _particleSystem.Play();
            if (_clip != null) _audioSource.PlayOneShot(_clip);
        }

        public void Pause()
        {
            IsPaused = true;
            _particleSystem.Pause();
            _audioSource.Pause();
        }

        public void Stop()
        {
            IsPaused = false;
            _particleSystem.Stop();
            _particleSystem.Clear();
            _audioSource.Stop();
        }
    }
}