using System;
using _Project.Core.Audio;
using _Project.Core.Render.VFX;
using _Project.Core.Services;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Effects
{
    public class SoundEffect : MonoBehaviour, IEffect
    {
        [SerializeField] private AudioClip _clip;
        public event Action OnEnded;
        public bool IsPlaying { get; private set; }
        private IAudioService<AudioClip> _audioService;
        private Timer _timer;

        [Inject]
        private void Construct(IAudioService<AudioClip>  audioService, Timer timer)
        {
            _audioService = audioService;
            _timer = timer;
            _timer.Elapsed += OnTimerElapsed;
        }

        public void Play()
        {
            if (_clip == null) return;
            
            _audioService.PlaySound(_clip);
            _timer.Start(_clip.length);
            IsPlaying = true;
        }

        public void Pause() { }
        public void Resume() { }

        public void Stop()
        {
            if (!IsPlaying) return;
            
            IsPlaying = false;
            _timer.Stop();
            OnEnded?.Invoke();
        }
        
        private void OnTimerElapsed()
        {
            Stop();
        }

        private void OnDestroy()
        {
            _timer.Elapsed -= OnTimerElapsed;
            _timer.Dispose();
        }
    }
}