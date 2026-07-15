using System;
using _Project.Core.Audio;
using _Project.Core.GameLifecycle;
using UnityEngine;

namespace _Project.Infrastructure.Audio
{
    public class AudioPauseController : IDisposable
    {
        private readonly IAudioService<AudioClip> _audioService;
        private readonly IGameStateService _gameStateService;


        public AudioPauseController(IAudioService<AudioClip> audioService, IGameStateService gameStateService)
        {
            _audioService = audioService;
            _gameStateService = gameStateService;
            _gameStateService.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState, TransitionType transitionType)
        {
            switch (transitionType)
            {
                case TransitionType.OnStart:
                    _audioService.StopAllSounds();
                    break;
                case TransitionType.OnPause:
                    _audioService.PauseSound();
                    break;
                case TransitionType.OnResume:
                    _audioService.ResumeSound();
                    break;
            }
        }


        public void Dispose()
        {
            _gameStateService.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}