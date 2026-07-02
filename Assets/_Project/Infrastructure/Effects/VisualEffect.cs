using System;
using _Project.Core.Render.VFX;
using UnityEngine;

namespace _Project.Infrastructure.Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class VisualEffect : MonoBehaviour, IEffect
    {
        public bool IsPlaying { get; private set; }
        private ParticleSystem _particleSystem;
        public event Action OnEnded;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
        
        public void Play()
        {
            IsPlaying = true;
            _particleSystem.Play();
        }

        public void Pause()
        {
            if (!IsPlaying) return;
            
            _particleSystem.Pause();
        }

        public void Resume()
        {
            if (!IsPlaying) return;
            
            _particleSystem.Play();
        }

        public void Stop()
        {
            if (!IsPlaying) return;
            
            IsPlaying = false;
            _particleSystem.Stop();
            _particleSystem.Clear();
            OnEnded?.Invoke();
        }

        private void OnParticleSystemStopped()
        {
            Stop();
        }
    }
}